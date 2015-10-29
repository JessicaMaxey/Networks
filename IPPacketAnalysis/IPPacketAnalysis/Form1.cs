using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net.Sockets;
using System.Net;

namespace IPPacketAnalysis
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void start_btn_Click(object sender, EventArgs e)
        {
            //IPHostEntry ip_host_info = Dns.GetHostEntry("");
            IPHostEntry ip_host_info = Dns.Resolve(Dns.GetHostName());
            IPAddress ip_address = ip_host_info.AddressList[0];
            IPEndPoint end_point = new IPEndPoint(ip_address, 0);

            Socket com_sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //com_sock.Connect(end_point);
            com_sock.Bind(end_point);

            NetworkStream net_stream = new NetworkStream(com_sock);
            
            StringBuilder derp = new StringBuilder();
            while (true)
            {
                 derp.Append(net_stream.ReadByte());
            }
        }
    }
}
