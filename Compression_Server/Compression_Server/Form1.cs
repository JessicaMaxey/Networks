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

namespace Compression_Server
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            HttpListener listener = new HttpListener();

            Listen(listener);

            listener.Stop();

        }

        private void Run()
        {
            //Listen();
        }

        private void Listen(HttpListener listener)
        {
            while (true)
            {
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
            }

        }

    }
}
