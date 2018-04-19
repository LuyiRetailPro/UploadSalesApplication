using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using System.Collections;

namespace UploadSalesApplication
{
    class DBHandler
    {
        public static string getConnectionString()
        {
            return String.Format("Data Source={0};User Id={1};Password={2};", getListenerString(), Properties.Settings.Default.Userid, Properties.Settings.Default.password);
        }

        public static Dictionary<string, int> loadStores(int sbs_no, List<ErrorField> errorList)
        {
            string connectionString = getConnectionString();
            string sql = "SELECT DISTINCT STORE_NO, UDF1_VALUE FROM STORE_V WHERE SBS_NO =:sbs_no AND UDF1_VALUE IS NOT NULL AND (WEB_STORE=0 OR WEB_STORE IS NULL)";
            Dictionary<string, int> storeDictionary = new Dictionary<string, int>();
            using (OracleConnection cn = new OracleConnection(connectionString))
            {
                OracleCommand cmd = new OracleCommand(sql, cn);
                cmd.Parameters.Add(new OracleParameter("sbs_no", sbs_no));
                cmd.Connection = cn;
                cn.Open();
                OracleDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int store_no = reader.GetInt32(0);
                        string udf_value1 = reader.GetString(1);
                        storeDictionary[udf_value1] = store_no;
                    }
                }
                else
                {
                    errorList.Add(new ErrorField(-1, "getStoreNumbers", "sbs_no:"+sbs_no));
                }
                cn.Close();
            }
            return storeDictionary;
        }
    

        public static string getListenerString()
        {
            return string.Format("(DESCRIPTION = (ADDRESS_LIST=(ADDRESS =(PROTOCOL = TCP)(HOST = {0})(PORT = {1})))(CONNECT_DATA = (SERVICE_NAME = {2})))", Properties.Settings.Default.Host, Properties.Settings.Default.Port, Properties.Settings.Default.Service_name);
        }

        private OracleCommand getCommandForALU(string alu, string sbs_no, OracleConnection cn)
        {

            string sql = "SELECT DISTINCT INVN.ITEM_SID, INVN.ALU, PRICE.PRICE FROM INVN_SBS_V INVN INNER JOIN INVN_SBS_PRICE_V PRICE ON INVN.ITEM_SID = PRICE.ITEM_SID AND PRICE.SBS_NO = INVN.SBS_NO ";
            sql += "WHERE INVN.SBS_NO=:sbs_no AND PRICE.PRICE_LVL=:price_lvl AND PRICE.SBS_NO=1 AND INVN.ALU =:alu";
            OracleCommand cmd = new OracleCommand(sql, cn);
            cmd.Parameters.Add(new OracleParameter("sbs_no", sbs_no));
            cmd.Parameters.Add(new OracleParameter("price_lvl", 1));
            cmd.Parameters.Add(new OracleParameter("alu", alu));

            return cmd;
        }

        private OracleCommand getCommandForUPC(string upc, string sbs_no, OracleConnection cn)
        {

            string sql = "SELECT DISTINCT INVN.ITEM_SID, INVN.ALU, PRICE.PRICE FROM INVN_SBS_V INVN INNER JOIN INVN_SBS_PRICE_V PRICE ON INVN.ITEM_SID = PRICE.ITEM_SID AND PRICE.SBS_NO = INVN.SBS_NO ";
            sql += "WHERE INVN.SBS_NO=:sbs_no AND PRICE.PRICE_LVL=:price_lvl AND PRICE.SBS_NO=1 AND INVN.LOCAL_UPC =:upc";
            OracleCommand cmd = new OracleCommand(sql, cn);
            cmd.Parameters.Add(new OracleParameter("sbs_no", sbs_no));
            cmd.Parameters.Add(new OracleParameter("price_lvl", 1));
            cmd.Parameters.Add(new OracleParameter("upc", upc));

            return cmd;
        }

        

        public void getItemInfoFromKey(string key, string sbs_no, Hashtable items, List<ErrorField> errorList, string errorPath)
        {
            string[] keyArr = key.Split(UploadSalesControl.ALU_DELIMITER);
            string alu = keyArr[0];
            string upc = keyArr[1];
            if (string.IsNullOrEmpty(alu) && string.IsNullOrEmpty(upc))
            {
                errorList.Add(new ErrorField(-1, "ALU", key));
                return;
            }

            string connectionString = getConnectionString();

            using (OracleConnection cn = new OracleConnection(connectionString))
            {
                cn.Open();
                OracleDataReader reader = null;
                try
                {
                    if (!string.IsNullOrEmpty(alu))
                    {
                        OracleCommand cmd = getCommandForALU(alu, sbs_no, cn);
                        reader = cmd.ExecuteReader();
                    }
                    if (reader == null || !reader.HasRows)
                    {
                        if (!string.IsNullOrEmpty(upc))
                        {
                            OracleCommand cmd = getCommandForUPC(upc, sbs_no, cn);
                            reader = cmd.ExecuteReader();
                        }
                    }
                    if (reader == null || !reader.HasRows)
                    {
                        errorList.Add(new ErrorField(-1, "ALU/UPC not found:", key));
                    }
                    else
                    {
                        reader.Read();
                        decimal item_sid = reader.GetDecimal(0);
                        decimal price = reader.GetDecimal(2);
                        items.Add(key, new ItemInfo(item_sid, price));
                    }
                }
                catch (Exception e)
                {
                    LogWriter.writeErrorLog(e, errorPath);
                    errorList.Add(new ErrorField(-1, "Invalid ALU/UPC", key));
                }
                finally
                {
                    cn.Close();
                }
            }
        }

        //SELECT STORE_NO FROM STORE_V WHERE GLOB_STORE_CODE='C00026' AND SBS_NO = 1
        public void getStoreCode(string glob_store_code, string sbs_no, Hashtable stores, List<ErrorField> errorList)
        {
            string connectionString = getConnectionString();
            string sql = "SELECT STORE_NO FROM STORE_V WHERE GLOB_STORE_CODE=:glob_store_code AND SBS_NO =:sbs_no";
            using (OracleConnection cn = new OracleConnection(connectionString))
            {
                OracleCommand cmd = new OracleCommand(sql, cn);
                cmd.Parameters.Add(new OracleParameter("glob_store_code", glob_store_code));
                cmd.Parameters.Add(new OracleParameter("sbs_no", sbs_no));
                cmd.Connection = cn;
                cn.Open();
                OracleDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int store_no = reader.GetInt32(0);
                        stores.Add(glob_store_code, store_no);
                    }
                }
                else
                {
                    errorList.Add(new ErrorField(-1, "global store code", glob_store_code));
                }
                cn.Close();
            }
        }
    }
}
