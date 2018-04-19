using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UploadSalesApplication
{
    class UploadSalesConfig
    {
        public string input_folder_path { get; set; }
        public string output_folder_path { get; set; }

        public Hashtable columnTable = new Hashtable();
        private Hashtable items = new Hashtable();

        public int start_row;
        public int worksheet_no;

        public enum COL_NAMES
        {
            col_sbs_no, col_store_no, col_receipt_no, col_sku, col_size, col_qty, col_total_price, col_date
        }
        /*
        public const int INITIAL_ROW = 3;
        public const int WORKSHEET_NO = 1;
        public const int COL_SBS_NO = 12;
        public const int COL_STORE_NO = 4;
        public const int COL_RECEIPT_NO = 5;
        public const int COL_SKU = 6;
        public const int COL_SIZE = 7;
        public const int COL_QTY = 9;
        public const int COL_TOTAL = 10;
        public const int COL_DATE = 2;
        */

        public void loadSettings()
        {
            input_folder_path = Properties.Settings.Default.Input_folder;
            output_folder_path = Properties.Settings.Default.Output_folder;
            start_row = Properties.Settings.Default.start_row;
            worksheet_no = Properties.Settings.Default.s

        }
    }

    class ItemInfo
    {
        public string item_sid { get; set; }
        public string price { get; set; }

        public ItemInfo(string item_sid, string price)
        {
            this.item_sid = item_sid;
            this.price = price;
        }
    }
}
