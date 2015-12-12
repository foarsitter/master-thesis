using RugJelmertModelingLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RugJelmertModelingCLI
{
    class Program
    {
        AgentBasedModel abm; 

        [STAThread]
        static void Main(string[] args)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            DialogResult result = ofd.ShowDialog();

            if(result == DialogResult.OK)
            {
                Console.WriteLine(ofd.FileName);
            }

            Console.ReadLine();
        }

        public void something()
        {
            abm = new AgentBasedModel();
            
                
        }
    }
}
