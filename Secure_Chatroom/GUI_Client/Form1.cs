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
using Microsoft.VisualBasic;


namespace GUI_Client
{
    public partial class Form1 : Form
    {
        bool exitwithoutconnecting = true;
        bool endthread = false;
        string username;

        public Form1(String[] args)
        {
            InitializeComponent();

            //doesn't let the user send message until the connection is made
            send_btn.ForeColor = Color.LightGray;
            send_btn.Enabled = false;

            private_chat_btn.ForeColor = Color.LightGray;
            private_chat_btn.Enabled = false;

            NetworkController.AddListener("", SetText);
            NetworkController.AddListener("_mainchat_", SetText);
            NetworkController.AddListener("_privatechatpopup_", ShowPrivateChatPopUp);
            
            string text;

            //Greets the user with some instructions
            text = ("Hello! First, please type in the host IP into the box above. \r\n");
            main_txtbx.Text += text;

            //prompts the user the screen name for this client
            text = "Then, please enter screen name into the second box above. \r\n";
            main_txtbx.Text += text;

            //tells the user how to exit the chat properly 
            text = ("To leave the chat, type \"exit\" to leave the chat or click the \"X\" in the corner. \r\n");
            main_txtbx.Text += text;
        }

        public void connect_btn_Click(object sender, EventArgs e)
        {
            if (server_address_txtbx.Text.Length <= 15 && server_address_txtbx.Text.Length >= 7)
            {
                if (screenname_txtbx.Text != null && screenname_txtbx.Text != "")
                {
                    try
                    {
                        //Host name comes from command line
                        //If no host specified, local machine is host
                        string host = server_address_txtbx.Text;

                        NetworkController.Initialize(host);
                        NetworkController.SendMessage(screenname_txtbx.Text);
                        username = screenname_txtbx.Text;

                        //lets you use the send button now that the connect has been made
                        send_btn.Enabled = true;
                        send_btn.ForeColor = Color.Black;
                        server_address_txtbx.Enabled = false;
                        connect_btn.Enabled = false;
                        connect_btn.ForeColor = Color.LightGray;
                        screenname_txtbx.Enabled = false;
                        private_chat_btn.ForeColor = Color.Black;
                        private_chat_btn.Enabled = true;
                    }
                    catch (Exception s)
                    {
                        MessageBox.Show(s + " " + s.StackTrace);

                    }
                }
            }
        }

        public void send_btn_Click(object sender, EventArgs e)
        {
            //gets the message to be sent from the user
            String input = message_txtbx.Text;

            NetworkController.SendMessage(input);

            if (input == "exit")
            {
                endthread = true;

                this.Close();
            }

            message_txtbx.Clear();
                 
        }

        /*
        private void Reader()
        {

            //while the client is still alive, read for data
            //if client has been closed, the reading will end
            while (endthread == false)
            {
                String returndata = sreader.ReadLine();

                //this is where need to look for the returning message saying
                //that the user you want to start a private chat with is not
                //there

                if (returndata.Contains("_privatechatpopup_"))
                {
                    //removes flag text, and leaves just the screen name of the 
                    //user who is requesting the private chatroom
                    returndata = returndata.Remove(0, 24);

                    DialogResult dialogresult = MessageBox.Show("User " + returndata + " is requesting a private chatroom with you.", "Private Chatroom request", MessageBoxButtons.YesNo);

                    if (dialogresult == DialogResult.Yes)
                    {
                        string input = "Private Chatroom was accepted.";
                        swriter.WriteLine(input);
                        swriter.Flush();

                        //do something here to make another window pop up
                        MakeSecureChatroomWindow();

                    }
                    else if (dialogresult == DialogResult.No)
                    {
                        string input = "Private Chatroom was declined.";
                        swriter.WriteLine(input);
                        swriter.Flush();
                    }

                }
                else
                {
                    returndata += "\n";
                    //using this method makes writing to the 
                    //message box thread safe
                    this.SetText(returndata);
                }
            }

        }
        */

        delegate bool PrivateChatPopUpCallBack(string text);
        //Thread safe pop up window for private chat
        private bool ShowPrivateChatPopUp(string text)
        {
            if (this.InvokeRequired)
            {
                PrivateChatPopUpCallBack d = new PrivateChatPopUpCallBack(ShowPrivateChatPopUp);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                DialogResult dialogresult = MessageBox.Show("User " + text + " is requesting a private chatroom with you.", "Private Chatroom request", MessageBoxButtons.YesNo);

                if (dialogresult == DialogResult.Yes)
                {
                    string input = "Private Chatroom was accepted.";
                    NetworkController.SendMessage("_pm" + text + "_" + "_pm" + text + username + "_" + input);

                    //do something here to make another window pop up
                    MakeSecureChatroomWindow(text);

                    NetworkController.SendMessage("_pm" + username + "_" + "_pm" + text + username + "_" + input);
                }
                else if (dialogresult == DialogResult.No)
                {
                    string input = "Private Chatroom was declined.";
                    NetworkController.SendMessage("_pm" + text + "_" + "_pm" + text + username + "_" + input);
                }
            }

            return false;
        }

        delegate bool SetTextCallback(string text);
        //this makes writing to the textbox thread safe
        private bool SetText (string text)
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

            return false;
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
                        string input = "exit";
                        NetworkController.SendMessage(input);
                        break;
                    }
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void MakeSecureChatroomWindow (string other)
        {
            string receiver = "_pm" + other + "_";
            string id = "_pm" + other + username + "_";
            new PrivateChatBox(username, "Private Chat: " + username + " & " + other, receiver, id);
        }

        private void private_chat_btn_Click(object sender, EventArgs e)
        {
            string input = "_createmessageroom_";
            string name = Interaction.InputBox("Please enter the name of the user you would like to start a private chat with: ", "Private Chatroom", "");
            //Get the name of the other user this user wants to start a private chat with
            input += name;


            //Send that name to the server to check and see if they exist
            //sends the message to the sever to be sent to 
            //other clients
            NetworkController.SendMessage(input);

            string receiver = "_pm" + name + "_";
            string id = "_pm" + username + name + "_";
            new PrivateChatBox(username, "Private Chat: " + username + " & " + name, receiver, id);
        }
    }
}
