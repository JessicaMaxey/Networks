using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Net;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace Compression_and_Remote_Objects
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            //Get up the ip address
            byte[] ip_address = { 173, 201, 44, 188 };
            int port = 80;
            IPAddress server_IP = new IPAddress(ip_address);


            //get file path from user
            string data_file = "data.txt";

            //Create connection
            TcpClient my_client = new TcpClient();
            my_client.Connect(server_IP, port);


            //open file to be sent that has the xml data
            FileStream data = new FileStream(data_file, FileMode.Open);

            byte[] my_data;
            my_data = new byte[data.Length];

            for (int i = 0; i < data.Length; i++)
            {
                my_data[i] = (byte)data.ReadByte();
            }

            data.Close();

            Stream client_stream = my_client.GetStream();
            client_stream.Write(my_data, 0, my_data.Length);
            label1.Text = "Data sent.... ";
            Thread.Sleep(4000);

            data = new FileStream("response.txt", FileMode.Create);//create another file 
                                                                   //to see the response
            StreamReader sr = new StreamReader(client_stream); textBox1.Text = sr.ReadToEnd();//read the 
                                                                                             //response
            data.Write(Encoding.ASCII.GetBytes(textBox1.Text), 0,
                    Encoding.ASCII.GetByteCount(textBox1.Text));
            data.Close();
        }
    }
}
