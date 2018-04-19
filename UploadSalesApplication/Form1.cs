using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace UploadSalesApplication
{
    public partial class Form1 : Form, UploadSalesInterface
    {
        SBSControl sbsControl;
        BindingSource SBind = new BindingSource();
        DataTable data_settings = null;
        string displayMessage = "";
        public Form1()
        {
            InitializeComponent();
            bindTextboxToProperties(group_excel);
            bindTextboxToProperties(group_server);
            bindTextboxToProperties(group_ecm);
            cb_ecm.Checked = Properties.Settings.Default.ecm_use_ecm;
            sbsControl = new SBSControl();
            data_settings = sbsControl.data_settings.Copy();
            SBind.DataSource = data_settings;
            data_sbs.DataSource = data_settings;
            data_sbs.DataSource = SBind;
            data_sbs.Refresh();
        }

        private bool requiresExcelColumnConversion(string propertyName)
        {
            return (propertyName.Contains("col_") || propertyName.Equals("alu") || propertyName.Equals("upc"));
        }

        private void bindTextboxToProperties(GroupBox group)
        {
            foreach (Control control in group.Controls)
            {
                if (control is TextBox)
                {
                    TextBox tb = control as TextBox;
                    if (tb.Tag != null)
                    {
                        string propertyName = tb.Tag.ToString();
                        string text = Properties.Settings.Default[propertyName].ToString();
                        if (requiresExcelColumnConversion(tb.Tag.ToString())){
                            text = ExcelColumnFromNumber((int)Properties.Settings.Default[propertyName]);
                        }
                        tb.Text = text;
                        tb.Leave += new EventHandler(saveSettings);
                    }
                }
            }
        }

        private void bn_run_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tb_sbs_no.Text)){
                MessageBox.Show("Please enter subsidiary numbers (e.g: 1,2,101)");
                return;
            };

            disableForm();
            new UploadSalesControl(this, sbsControl).beginProcess(tb_sbs_no.Text);
        }

        private void enableForm()
        {
            group_excel.Enabled = true;
            group_server.Enabled = true;
            data_sbs.Enabled = true;
            bn_save_sbs_settings.Enabled = true;
            bn_run.Enabled = true;
        }

        private void disableForm()
        {
            group_excel.Enabled = false;
            group_server.Enabled = false;
            data_sbs.Enabled = false;
            bn_save_sbs_settings.Enabled = false;
            bn_run.Enabled = false;
        }

        public void updateMessage(string message, bool keep)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string, bool>(updateMessage), new object[] { message, keep });
                return;
            }

            tb_msg.Text = displayMessage + "\r\n" + message;
            if (keep)
            {
                displayMessage += "\r\n" + message;
            }
        }


        private void saveSettings(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (string.IsNullOrEmpty(tb.Text))
                return;
            if (tb.Tag != null)
            {
                string propertyName = tb.Tag.ToString();
                //additional check for validity of new values if they are supposed to be integer values
                if (requiresExcelColumnConversion(propertyName))
                {
                    try
                    {
                        Properties.Settings.Default[propertyName] = NumberFromExcelColumn(tb.Text);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Please enter a valid excel column (A to ZZ");
                        return;
                    }
                }
                else if (propertyName.Equals("Worksheet"))
                {
                    int newValue = -1;
                    if (!Int32.TryParse(tb.Text, out newValue))
                    {
                        tb.Text = Properties.Settings.Default[propertyName].ToString();
                        return;
                    }
                    Properties.Settings.Default[propertyName] = newValue;
                }
                else {
                    Properties.Settings.Default[propertyName] = tb.Text;
                }
                Properties.Settings.Default.Save();
            }
        }

        public void workDone()
        {
            enableForm();
            displayMessage += "\r\nTask completed.";
            tb_msg.Text = displayMessage;
        }
        //A=1, Z=26
        public static string ExcelColumnFromNumber(int column)
        {
            string columnString = "";
            int columnNumber = column;
            while (columnNumber > 0)
            {
                int currentLetterNumber = (columnNumber - 1) % 26;
                char currentLetter = (char)(currentLetterNumber + 65);
                columnString = currentLetter + columnString;
                columnNumber = (columnNumber - (currentLetterNumber + 1)) / 26;
            }
            return columnString;
        }

        public static int NumberFromExcelColumn(string column)
        {
            int retVal = 0;
            string col = column.ToUpper();
            for (int iChar = col.Length - 1; iChar >= 0; iChar--)
            {
                char colPiece = col[iChar];
                int colNum = colPiece - 64;
                retVal = retVal + colNum * (int)Math.Pow(26, col.Length - (iChar + 1));
            }
            return retVal;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                tb_ecm_location.Text = openFileDialog.FileName;
                Properties.Settings.Default.ecm_file_path = openFileDialog.FileName;
                Properties.Settings.Default.Save();
            }
        }

        private void enableECM(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            Properties.Settings.Default.ecm_use_ecm = cb.Checked;
            Properties.Settings.Default.Save();
        }


        private void data_sbs_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            
            if (e.ColumnIndex == (int)SBSControl.COL_NAMES.INPUT
                || e.ColumnIndex == (int)SBSControl.COL_NAMES.FTP_INPUT
                || e.ColumnIndex == (int)SBSControl.COL_NAMES.FTP_ARCHIVE
                || e.ColumnIndex == (int)SBSControl.COL_NAMES.OUTPUT
                || e.ColumnIndex == (int)SBSControl.COL_NAMES.ARCHIVE
                || e.ColumnIndex == (int)SBSControl.COL_NAMES.ERROR)
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                object path = data_sbs.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                if (path!=null)
                {
                    fbd.SelectedPath = path.ToString();
                }
                DialogResult result = fbd.ShowDialog();
                if (!string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    data_sbs.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = fbd.SelectedPath;
                }
                data_sbs.BeginEdit(false);
            }
        }

        private void bn_save_sbs_settings_Click(object sender, EventArgs e)
        {
            foreach (DataRow row in data_settings.Rows)
            {
                for (int i = 0; i < data_settings.Columns.Count; ++i)
                {
                    if (string.IsNullOrEmpty(row[i].ToString()) && i != (int)SBSControl.COL_NAMES.NAME)
                    {
                        MessageBox.Show("Please fill in values for all cells");
                        data_sbs.CurrentCell = data_sbs.Rows[data_settings.Rows.IndexOf(row)].Cells[i];
                        data_sbs.BeginEdit(false);
                        return;
                    }
                    string data = row[i].ToString();
                    if (i == (int)SBSControl.COL_NAMES.SBS)
                    {
                        int reference;
                        if (!Int32.TryParse(data, out reference))
                        {
                            MessageBox.Show("Please fill in correct value for subsidiary");
                            data_sbs.CurrentCell = data_sbs.Rows[data_settings.Rows.IndexOf(row)].Cells[i];
                            data_sbs.BeginEdit(false);
                            return;
                        }
                    }
                    else if (i != (int)SBSControl.COL_NAMES.STID && i !=(int)SBSControl.COL_NAMES.NAME)
                    {
                        if (!Directory.Exists(data))
                        {
                            MessageBox.Show("Directory not valid");
                            data_sbs.CurrentCell = data_sbs.Rows[data_settings.Rows.IndexOf(row)].Cells[i];
                            data_sbs.BeginEdit(false);
                            return;
                        }
                    }
                }
               
            }
            sbsControl.save(data_settings);
        }
    }
}
