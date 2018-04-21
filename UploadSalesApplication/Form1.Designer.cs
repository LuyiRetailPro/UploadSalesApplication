namespace UploadSalesApplication
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabs = new System.Windows.Forms.TabControl();
            this.tab_main = new System.Windows.Forms.TabPage();
            this.label18 = new System.Windows.Forms.Label();
            this.tb_msg = new System.Windows.Forms.RichTextBox();
            this.tab_server_settings = new System.Windows.Forms.TabPage();
            this.group_ecm = new System.Windows.Forms.GroupBox();
            this.cb_ecm = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label22 = new System.Windows.Forms.Label();
            this.tb_ecm_parameters = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.tb_ecm_location = new System.Windows.Forms.TextBox();
            this.group_server = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tb_password = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tb_user_id = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tb_service_name = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tb_port = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_host = new System.Windows.Forms.TextBox();
            this.tab_excel_setting = new System.Windows.Forms.TabPage();
            this.group_excel = new System.Windows.Forms.GroupBox();
            this.label17 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.tb_total_price = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.tb_qty = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.tb_size = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tb_sku = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tb_receipt_no = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tb_store_no = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tb_upc = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.tb_worksheet_no = new System.Windows.Forms.TextBox();
            this.tab_subsidiary = new System.Windows.Forms.TabPage();
            this.bn_save_sbs_settings = new System.Windows.Forms.Button();
            this.data_sbs = new System.Windows.Forms.DataGridView();
            this.bn_run = new System.Windows.Forms.Button();
            this.label19 = new System.Windows.Forms.Label();
            this.tb_sbs_no = new System.Windows.Forms.TextBox();
            this.tabs.SuspendLayout();
            this.tab_main.SuspendLayout();
            this.tab_server_settings.SuspendLayout();
            this.group_ecm.SuspendLayout();
            this.group_server.SuspendLayout();
            this.tab_excel_setting.SuspendLayout();
            this.group_excel.SuspendLayout();
            this.tab_subsidiary.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.data_sbs)).BeginInit();
            this.SuspendLayout();
            // 
            // tabs
            // 
            this.tabs.Controls.Add(this.tab_main);
            this.tabs.Controls.Add(this.tab_server_settings);
            this.tabs.Controls.Add(this.tab_excel_setting);
            this.tabs.Controls.Add(this.tab_subsidiary);
            this.tabs.Location = new System.Drawing.Point(12, 13);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(904, 401);
            this.tabs.TabIndex = 0;
            // 
            // tab_main
            // 
            this.tab_main.Controls.Add(this.label18);
            this.tab_main.Controls.Add(this.tb_msg);
            this.tab_main.Location = new System.Drawing.Point(4, 25);
            this.tab_main.Name = "tab_main";
            this.tab_main.Padding = new System.Windows.Forms.Padding(3);
            this.tab_main.Size = new System.Drawing.Size(896, 372);
            this.tab_main.TabIndex = 0;
            this.tab_main.Text = "Main";
            this.tab_main.UseVisualStyleBackColor = true;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(8, 16);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(52, 17);
            this.label18.TabIndex = 20;
            this.label18.Text = "Status:";
            // 
            // tb_msg
            // 
            this.tb_msg.Location = new System.Drawing.Point(11, 36);
            this.tb_msg.Name = "tb_msg";
            this.tb_msg.ReadOnly = true;
            this.tb_msg.Size = new System.Drawing.Size(879, 299);
            this.tb_msg.TabIndex = 7;
            this.tb_msg.Text = "";
            // 
            // tab_server_settings
            // 
            this.tab_server_settings.Controls.Add(this.group_ecm);
            this.tab_server_settings.Controls.Add(this.group_server);
            this.tab_server_settings.Location = new System.Drawing.Point(4, 25);
            this.tab_server_settings.Name = "tab_server_settings";
            this.tab_server_settings.Padding = new System.Windows.Forms.Padding(3);
            this.tab_server_settings.Size = new System.Drawing.Size(896, 372);
            this.tab_server_settings.TabIndex = 1;
            this.tab_server_settings.Text = "Server Settings";
            this.tab_server_settings.UseVisualStyleBackColor = true;
            // 
            // group_ecm
            // 
            this.group_ecm.Controls.Add(this.cb_ecm);
            this.group_ecm.Controls.Add(this.button1);
            this.group_ecm.Controls.Add(this.label22);
            this.group_ecm.Controls.Add(this.tb_ecm_parameters);
            this.group_ecm.Controls.Add(this.label23);
            this.group_ecm.Controls.Add(this.tb_ecm_location);
            this.group_ecm.Location = new System.Drawing.Point(16, 203);
            this.group_ecm.Name = "group_ecm";
            this.group_ecm.Size = new System.Drawing.Size(452, 143);
            this.group_ecm.TabIndex = 12;
            this.group_ecm.TabStop = false;
            this.group_ecm.Text = "ECM Details";
            // 
            // cb_ecm
            // 
            this.cb_ecm.AutoSize = true;
            this.cb_ecm.Location = new System.Drawing.Point(117, 84);
            this.cb_ecm.Name = "cb_ecm";
            this.cb_ecm.Size = new System.Drawing.Size(211, 21);
            this.cb_ecm.TabIndex = 18;
            this.cb_ecm.Text = "Use ECM after xml is created";
            this.cb_ecm.UseVisualStyleBackColor = true;
            this.cb_ecm.CheckedChanged += new System.EventHandler(this.enableECM);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(367, 59);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 17;
            this.button1.Text = "Browse";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(6, 114);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(85, 17);
            this.label22.TabIndex = 5;
            this.label22.Text = "Parameters:";
            // 
            // tb_ecm_parameters
            // 
            this.tb_ecm_parameters.Location = new System.Drawing.Point(117, 111);
            this.tb_ecm_parameters.Name = "tb_ecm_parameters";
            this.tb_ecm_parameters.Size = new System.Drawing.Size(325, 22);
            this.tb_ecm_parameters.TabIndex = 4;
            this.tb_ecm_parameters.Tag = "ecm_parameters";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(6, 34);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(99, 17);
            this.label23.TabIndex = 3;
            this.label23.Text = "ECM Location:";
            // 
            // tb_ecm_location
            // 
            this.tb_ecm_location.Location = new System.Drawing.Point(117, 31);
            this.tb_ecm_location.Name = "tb_ecm_location";
            this.tb_ecm_location.ReadOnly = true;
            this.tb_ecm_location.Size = new System.Drawing.Size(325, 22);
            this.tb_ecm_location.TabIndex = 2;
            this.tb_ecm_location.Tag = "ecm_file_path";
            // 
            // group_server
            // 
            this.group_server.Controls.Add(this.label8);
            this.group_server.Controls.Add(this.tb_password);
            this.group_server.Controls.Add(this.label7);
            this.group_server.Controls.Add(this.tb_user_id);
            this.group_server.Controls.Add(this.label6);
            this.group_server.Controls.Add(this.tb_service_name);
            this.group_server.Controls.Add(this.label5);
            this.group_server.Controls.Add(this.tb_port);
            this.group_server.Controls.Add(this.label4);
            this.group_server.Controls.Add(this.tb_host);
            this.group_server.Location = new System.Drawing.Point(16, 20);
            this.group_server.Name = "group_server";
            this.group_server.Size = new System.Drawing.Size(452, 177);
            this.group_server.TabIndex = 8;
            this.group_server.TabStop = false;
            this.group_server.Text = "Server Details";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 146);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(69, 17);
            this.label8.TabIndex = 11;
            this.label8.Text = "Password";
            // 
            // tb_password
            // 
            this.tb_password.Location = new System.Drawing.Point(117, 143);
            this.tb_password.Name = "tb_password";
            this.tb_password.Size = new System.Drawing.Size(325, 22);
            this.tb_password.TabIndex = 10;
            this.tb_password.Tag = "password";
            this.tb_password.UseSystemPasswordChar = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 118);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 17);
            this.label7.TabIndex = 9;
            this.label7.Text = "User ID:";
            // 
            // tb_user_id
            // 
            this.tb_user_id.Location = new System.Drawing.Point(117, 115);
            this.tb_user_id.Name = "tb_user_id";
            this.tb_user_id.Size = new System.Drawing.Size(325, 22);
            this.tb_user_id.TabIndex = 8;
            this.tb_user_id.Tag = "Userid";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 90);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 17);
            this.label6.TabIndex = 7;
            this.label6.Text = "Service Name:";
            // 
            // tb_service_name
            // 
            this.tb_service_name.Location = new System.Drawing.Point(117, 87);
            this.tb_service_name.Name = "tb_service_name";
            this.tb_service_name.Size = new System.Drawing.Size(325, 22);
            this.tb_service_name.TabIndex = 6;
            this.tb_service_name.Tag = "Service_name";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 62);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 17);
            this.label5.TabIndex = 5;
            this.label5.Text = "Port:";
            // 
            // tb_port
            // 
            this.tb_port.Location = new System.Drawing.Point(117, 59);
            this.tb_port.Name = "tb_port";
            this.tb_port.Size = new System.Drawing.Size(325, 22);
            this.tb_port.TabIndex = 4;
            this.tb_port.Tag = "Port";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 17);
            this.label4.TabIndex = 3;
            this.label4.Text = "Host:";
            // 
            // tb_host
            // 
            this.tb_host.Location = new System.Drawing.Point(117, 31);
            this.tb_host.Name = "tb_host";
            this.tb_host.Size = new System.Drawing.Size(325, 22);
            this.tb_host.TabIndex = 2;
            this.tb_host.Tag = "Host";
            // 
            // tab_excel_setting
            // 
            this.tab_excel_setting.Controls.Add(this.group_excel);
            this.tab_excel_setting.Location = new System.Drawing.Point(4, 25);
            this.tab_excel_setting.Name = "tab_excel_setting";
            this.tab_excel_setting.Padding = new System.Windows.Forms.Padding(3);
            this.tab_excel_setting.Size = new System.Drawing.Size(896, 372);
            this.tab_excel_setting.TabIndex = 2;
            this.tab_excel_setting.Text = "Excel Settings";
            this.tab_excel_setting.UseVisualStyleBackColor = true;
            // 
            // group_excel
            // 
            this.group_excel.Controls.Add(this.label17);
            this.group_excel.Controls.Add(this.textBox1);
            this.group_excel.Controls.Add(this.label16);
            this.group_excel.Controls.Add(this.tb_total_price);
            this.group_excel.Controls.Add(this.label15);
            this.group_excel.Controls.Add(this.tb_qty);
            this.group_excel.Controls.Add(this.label14);
            this.group_excel.Controls.Add(this.tb_size);
            this.group_excel.Controls.Add(this.label9);
            this.group_excel.Controls.Add(this.tb_sku);
            this.group_excel.Controls.Add(this.label10);
            this.group_excel.Controls.Add(this.tb_receipt_no);
            this.group_excel.Controls.Add(this.label11);
            this.group_excel.Controls.Add(this.tb_store_no);
            this.group_excel.Controls.Add(this.label12);
            this.group_excel.Controls.Add(this.tb_upc);
            this.group_excel.Controls.Add(this.label13);
            this.group_excel.Controls.Add(this.tb_worksheet_no);
            this.group_excel.Location = new System.Drawing.Point(15, 17);
            this.group_excel.Name = "group_excel";
            this.group_excel.Size = new System.Drawing.Size(421, 291);
            this.group_excel.TabIndex = 13;
            this.group_excel.TabStop = false;
            this.group_excel.Text = "Excel Details";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(6, 258);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(40, 17);
            this.label17.TabIndex = 19;
            this.label17.Text = "date:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(117, 255);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(286, 22);
            this.textBox1.TabIndex = 18;
            this.textBox1.Tag = "col_date";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(6, 230);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(78, 17);
            this.label16.TabIndex = 17;
            this.label16.Text = "total_price:";
            // 
            // tb_total_price
            // 
            this.tb_total_price.Location = new System.Drawing.Point(117, 227);
            this.tb_total_price.Name = "tb_total_price";
            this.tb_total_price.Size = new System.Drawing.Size(286, 22);
            this.tb_total_price.TabIndex = 16;
            this.tb_total_price.Tag = "col_total_price";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 202);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(31, 17);
            this.label15.TabIndex = 15;
            this.label15.Text = "qty:";
            // 
            // tb_qty
            // 
            this.tb_qty.Location = new System.Drawing.Point(117, 199);
            this.tb_qty.Name = "tb_qty";
            this.tb_qty.Size = new System.Drawing.Size(286, 22);
            this.tb_qty.TabIndex = 14;
            this.tb_qty.Tag = "col_qty";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 174);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(37, 17);
            this.label14.TabIndex = 13;
            this.label14.Text = "size:";
            // 
            // tb_size
            // 
            this.tb_size.Location = new System.Drawing.Point(117, 171);
            this.tb_size.Name = "tb_size";
            this.tb_size.Size = new System.Drawing.Size(286, 22);
            this.tb_size.TabIndex = 12;
            this.tb_size.Tag = "col_size";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 146);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(34, 17);
            this.label9.TabIndex = 11;
            this.label9.Text = "sku:";
            // 
            // tb_sku
            // 
            this.tb_sku.Location = new System.Drawing.Point(117, 143);
            this.tb_sku.Name = "tb_sku";
            this.tb_sku.Size = new System.Drawing.Size(286, 22);
            this.tb_sku.TabIndex = 10;
            this.tb_sku.Tag = "alu";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 118);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(75, 17);
            this.label10.TabIndex = 9;
            this.label10.Text = "receipt_no";
            // 
            // tb_receipt_no
            // 
            this.tb_receipt_no.Location = new System.Drawing.Point(117, 115);
            this.tb_receipt_no.Name = "tb_receipt_no";
            this.tb_receipt_no.Size = new System.Drawing.Size(286, 22);
            this.tb_receipt_no.TabIndex = 8;
            this.tb_receipt_no.Tag = "col_receipt_no";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 90);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(68, 17);
            this.label11.TabIndex = 7;
            this.label11.Text = "store_no:";
            // 
            // tb_store_no
            // 
            this.tb_store_no.Location = new System.Drawing.Point(117, 87);
            this.tb_store_no.Name = "tb_store_no";
            this.tb_store_no.Size = new System.Drawing.Size(286, 22);
            this.tb_store_no.TabIndex = 6;
            this.tb_store_no.Tag = "col_store_no";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 62);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(31, 17);
            this.label12.TabIndex = 5;
            this.label12.Text = "upc";
            // 
            // tb_upc
            // 
            this.tb_upc.Location = new System.Drawing.Point(117, 59);
            this.tb_upc.Name = "tb_upc";
            this.tb_upc.Size = new System.Drawing.Size(286, 22);
            this.tb_upc.TabIndex = 4;
            this.tb_upc.Tag = "upc";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 34);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(102, 17);
            this.label13.TabIndex = 3;
            this.label13.Text = "Worksheet No:";
            // 
            // tb_worksheet_no
            // 
            this.tb_worksheet_no.Location = new System.Drawing.Point(117, 31);
            this.tb_worksheet_no.Name = "tb_worksheet_no";
            this.tb_worksheet_no.Size = new System.Drawing.Size(286, 22);
            this.tb_worksheet_no.TabIndex = 2;
            this.tb_worksheet_no.Tag = "Worksheet_no";
            // 
            // tab_subsidiary
            // 
            this.tab_subsidiary.Controls.Add(this.bn_save_sbs_settings);
            this.tab_subsidiary.Controls.Add(this.data_sbs);
            this.tab_subsidiary.Location = new System.Drawing.Point(4, 25);
            this.tab_subsidiary.Name = "tab_subsidiary";
            this.tab_subsidiary.Padding = new System.Windows.Forms.Padding(3);
            this.tab_subsidiary.Size = new System.Drawing.Size(896, 372);
            this.tab_subsidiary.TabIndex = 3;
            this.tab_subsidiary.Text = "Subsidiary Settings";
            this.tab_subsidiary.UseVisualStyleBackColor = true;
            // 
            // bn_save_sbs_settings
            // 
            this.bn_save_sbs_settings.Location = new System.Drawing.Point(815, 319);
            this.bn_save_sbs_settings.Name = "bn_save_sbs_settings";
            this.bn_save_sbs_settings.Size = new System.Drawing.Size(75, 23);
            this.bn_save_sbs_settings.TabIndex = 8;
            this.bn_save_sbs_settings.Text = "Save";
            this.bn_save_sbs_settings.UseVisualStyleBackColor = true;
            this.bn_save_sbs_settings.Click += new System.EventHandler(this.bn_save_sbs_settings_Click);
            // 
            // data_sbs
            // 
            this.data_sbs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.data_sbs.Location = new System.Drawing.Point(6, 6);
            this.data_sbs.Name = "data_sbs";
            this.data_sbs.RowTemplate.Height = 24;
            this.data_sbs.Size = new System.Drawing.Size(884, 307);
            this.data_sbs.TabIndex = 0;
            this.data_sbs.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.data_sbs_CellEnter);
            // 
            // bn_run
            // 
            this.bn_run.Location = new System.Drawing.Point(831, 419);
            this.bn_run.Name = "bn_run";
            this.bn_run.Size = new System.Drawing.Size(75, 23);
            this.bn_run.TabIndex = 7;
            this.bn_run.Text = "Run";
            this.bn_run.UseVisualStyleBackColor = true;
            this.bn_run.Click += new System.EventHandler(this.bn_run_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(515, 422);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(136, 17);
            this.label19.TabIndex = 23;
            this.label19.Text = "Subsidiary Number: ";
            // 
            // tb_sbs_no
            // 
            this.tb_sbs_no.Location = new System.Drawing.Point(657, 420);
            this.tb_sbs_no.Name = "tb_sbs_no";
            this.tb_sbs_no.Size = new System.Drawing.Size(168, 22);
            this.tb_sbs_no.TabIndex = 22;
            this.tb_sbs_no.Tag = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(928, 454);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.tb_sbs_no);
            this.Controls.Add(this.bn_run);
            this.Controls.Add(this.tabs);
            this.Name = "Form1";
            this.Text = "Upload Sales Application V1.0.0.21";
            this.tabs.ResumeLayout(false);
            this.tab_main.ResumeLayout(false);
            this.tab_main.PerformLayout();
            this.tab_server_settings.ResumeLayout(false);
            this.group_ecm.ResumeLayout(false);
            this.group_ecm.PerformLayout();
            this.group_server.ResumeLayout(false);
            this.group_server.PerformLayout();
            this.tab_excel_setting.ResumeLayout(false);
            this.group_excel.ResumeLayout(false);
            this.group_excel.PerformLayout();
            this.tab_subsidiary.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.data_sbs)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage tab_main;
        private System.Windows.Forms.TabPage tab_server_settings;
        private System.Windows.Forms.RichTextBox tb_msg;
        private System.Windows.Forms.GroupBox group_server;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tb_host;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tb_password;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tb_user_id;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tb_service_name;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tb_port;
        private System.Windows.Forms.TabPage tab_excel_setting;
        private System.Windows.Forms.GroupBox group_excel;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tb_sku;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tb_receipt_no;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tb_store_no;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tb_upc;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tb_worksheet_no;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox tb_total_price;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox tb_qty;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox tb_size;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button bn_run;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.GroupBox group_ecm;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox tb_ecm_parameters;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox tb_ecm_location;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox cb_ecm;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox tb_sbs_no;
        private System.Windows.Forms.TabPage tab_subsidiary;
        private System.Windows.Forms.DataGridView data_sbs;
        private System.Windows.Forms.Button bn_save_sbs_settings;
    }
}

