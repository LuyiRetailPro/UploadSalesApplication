using Microsoft.Office.Interop.Excel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using UploadSalesApplication.Properties;

namespace UploadSalesApplication
{
    class UploadSalesControl
    {
        public UploadSalesInterface uploadSalesInterface;
        public const char ALU_DELIMITER = ' ';
        public Hashtable items;
        public List<ErrorField> errorList = new List<ErrorField>();
        public Hashtable invoices;
        public DBHandler dbHandler = new DBHandler();
        public SBSControl sbsControl;
        Queue<int> sbs_nos = new Queue<int>();
        public UploadSalesControl(UploadSalesInterface uploadSalesInterface, SBSControl sbsControl)
        {
            this.uploadSalesInterface = uploadSalesInterface;
            this.sbsControl = sbsControl;
        }

        public void beginProcess(string _input)
        {
            sbs_nos.Clear();
            string[] input = _input.Split(',');
            foreach (string s in input)
            {
                try
                {
                    int sbs_no = Int32.Parse(s);
                    sbs_nos.Enqueue(sbs_no);
                } catch (Exception)
                {
                    //do nothing
                }
            }
            if (sbs_nos.Count > 0)
            {
                loadSBSandPrepareSalesUpload(sbs_nos.Dequeue());
            }
            else
            {
                uploadSalesInterface.workDone();
            }
        }

        public void loadSBSandPrepareSalesUpload(int sbs_no)
        {
            SbsSetting sbsSetting = sbsControl.getSbsSettings(sbs_no);
            bool valid = true;
            if (sbsSetting == null)
            {
                uploadSalesInterface.updateMessage("Subsidiary " + sbs_no + " settings not found. Aborted\r\n", true);
                valid = false;
            }
            else if (sbsSetting.folderNotSet())
            {
                uploadSalesInterface.updateMessage("Folder paths are not set properly. Run aborted.", true);
                valid = false;
            }

            if (valid)
            {
                uploadSalesInterface.updateMessage("Subsidiary " + sbs_no + " settings found. Initializing.\r\n", true);
                uploadSalesInterface.updateMessage("Copying all files to ftp archive folder", true);
                copyAllExcelFiles(sbsSetting.ftp_input, sbsSetting.ftp_archive);
                uploadSalesInterface.updateMessage("Copying all files to input folder", true);
                copyAllExcelFiles(sbsSetting.ftp_input, sbsSetting.input);
                deleteAllExcelFiles(sbsSetting.ftp_input);
                errorList.Clear();
                invoices = null;
                items = null;
                if (uploadSalesInterface is Form1)
                {
                    BackgroundWorker bw = new BackgroundWorker();
                    bw.DoWork += new DoWorkEventHandler(bw_doWork_processSalesUpload);
                    bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_processSalesUpload_completed);
                    bw.RunWorkerAsync(sbsSetting);
                }
                else
                {
                    processSalesUploadBySBS(sbsSetting);
                    if (sbs_nos.Count > 0)
                    {
                        loadSBSandPrepareSalesUpload(sbs_nos.Dequeue());
                    }
                }
            }
            else
            {
                if (sbs_nos.Count > 0)
                {
                    loadSBSandPrepareSalesUpload(sbs_nos.Dequeue());
                }
                else
                {
                    uploadSalesInterface.workDone();
                }
            }
        }

        public static String[] getExcelFiles(string source_folder)
        {
            var files = Directory.EnumerateFiles(source_folder, "*.*", SearchOption.TopDirectoryOnly)
            .Where(s => s.EndsWith(".xls") || s.EndsWith(".xlsx"));
            return files.ToArray();
        }

        private void copyAllExcelFiles(string source_folder, string destination_folder)
        {
            String[] files = getExcelFiles(source_folder);
            foreach (string input_file_path in files)
            {
                if (!input_file_path.StartsWith("~$"))
                {
                    string file = Path.GetFileName(input_file_path);
                    string archivePath = Path.Combine(destination_folder, file);
                    try
                    {
                        System.IO.File.Copy(input_file_path, archivePath);
                    }
                    catch (Exception)
                    {
                        //file already exists in the other folder, do nothing
                    }
                }
            }
        }

