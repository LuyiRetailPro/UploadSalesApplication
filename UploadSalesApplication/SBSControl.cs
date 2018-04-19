using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UploadSalesApplication
{
    class SBSControl
    {
        public const string SETTINGSFILENAME = "SBS_SETTINGS.XML";
        public const string TABLE_NAME = "SBS_SETTINGS";
        public enum COL_NAMES
        {
            NAME = 0,
            SBS = 1,
            FTP_INPUT = 2,
            FTP_ARCHIVE = 3,
            INPUT = 4,
            OUTPUT = 5,
            ARCHIVE = 6,
            ERROR = 7,
            STID = 8,
        }


        public SbsSetting getSbsSettings(int sbs_no)
        {
            foreach (DataRow row in data_settings.Rows)
            {

                if (row[(int)COL_NAMES.SBS].ToString().Equals(sbs_no.ToString()))
                {
                    SbsSetting setting = new SbsSetting(sbs_no);
                    setting.input = row[(int)COL_NAMES.INPUT].ToString();
                    setting.output = row[(int)COL_NAMES.OUTPUT].ToString();
                    setting.archive = row[(int)COL_NAMES.ARCHIVE].ToString();
                    setting.error = row[(int)COL_NAMES.ERROR].ToString();
                    setting.stid = row[(int)COL_NAMES.STID].ToString();
                    setting.name = row[(int)COL_NAMES.NAME].ToString();
                    setting.ftp_input = row[(int)COL_NAMES.FTP_INPUT].ToString();
                    setting.ftp_archive = row[(int)COL_NAMES.FTP_ARCHIVE].ToString();
                    return setting;
                }
            }
            return null;
        }

        public DataTable data_settings;
        public SBSControl()
        {
            data_settings = new DataTable(TABLE_NAME);
            data_settings.Columns.Add("NAME");
            data_settings.Columns.Add("SBS");
            data_settings.Columns.Add("FTP_INPUT");
            data_settings.Columns.Add("FTP_ARCHIVE");
            data_settings.Columns.Add("INPUT");
            data_settings.Columns.Add("OUTPUT");
            data_settings.Columns.Add("ARCHIVE");
            data_settings.Columns.Add("ERROR");
            data_settings.Columns.Add("STID");
            loadDataTable();
        }
        
        public void loadDataTable()
        {
            if (!File.Exists(SETTINGSFILENAME))
            {
                data_settings.WriteXml(SETTINGSFILENAME);
            }
            data_settings.ReadXml(SETTINGSFILENAME);
        }

        public void save(DataTable dt)
        {
            data_settings = dt;
            data_settings.WriteXml(SETTINGSFILENAME);
        }
    }

    class SbsSetting
    {
        public int sbs_no { get; set; }
        public string input { get; set; }
        public string output { get; set; }
        public string error { get; set; }
        public string stid { get; set; }
        public string archive { get; set; }
        public string name { get; set; }
        public string ftp_input { get; set; }
        public string ftp_archive { get; set; }
        public SbsSetting(int sbs_no)
        {
            this.sbs_no = sbs_no;
        }

        public bool folderNotSet()
        {
            return (
                string.IsNullOrEmpty(input)
                || string.IsNullOrEmpty(output)
                || string.IsNullOrEmpty(archive)
                || string.IsNullOrEmpty(error)
                || string.IsNullOrEmpty(ftp_input)
                || string.IsNullOrEmpty(ftp_archive)
                );
        }
        
        public override string ToString()
        {
            return string.Format("{0};{1};{2};{3};{4};{5}", sbs_no, input, output, error, stid, archive);
        }
    }
}
