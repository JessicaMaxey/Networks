using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Encryption;

namespace GUI_Client
{
    public partial class PrivateChatBox : Form
    {
        string roomname;
        string identifier;
        string sendIdentifier;
        string myscreenname;

        Encryption.Keys myKeys = Encryption.diffie_hellman.KeyGen(Encryption.diffie_hellman.GeneratePrime(100, 1000), Encryption.diffie_hellman.GeneratePrime(100, 1000));
        Encryption.Keys theirKeys = null;

        delegate bool SetTextCallback(string text);
        //this makes writing to the textbox thread safe
        private bool SetText(string text)
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

        delegate bool SetKeyCallback(string key);
        private bool SetKey(string key)
        {
            if (this.InvokeRequired)
            {
                SetKeyCallback d = new SetKeyCallback(SetText);
                this.Invoke(d, new object[] { key });
            }
            else
            {
                theirKeys = new Encryption.Keys();
                var keys = key.Split(' ');
                theirKeys.public_key_e = UInt64.Parse(keys[0]);
                theirKeys.public_key_n = UInt64.Parse(keys[1]);
            }

            return false;
        }

        public PrivateChatBox(string screenname, string name, string receiver, string id)
        {
            roomname = name;
            sendIdentifier = receiver;
            identifier = id;
            myscreenname = screenname;

            InitializeComponent();

            NetworkController.AddListener(identifier, SetText);
            NetworkController.AddListener("_pmkeys_", SetKey);

            NetworkController.SendMessage(sendIdentifier + "_pmkeys_" + myKeys.public_key_e + " " + myKeys.public_key_n);            

            this.Text = roomname;
            this.Show();
        }

        private void PrivateChatBox_Load(object sender, EventArgs e)
        {

        }

        private void send_btn_Click(object sender, EventArgs e)
        {
            string name = myscreenname + ": ";
            string message = message_txtbx.Text;




            main_txtbx.Text += name + message + "\r\n";

            NetworkController.SendMessage(sendIdentifier + identifier + name + message);

            if (message == "exit")
            {
                NetworkController.RemoveListener(identifier);
                Close();
            }

            message_txtbx.Clear();
        }
    }
}