        private void deleteAllExcelFiles(string source_folder)
        {
            String[] files = getExcelFiles(source_folder);
            foreach (string input_file_path in files)
            {
                if (!input_file_path.StartsWith("~$"))
                {
                    try
                    {
                        File.Delete(input_file_path);
                    }
                    catch (Exception e)
                    {
                        uploadSalesInterface.updateMessage("Unable to delete file in " + input_file_path + ": " + e.Message, true);
                    }
                }
            }
        }

        private void processSalesUploadBySBS(SbsSetting sbsSetting)
        {
            bool success = true;
            //  BackgroundWorker worker = sender as BackgroundWorker;
            String[] files = getExcelFiles(sbsSetting.input);
            if (files.Length == 0)
            {
                uploadSalesInterface.updateMessage("No file in input folder " + sbsSetting.ftp_input, true);
            }
            foreach (string input_file_path in files)
            {
                if (input_file_path.Contains("~$")) //skip annoying hidden excel files that are open (thanks microsoft!)
                {
                    continue;
                }
                try
                {
                    object[,] data = loadExcelData(input_file_path);

                   // int dataCount = data.Length;
                   // uploadSalesInterface.updateMessage("dataCount Count : " + dataCount, true);

                    if (data == null)
                    {
                        continue;
                    }

                    HashSet<string> invoiceItemKey = loadData(data, sbsSetting.sbs_no.ToString());
                    data = null; //release from memory

                    // int invoiceItemKeyCount = invoiceItemKey.Count;
                    //uploadSalesInterface.updateMessage("InvoiceitemKey Count : " + invoiceItemKeyCount, true);

                    if (errorList.Count > 0)
                    {
                        uploadSalesInterface.updateMessage("Error: invalid fields in excel file. Run aborted.\n", true);
                        LogWriter.writeErrorLog(errorList, sbsSetting.error);
                        return;
                    }

                    uploadSalesInterface.updateMessage("Fetching item details from database\n", true);

                    items = new Hashtable();
                    foreach (string key in invoiceItemKey)
                    {
                        dbHandler.getItemInfoFromKey(key, "1", items, errorList, sbsSetting.error); //SBS_NO is defaulted to 1 as it should refer to price from the master subsidiary
                        string message = string.Format("Fetching item details from database\nTotal ALU: {0} \t Found: {1} \t Error: {2}", invoiceItemKey.Count, items.Count, errorList.Count);
                        uploadSalesInterface.updateMessage(message, false);
                    }
                  //  int i = invoices.Count;
                   // uploadSalesInterface.updateMessage("Invoice Count : " + i, true);

                    if (errorList.Count > 0)
                    {
                        uploadSalesInterface.updateMessage("Error: invalid ALU/UPC in excel file. Run aborted.\n", true);
                        LogWriter.writeErrorLog(errorList, sbsSetting.error);
                        return;
                    }
                    uploadSalesInterface.updateMessage("Writing to Invoice.xml", true);

                    CreateInvoiceXML createInvoiceXML = new CreateInvoiceXML();
                    foreach (DictionaryEntry pair in invoices)
                    {
                        Invoice invoice = (Invoice)pair.Value;
                        createInvoiceXML.createInvoice(invoice, items);
                    }
                    createInvoiceXML.printToFile(sbsSetting.output);
                }
                catch (Exception ex)
                {
                    uploadSalesInterface.updateMessage("Unexpected error occured: " + ex.Message, true);
                    LogWriter.writeErrorLog(ex, sbsSetting.error);
                    success = false;
                }

                finally
                {
                    //archive files
                    if (success && errorList.Count == 0)
                    {
                        archiveOutputFiles(input_file_path, sbsSetting);
                    }
                    if (success)
                    {
                        runECM(sbsSetting);
                    }
                }
            }
        }

