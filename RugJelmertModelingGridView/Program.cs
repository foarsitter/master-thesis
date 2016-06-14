using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RugJelmertModelingGridView
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GridViewForm(@"C:\RugJelmertModelingOutput\rotterdam\2016-05-06\a9fce6f6\grid-dumps"));
        }
    }
}
