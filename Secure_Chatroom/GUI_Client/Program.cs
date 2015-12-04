using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;
using System.Text;

namespace GUI_Client
{
    public class Program
    {
        static public Form1 mainform;

        static void Main(String[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(mainform = new Form1(args));

        }
    }
}