        private void archiveOutputFiles(string input_file_path, SbsSetting sbsSetting)
        {
            string file = Path.GetFileName(input_file_path);
            string archivePath = Path.Combine(sbsSetting.archive, file);
            try
            {
                System.IO.File.Copy(input_file_path, archivePath);
                uploadSalesInterface.updateMessage(Path.GetFileName(input_file_path) + " archived.", true);
            }
            catch (Exception ex)
            {
                uploadSalesInterface.updateMessage("Unable to copy file to archive folder:  " + Path.GetFileName(input_file_path) + ":" + ex.Message, true);
            }
            try
            {
                System.IO.File.Delete(input_file_path);
                uploadSalesInterface.updateMessage(Path.GetFileName(input_file_path) + " removed from input folder.", true);
            }
            catch (Exception ex)
            {
                uploadSalesInterface.updateMessage("Unable to delete file in input folder:  " + Path.GetFileName(input_file_path) + ":" + ex.Message, true);
            }
        }

        private void bw_doWork_processSalesUpload(object sender, DoWorkEventArgs e)
        {
            SbsSetting sbsSetting = (SbsSetting)e.Argument;
            processSalesUploadBySBS(sbsSetting);
        }

        private object[,] loadExcelData(string input_file_path)
        {
            try
            {
                uploadSalesInterface.updateMessage("Excel file found: " + input_file_path + ". Loading and checking excel data.", true);
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                Workbook wb = excel.Workbooks.Open(input_file_path);
                Worksheet ws = wb.Worksheets[Properties.Settings.Default.Worksheet_no];
                object[,] data = ws.UsedRange.Value2;

                wb.Close(false, Missing.Value, Missing.Value);
                excel.Quit();
                return data;
            }
            catch (Exception)
            {
                MessageBox.Show("Error opening excel. Please make sure the input folder path is correct, and the excel file is in .xlsx format");
                return null; ;
            }
        }

        private void runECM(SbsSetting sbsSetting)
        {
            if (Properties.Settings.Default.ecm_use_ecm){
                try
                {
                    String command = Properties.Settings.Default.ecm_parameters;
                    if (!string.IsNullOrEmpty(sbsSetting.stid))
                    {
                        command += " STID:" + sbsSetting.stid;
                    }
                    ProcessStartInfo cmdsi = new ProcessStartInfo(Properties.Settings.Default.ecm_file_path);
                    cmdsi.Arguments = command;
                    Process cmd = Process.Start(cmdsi);
                    cmd.WaitForExit();
                }
                catch (Exception ex)
                {
                    uploadSalesInterface.updateMessage("Unexpected error occured when running ECM: " + ex.Message, true);
                    LogWriter.writeErrorLog(ex, sbsSetting.error);
                }

            }
        }
    
        private HashSet<string> loadData(object[,] data, string sbs_no)
        {
            Dictionary<string, int> stores = DBHandler.loadStores(Convert.ToInt32(sbs_no), errorList);
            if (stores.Count == 0)
            {
                errorList.Add(new ErrorField(-1, "Store UDFs not set for ", "subsidiary " + sbs_no));
            }
            HashSet<string> aluList = new HashSet<string>();
            invoices = new Hashtable();
            for (int i = Properties.Settings.Default.start_row; i <= data.GetLength(0); i++)
            {
                if (checkNullValues(data, i))
                {
                    if (addInvoiceItem(data, i, stores, sbs_no))
                    {
                        string alu = getInvoiceItemKey(data, i);
                        aluList.Add(alu);
                    }
                }
            }
            return aluList;
        }

        /*
            validates that there are no null fields in required data in the row.
            returns false if there are empty / null fields.
        */
        private bool checkNullValues(object[,] data, int row)
        {
            List<string> nullPropertyList = new List<string>();
            foreach (SettingsProperty currentProperty in Properties.Settings.Default.Properties)
            {
             
                if (currentProperty.Name.Contains("col_"))
                {
                    int propValue = (int)Properties.Settings.Default[currentProperty.Name];
                    if (data[row, propValue] == null)
                    {
                        nullPropertyList.Add(currentProperty.Name);
                    }
                }
            }
            if (nullPropertyList.Count > 0)
            {
                if (nullPropertyList.Count < 6)
                {
                    foreach (string propertyName in nullPropertyList)
                    {
                        errorList.Add(new ErrorField(row, propertyName, " is empty field"));
                    }
                }
                return false;
            }
            return true;
        }

