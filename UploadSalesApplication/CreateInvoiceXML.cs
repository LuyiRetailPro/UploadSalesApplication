using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;

namespace UploadSalesApplication
{
    class CreateInvoiceXML
    {
        public const int MAX_RECEIPT_SIZE = 5;
        public XmlDocument doc = new XmlDocument();


        private XmlElement e_invoices;

        public CreateInvoiceXML()
        {
            initialize();
        }

        public void initialize()
        {
            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement root = doc.DocumentElement;
            doc.InsertBefore(xmlDeclaration, root);

            XmlElement e_document = doc.CreateElement(string.Empty, "DOCUMENT", string.Empty);
            doc.AppendChild(e_document);

            e_invoices = doc.CreateElement(string.Empty, "INVOICES", string.Empty);
            e_document.AppendChild(e_invoices);
        }

        public void createInvoice(Invoice invoice, Hashtable items)
        {
            /*
            <INVOICE invc_sid="011" sbs_no="101" store_no="1" note="" 
            invc_type="0" status="0" proc_status="0"  orig_store_no="1" orig_station="" use_vat="1" 
            vat_options="0"  created_date="2016-12-19T15:42:43" modified_date="2016-12-19T15:42:43+08:00"
            post_date="2016-12-19T15:42:43" audited="0" cms_post_date="2016-12-19T15:42:43" held="0" controller="1" 
            orig_controller="1" activity_perc="100"  detax="0" empl_sbs_no="1" empl_name="SYSADMIN" 
            clerk_sbs_no="1" clerk_name="SYSADMIN" createdby_sbs_no="1" createdby_empl_name="SYSADMIN" modifiedby_sbs_no="1" 
            modifiedby_empl_name="SYSADMIN">
            eft_invc_no="120" so_no="" so_sid=""
            */
            XmlElement e_invoice = doc.CreateElement(string.Empty, "INVOICE", string.Empty);
            e_invoice.SetAttribute("invc_sid", generate_SID(invoice));
            e_invoice.SetAttribute("sbs_no", invoice.sbs_no);
            e_invoice.SetAttribute("store_no", invoice.store_no);
            e_invoice.SetAttribute("note", invoice.receipt_no);
            e_invoice.SetAttribute("invc_type", "0");
            e_invoice.SetAttribute("status", "0");
            e_invoice.SetAttribute("proc_status", "0");
            e_invoice.SetAttribute("orig_store_no", invoice.store_no);
            e_invoice.SetAttribute("orig_station", "");
            e_invoice.SetAttribute("use_vat", "1");
            e_invoice.SetAttribute("vat_options", "0");
            e_invoice.SetAttribute("created_date", invoice.getDateString());
            e_invoice.SetAttribute("modified_date", invoice.getDateString());
            e_invoice.SetAttribute("post_date", invoice.getDateString());
            e_invoice.SetAttribute("audited", "0");
            e_invoice.SetAttribute("cms_post_date", invoice.getDateString());
            e_invoice.SetAttribute("held", "0");
            e_invoice.SetAttribute("controller", "1");
            e_invoice.SetAttribute("orig_controller", "1");
            e_invoice.SetAttribute("activity_perc", "100");
            e_invoice.SetAttribute("detax", "0");
            e_invoice.SetAttribute("empl_sbs_no", "1");
            e_invoice.SetAttribute("empl_name", "SYSADMIN");
            e_invoice.SetAttribute("clerk_sbs_no", "1");
            e_invoice.SetAttribute("clerk_name", "SYSADMIN");
            e_invoice.SetAttribute("createdby_sbs_no", "1");
            e_invoice.SetAttribute("createdby_empl_name", "SYSADMIN");
            e_invoice.SetAttribute("modifiedby_sbs_no", "1");
            e_invoice.SetAttribute("modifiedby_empl_name", "SYSADMIN");
            e_invoices.AppendChild(e_invoice);

            addDefaultEmptyTag(e_invoice, "CUSTOMER");
            addDefaultEmptyTag(e_invoice, "SHIPTO_CUSTOMER");

            addTenderToInvoice(e_invoice, invoice.getTotalPrice());

            addInvcItems(e_invoice, invoice, items);
        }

        private void addDefaultEmptyTag(XmlElement e_invoice, string tagName)
        {
            XmlElement emptyTag = doc.CreateElement(string.Empty, tagName, string.Empty);
            e_invoice.AppendChild(emptyTag);
        }

        private void addTenderToInvoice(XmlElement e_invoice, decimal total)
        {
            XmlElement e_tenders = doc.CreateElement(string.Empty, "INVC_TENDERS", string.Empty);
            e_invoice.AppendChild(e_tenders);

            XmlElement e_tender = doc.CreateElement(string.Empty, "INVC_TENDER", string.Empty);
            e_tenders.AppendChild(e_tender);


            e_tender.SetAttribute("tender_type", "0");
            e_tender.SetAttribute("tender_no", "1");
            if (total > 0)
            {
                e_tender.SetAttribute("taken", total.ToString("0.00"));
                e_tender.SetAttribute("given", "0");
            }
            else
            {
                e_tender.SetAttribute("taken", "0");
                e_tender.SetAttribute("given", (total*-1).ToString("0.00"));
            }
                
            e_tender.SetAttribute("amt", total.ToString("0.00"));
            e_tender.SetAttribute("crd_normal_sale", "1");
            e_tender.SetAttribute("crd_present", "0");
            e_tender.SetAttribute("avs_code", "0");
            e_tender.SetAttribute("chk_type", "0");
            e_tender.SetAttribute("tender_state", "0");
            e_tender.SetAttribute("failure_msg", "");
            e_tender.SetAttribute("orig_currency_name", "CNY");
            e_tender.SetAttribute("cent_commit_txn", "0");
            e_tender.SetAttribute("currency_name", "CNY");
        }

