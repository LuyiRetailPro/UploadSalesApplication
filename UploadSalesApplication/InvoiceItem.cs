using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UploadSalesApplication
{
    class InvoiceItem
    {
        public string alu { get; set; }
        public int qty { get; set; }
        public decimal total_price { get; set; }
        public decimal unit_price { get; set; }
        public string upc { get; set; }
        public InvoiceItem(string alu, string upc, int qty, decimal total_price)
        {
            if (string.IsNullOrEmpty(alu))
            {
                this.alu = "";
            }
            else
            {
                this.alu = alu;
            }
            if (string.IsNullOrEmpty(upc))
            {
                this.upc = "";
            }
            else
            {
                this.upc = upc;
            }
            this.qty = qty;
            this.total_price = total_price;
            if (qty != 0)
            {
                this.unit_price = total_price / qty;
            }
        }
        public decimal getUnitPrice()
        {
            return total_price / qty;
        }

        public decimal getTaxAmount()
        {
            return unit_price - (unit_price / 1.17M);
        }
        public decimal getDiscPct(decimal original_price)
        {
            return ((original_price - unit_price) / original_price * 100);
        }
        public decimal getDiscAmt(decimal original_price)
        {
            return original_price - unit_price;
        }
    }

    class Invoice
    {
        public DateTime created_date { get; set; }
        public string sbs_no { get; set; }
        public string store_no { get; set; }
        public string receipt_no { get; set; }

        public List<InvoiceItem> invoiceItems = new List<InvoiceItem>();

        public decimal getTotalPrice()
        {
            decimal total_price = 0;
            foreach(InvoiceItem item in invoiceItems)
            {
                total_price += item.total_price;
            }
            return total_price;
        }

        public void addInvoiceItem(string alu, string upc, int qty, decimal total_price)
        {
            this.invoiceItems.Add(new InvoiceItem(alu, upc, qty, total_price));
        }

        public Invoice(string store_no, string sbs_no, string receipt_no, DateTime created_date)
        {
            this.store_no = store_no;
            this.sbs_no = sbs_no;
            this.receipt_no = receipt_no;
            this.created_date = created_date;
        }
        public string getDateString()
        {
            return created_date.ToString("yyyy-MM-ddTHH:mm:ss");
        }
    }

    class ItemInfo
    {
        public decimal item_sid { get; set; }
        public decimal price { get; set; }
        public ItemInfo(decimal item_sid, decimal price)
        {
            this.item_sid = item_sid;
            this.price = price;
        }
    }
}
