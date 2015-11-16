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
using System.Net.Sockets;

namespace Compression_Server
{
    public partial class Form1 : Form
    {
        StreamReader sreader = null;
        StreamWriter swriter = null;
        TcpClient client = null;
        
        public Form1(TcpClient tcpc) { client = tcpc; }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Listen();
        }

        private void Listen()
        {
            while (true)
            {
                HttpListener listener = new HttpListener();

                listener.Prefixes.Add("http://127.0.0.1:8080/");
                listener.Start();
                textBox1.Text = "listening.../n/r";

                HttpListenerContext context = listener.GetContext();

                HttpListenerRequest request = context.Request;
                textBox1.Text = request.ToString();

                HttpListenerResponse response = context.Response;
                textBox1.Text = response.ToString();

                string responsestring = "received";
                byte[] buffer = Encoding.UTF8.GetBytes(responsestring);
                response.ContentLength64 = buffer.Length;

                Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                output.Flush();
                output.Close();

                //byte[] byte_ipaddress = { 127, 0, 0, 1 };

                //IPAddress ip_address = new IPAddress(byte_ipaddress);
                //int port = 8080;

                //TcpListener server = new TcpListener(ip_address, port);

                //server.Start();

                ////loops listening for clients
                //for (;;)
                //{
                //    //gets the clients, will wait here listening for clients
                //    Form1 es = new Form1(server.AcceptTcpClient());

                //    //stores the es to keep track of all the clients instances
                //    Tuple<Form1, string> new_es = new Tuple<Form1, string>(es, "");

                //    //creates threads
                //    Thread serverThread = new Thread(new ThreadStart(es.Messaging));
                //    serverThread.Start();
                //}

            }

        }

        public void Messaging ()
        {
            textBox1.Text =  ("Connected... Getting screen name... ");

            sreader = new StreamReader(client.GetStream());
            String input = sreader.ReadLine();

            textBox1.Text = input;
            
            swriter = new StreamWriter(client.GetStream());
            swriter.WriteLine("got it.");
        }

    }
}
