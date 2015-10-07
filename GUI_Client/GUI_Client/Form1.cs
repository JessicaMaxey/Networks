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
using System.Threading;
using System.Runtime.InteropServices;


namespace GUI_Client
{
    public partial class Form1 : Form
    {
        static StreamWriter swriter = null;
        static StreamReader sreader = null;
        TcpClient client;
        int port = 7;
        bool endthread = false;
        Thread readthread;


        public Form1(String[] args)
        {
            InitializeComponent();

            //doesn't let the user send message until the connection is made
            send_btn.Enabled = false;
            send_btn.BackColor = Color.Black;

            try
            {
                client = new TcpClient();
                string text;


                //Greets the user with some instructions
                text = ("Hello! First, please type in the host IP into the box above. \r\n");
                main_txtbx.Text += text;

                //prompts the user the screen name for this client
                text = "Then, for your first message, please enter screen name. \r\n";
                main_txtbx.Text += text;

                //tells the user how to exit the chat properly 
                text = ("To leave the chat, type \"exit\" to leave the chat. \r\n");
                main_txtbx.Text += text;




            }
            catch (Exception e)
            {
                Console.WriteLine(e + " " + e.StackTrace);
            }

        }

        public void connect_btn_Click(object sender, EventArgs e)
        {
            //Host name comes from command line
            //If no host specified, local machine is host
            String host = server_address_txtbx.Text;
            client.Connect(host, port);

            //lets you use the send button now that the connect has been made
            send_btn.Enabled = true;
            send_btn.BackColor = Color.WhiteSmoke;
            server_address_txtbx.Enabled = false;
            server_address_txtbx.BackColor = Color.Gray;
            connect_btn.Enabled = false;
            connect_btn.BackColor = Color.Black;

            //gets stream between server and client
            NetworkStream networkstream = client.GetStream();

            //creates writer and reader
            swriter = new StreamWriter(networkstream);
            sreader = new StreamReader(networkstream);

            //starts thread to read messages being sent from other clients
            //from the server
            readthread = new Thread(new ThreadStart(Reader));
            readthread.Start();
        }

        public void send_btn_Click(object sender, EventArgs e)
        {
            //gets the message to be sent from the user
            String input = message_txtbx.Text;
            //clears the message box
            message_txtbx.Clear();

            //sends the message to the sever to be sent to 
            //other clients
            swriter.WriteLine(input);
            swriter.Flush();

            if (input == "exit")
            {
                endthread = true;

                //cleans up the threads
                readthread.Abort();
                this.Close();
            }
                 
        }

        private void Reader()
        {

            //while the client is still alive, read for data
            //if client has been closed, the reading will end
            while (endthread == false)
            {
                String returndata = sreader.ReadLine();
                returndata += "\n";
                //using this method makes writing to the 
                //message box thread safe
                this.SetText(returndata);
            }

        }

        delegate void SetTextCallback(string text);

        //this makes writing to the textbox thread safe
        private void SetText (string text)
        {
            if (this.main_txtbx.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.main_txtbx.Text += (text + "\r\n");
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.WindowsShutDown) return;

            // Confirm user wants to close
            switch (MessageBox.Show(this, "Are you sure you want to close?", "Closing", MessageBoxButtons.YesNo))
            {
                case DialogResult.No:
                    e.Cancel = true;
                    break;
                default:
                    {
                        //sends the exit message to the server
                        String input = "exit";
                        swriter.WriteLine(input);
                        swriter.Flush();
                        endthread = true;

                        //cleans up the threads
                        readthread.Abort();
                        //exits
                        break;
                    }

            }

        }

    }
}
