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
        SOAP soap_message;
        string ip_address = "";
        string port = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private async void SendMessage ()
        {
            //Get file and create xml SOAP message to be sent
            soap_message = new SOAP(file_txtbx.Text);

            //create http client
            //HttpClient client = new HttpClient();

            ip_address = ip_address_txtbx.Text;
            port = port_txtbx.Text;

            //create connection address
            Uri uri = new Uri(@"http://" + ip_address + ":" + port + @"/");

            //create connection
            var client = WebRequest.Create(uri);
            //client.Timeout = 300000;
            client.Method = "POST";
            client.ContentType = "text/xml;charset=UTF-8";

            //send SOAP message
            using (var writer = new StreamWriter(client.GetRequestStream()))
            {
                writer.WriteLine(soap_message.GetSoapMessage.ToString());
                writer.Close();
            }


            //get response 
            using (var response = client.GetResponse())
            {
                client.GetRequestStream().Close();
                if (response != null)
                {
                    using (var answerReader =
                                new StreamReader(response.GetResponseStream()))
                    {
                        var readString = answerReader.ReadToEnd();
                        textBox1.Text = readString.ToString();
                    }
                }
            }

            ////sets the contents of the message
            //var content = new ByteArrayContent(soap_message.GetSoapMessage);

            //var httpResponse = await client.PostAsync(uri.OriginalString, content);
            //textBox1.Text = await httpResponse.Content.ReadAsStringAsync();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void send_btn_Click(object sender, EventArgs e)
        {
            SendMessage();
        }

        private void file_txtbx_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