        private void addInvcItems(XmlElement e_invoice, Invoice invoice, Hashtable items)
        {
            XmlElement e_invcItems = doc.CreateElement(string.Empty, "INVC_ITEMS", string.Empty);
            e_invoice.AppendChild(e_invcItems);

            int line_item_no = 1;
            /*
            <INVC_ITEM item_pos="1" item_sid="10000048389" qty="1" orig_price="230" orig_tax_amt="33.4188" price="230" disc_perc="9.157" disc_amt="10 
            tax_code="0" tax_perc="17" tax_amt="33.4188" price_lvl="1"  activity_perc="100" tender_state="0" cent_commit_txn="0" price_flag="0" 
            force_orig_tax="0" empl_sbs_no="1" empl_name="SYSADMIN" >
	        <INVN_BASE_ITEM item_sid="10000048389" flag="0" ext_flag="0"/>
             */
            foreach (InvoiceItem item in invoice.invoiceItems)
            {
                ItemInfo itemInfo = (ItemInfo)items[item.alu+UploadSalesControl.ALU_DELIMITER+item.upc];
                decimal price = itemInfo.price;
                decimal item_sid = itemInfo.item_sid;

                XmlElement e_invcItem = doc.CreateElement(string.Empty, "INVC_ITEM", string.Empty);
                e_invcItems.AppendChild(e_invcItem);

                e_invcItem.SetAttribute("item_pos", line_item_no.ToString());
                e_invcItem.SetAttribute("item_sid", item_sid.ToString());
                e_invcItem.SetAttribute("qty", item.qty.ToString());
                e_invcItem.SetAttribute("orig_price", price.ToString("0.00"));
                e_invcItem.SetAttribute("orig_tax_amt", item.getTaxAmount().ToString("0.0000"));
                e_invcItem.SetAttribute("price", item.getUnitPrice().ToString("0.00"));
                e_invcItem.SetAttribute("tax_code", "0");
                e_invcItem.SetAttribute("tax_perc", "17");
                e_invcItem.SetAttribute("tax_amt", item.getTaxAmount().ToString("0.0000"));
                e_invcItem.SetAttribute("price_lvl", "1");
                e_invcItem.SetAttribute("activity_perc", "100");
                e_invcItem.SetAttribute("tender_state", "0");
                e_invcItem.SetAttribute("cent_commit_txn", "0");
                e_invcItem.SetAttribute("price_flag", "0");
                e_invcItem.SetAttribute("force_orig_tax", "0");
                e_invcItem.SetAttribute("empl_sbs_no", "1");
                e_invcItem.SetAttribute("empl_name", "SYSADMIN");
               
                XmlElement e_baseItem = doc.CreateElement(string.Empty, "INVN_BASE_ITEM", string.Empty);
                e_invcItem.AppendChild(e_baseItem);
                e_baseItem.SetAttribute("item_sid", item_sid.ToString());
                e_baseItem.SetAttribute("flag", "0");
                e_baseItem.SetAttribute("ext_flag", "0");
                e_invcItem.SetAttribute("kit_flag", "0");
                ++line_item_no;
            }
        }

        public string generate_SID(Invoice invoice)
        {
            string receipt_no = Regex.Replace(invoice.receipt_no, "[^0-9]", ""); //sanitizes the receipt number to remove anything that is not a digit

            string datestring = invoice.created_date.ToString("yyMMdd");
            string s = ""; 
            if (receipt_no.Length >= MAX_RECEIPT_SIZE)
            {
                s = receipt_no.Substring(receipt_no.Length - MAX_RECEIPT_SIZE);
            }
            else
            {
                s = receipt_no.PadLeft(MAX_RECEIPT_SIZE, '0');
            }
            return invoice.sbs_no.PadLeft(3, '0') + invoice.store_no.PadLeft(3, '0') + datestring + s;
        }

        public void printToFile(string output_folder_path)
        {
            int count = 0;
            string[] files = Directory.GetFiles(output_folder_path, "Invoice*.xml");
            List<int> usedValues = new List<int>();
            foreach (string file_path in files)
            {
                string filename = Path.GetFileName(file_path);
                string filenum = Regex.Match(filename, "\\d+").Value;
                if (filenum.Length >0 && filenum.Length <= 3)
                {
                    usedValues.Add(Convert.ToInt32(filenum));
                }
            }
            while (usedValues.Contains(count))
            {
                count++;
            }
            string invc_filename = "Invoice" + count.ToString("000") + ".xml";
            output_folder_path = Path.Combine(output_folder_path, invc_filename);
            doc.Save(output_folder_path);
        }

    }
}