        public string getALUString(object[,] data, int row)
        {
            string alu = data[row, Settings.Default.alu] + "-" + data[row, Settings.Default.col_size];
            alu = alu.Replace(" ", "");
            if (alu.Length <= 1)
            {
                return "";
            }
            return alu;
        }

        public string getInvoiceItemKey(object[,] data, int row)
        {
            string alu = getALUString(data, row);
            return alu + ALU_DELIMITER + data[row, Settings.Default.upc];
        }

        private bool addInvoiceItem(object[,] data, int row, Dictionary<string, int> storeDictionary, string sbs_no)
        {
            SbsSetting sbsSetting = sbsControl.getSbsSettings(int.Parse(sbs_no));

            bool valid = true;

            string alu = getALUString(data, row);
            string upc = data[row, Properties.Settings.Default.upc] + "";
            string store_no = data[row, Properties.Settings.Default.col_store_no].ToString();
            string receipt_no = data[row, Properties.Settings.Default.col_receipt_no].ToString();

            /*Check col_qty*/
            int qty;
            if (!Int32.TryParse(data[row, Properties.Settings.Default.col_qty].ToString(), out qty))
            {
                errorList.Add(new ErrorField(row, "Qty", data[row, Properties.Settings.Default.col_qty].ToString()));
                valid = false;
            }
            if (qty == 0)
            {
                valid = false;
            }

            /*Check col_total_price*/
            decimal total;
            if (!Decimal.TryParse(data[row, Properties.Settings.Default.col_total_price].ToString(), out total))
            {
                errorList.Add(new ErrorField(row, "Total Price", data[row, Properties.Settings.Default.col_total_price].ToString()));
                valid = false;
            }

            /*Check col_date*/
            double date_double;
            DateTime date = System.DateTime.Now;
            if (!Double.TryParse(data[row, Properties.Settings.Default.col_date].ToString(), out date_double))
            {
                try
                {
                    date = DateTime.Parse(data[row, Properties.Settings.Default.col_date].ToString());
                }
                catch (Exception)
                {
                    errorList.Add(new ErrorField(row, "Date", data[row, Properties.Settings.Default.col_date].ToString()));
                    valid = false;
                }
           }          
            else
            {
                date = DateTime.FromOADate(date_double);
                // uploadSalesInterface.updateMessage("date_double : " + date, true);

                if (date > System.DateTime.Now)
                {
                    errorList.Add(new ErrorField(row, "Date", "" + date));
                    valid = false;
                }
            }

            /*Check col_store_no*/
            if (storeDictionary.ContainsKey(store_no))
            {
                store_no = storeDictionary[store_no] + "";
            }
            else
            {
                errorList.Add(new ErrorField(row, "Store", data[row, Properties.Settings.Default.col_store_no].ToString()));
                valid = false;
            }

            /*All columns are valid, proceed to add Invoice Item*/
            if (valid)
            {
                addInvoiceItem(alu, upc, sbs_no, store_no, receipt_no, qty, total, date);
            }

            return valid;
        }

        private void addInvoiceItem(string alu, string upc, string sbs_no, string store_no, string receipt_no, int qty, decimal total, DateTime date)
        {
            string key = date.ToShortDateString()+"-_-"+ store_no + "-_-" + receipt_no;
            if (invoices.ContainsKey(key))
            {
                Invoice invoice = (Invoice)invoices[key];
                invoice.addInvoiceItem(alu, upc, qty, total);
            }
            else
            {
                Invoice invoice = new Invoice(store_no, sbs_no, receipt_no, date);
                invoice.addInvoiceItem(alu, upc, qty, total);
                invoices.Add(key, invoice);
            }
        }

        private void bw_processSalesUpload_completed(object sender, RunWorkerCompletedEventArgs e)
        {
            if (sbs_nos.Count > 0)
            {
                loadSBSandPrepareSalesUpload(sbs_nos.Dequeue());
            }
            else
            {
                uploadSalesInterface.workDone();
            }
        }

    }
}
