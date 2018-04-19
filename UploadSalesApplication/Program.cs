using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UploadSalesApplication
{
    class Program 
    {
        [STAThread]
        public static void Main(String[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (args.Length > 0)
            {
                SBSControl sbscontrol = new SBSControl();
                ConsoleInterface consoleIntf = new ConsoleInterface();
                UploadSalesControl control = new UploadSalesControl(consoleIntf, sbscontrol);
                control.beginProcess(args[0]);
                //Console.ReadKey();
                /*
                while (!consoleIntf.completed)
                {
                    System.Threading.Thread.Sleep(2000);
                }
                */
                consoleIntf.workDone();
            }
            else {
                Application.Run(new Form1());
            }
        }

        class ConsoleInterface : UploadSalesInterface
        {
            public bool completed = false;
            List<string> messages = new List<string>();
            public void updateMessage(string message, bool keep)
            {
                if (keep)
                {
                    messages.Add(message);
                    Console.WriteLine(message);
                }
            }

            public void workDone()
            {
                updateMessage("Task ended.", true);
                if (!Directory.Exists("Log"))
                {
                    Directory.CreateDirectory("Log");
                }
                string filename = "Log\\Sales Upload " + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + ".log";
                System.IO.StreamWriter file = new System.IO.StreamWriter(filename);
                foreach (string message in messages)
                {
                    file.WriteLine(message);
                }
                file.Close();
            }
        }
    }
}
