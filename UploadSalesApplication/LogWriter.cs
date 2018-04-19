using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace UploadSalesApplication
{
    class LogWriter
    {
        static DateTime logDate = DateTime.Now;
        public static void writeErrorLog(List<ErrorField> errorfields, string error_path)
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter(createLogFilePath(error_path));
            file.WriteLine("Invalid fields:");
            foreach (ErrorField errorfield in errorfields)
            {
                file.WriteLine(errorfield.ToString());
            }
            file.Close();
        }
        public static void writeErrorLog(Exception e, string error_path)
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter(createLogFilePath(error_path));
            file.WriteLine("Unexpected error occured");
            file.WriteLine("Error message: " + e.Message);
            file.WriteLine("Stacktrace: " + e.StackTrace);
            file.Close();
        }

        private static string createLogFilePath(string filepath)
        {
            
            string filename = "Error_" + logDate.ToString("yyyy-dd-M--HH-mm-ss") + ".log";
            if (String.IsNullOrEmpty(filepath))
            {
                return System.Environment.SpecialFolder.LocalApplicationData + filename;
            }
            
            filepath += filepath.EndsWith("\\") ? "" : "\\";
            return filepath + filename;
        }
    }
}
