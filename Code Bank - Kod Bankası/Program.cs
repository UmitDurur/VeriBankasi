using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace VeriBankasi
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static Anaform Anaform;
        [STAThread]
        static void Main()
        {/*
            string path = "en";//VeriBank.resources.dll
            DirectoryInfo di = Directory.CreateDirectory(path);
            di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
            if (File.Exists(@"Languages\" + options.Default.Language + ".dll") && options.Default.Language != "tr-TR")
            { 
                File.Copy(@"Languages\" + options.Default.Language + ".dll", @"en/VeriBank.resources.dll", true);
            }*/

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Anaform = new Anaform(); // THIS IS IMPORTANT
            Application.Run(Anaform);
            //Application.Run(new Anaform());
        }
    }
}
