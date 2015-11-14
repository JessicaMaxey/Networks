using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Net;
using System.Threading;
using System.IO;


namespace Compression_Client
{
    public partial class Form1 : Form
    {
        HttpClient client;
        SOAP soap_message = new SOAP();
        string ip_address = "";
        int port = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SendMessage();
        }

        private async void SendMessage ()
        {
            //create http client
            HttpClient client = new HttpClient();

            string test = "request";
            ip_address = "127.0.0.1";
            port = 8080;

            //create connection address
            Uri uri = new Uri(@"http://" + ip_address + ":" + port + @"/");

            var content = new StringContent("herpaderpsendamessage");

            var httpResponse = await client.PostAsync(uri.OriginalString, content);
            textBox1.Text = await httpResponse.Content.ReadAsStringAsync();
        }

    }
}
