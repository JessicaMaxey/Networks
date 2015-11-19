using System;
using System.Threading;
using System.Net.Http;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace SoapClient
{
    public partial class ClientForm : Form
    {
        ClientContext m_client;
        public ClientForm()
        {
            InitializeComponent();
        }

        private void ClientForm_Load(object sender, EventArgs e)
        {

        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            var reader = new BinaryReader(new FileStream(file_location_txtbx.Text, FileMode.Open));
            byte[] fileData = new byte[reader.BaseStream.Length + 1];
            reader.BaseStream.Read(fileData, 1, (int)reader.BaseStream.Length);
            reader.Close();
            fileData[0] = (byte)'c';

            m_client = new ClientContext(@"http://" + ip_address_txtbx.Text + ":" + port_txtbx.Text + "/");

            m_client.SendData(fileData);

            //Bad. Do not use while loop to wait for response, handle this with a trigger somehow. Maybe a timer to ping in every so often.
            //Don't sit and spin in a GUI thread
            while (m_client.data_available == false)
                Thread.Sleep(50);

            var new_data = m_client.ReceiveData();
            FileStream new_file = new FileStream(file_location_txtbx.Text + ".comp", FileMode.OpenOrCreate);
            new_file.SetLength(0);
            new_file.Position = 0;
            new_file.Write(new_data, 0, new_data.Length);
            new_file.Close();

            MessageBox.Show( "Compression Complete","", MessageBoxButtons.OK);
        }

        private void decompress_btn_Click(object sender, EventArgs e)
        {
            var reader = new BinaryReader(new FileStream(file_location_txtbx.Text, FileMode.Open));
            byte[] fileData = new byte[reader.BaseStream.Length + 1];
            reader.BaseStream.Read(fileData, 1, (int)reader.BaseStream.Length);
            reader.Close();
            fileData[0] = (byte)'d';

            m_client = new ClientContext(@"http://" + ip_address_txtbx.Text + ":" + port_txtbx.Text + "/");

            m_client.SendData(fileData);

            //Bad. Do not use while loop to wait for response, handle this with a trigger somehow. Maybe a timer to ping in every so often.
            //Don't sit and spin in a GUI thread
            while (m_client.data_available == false)
                Thread.Sleep(50);

            var new_data = m_client.ReceiveData();
            FileStream new_file = new FileStream(file_location_txtbx.Text + ".decomp", FileMode.OpenOrCreate);
            new_file.SetLength(0);
            new_file.Position = 0;
            new_file.Write(new_data, 0, new_data.Length);
            new_file.Close();
            MessageBox.Show( "Decompression Complete","", MessageBoxButtons.OK);

        }
    }

    //Class to handle the client communication stuff without worrying about threads and shit
    class ClientContext
    {
        private HttpClient m_context;
        private bool m_thread_token = false;
        private byte[] m_text_data;

        public bool data_available {get; private set;}
        
        public ClientContext(string uri)
        {
            data_available = false;
            m_context = new HttpClient();

            m_context.BaseAddress = new Uri(uri);
            m_context.Timeout = TimeSpan.FromSeconds(500); //Basically never timeout
            m_context.MaxResponseContentBufferSize = 500000; //Never too much data

            Thread clientRun = new Thread(ClientLoop);
            clientRun.Start();
        }

        private void ClientLoop()
        {
            while (true)
            {
                //Sleep until the thread is awoken for tx
                while (m_thread_token == false)
                    Thread.Sleep(50);

                //Send the request
                var response_task = m_context.PostAsync("", new ByteArrayContent(m_text_data));

                //Clear sleep token
                m_thread_token = false;

                response_task.Wait();
                var response = response_task.Result;

                //Get the request
                var message_task = response.Content.ReadAsStreamAsync();
                message_task.Wait();
                BinaryReader message = new BinaryReader(message_task.Result);

                m_text_data = message.ReadBytes((int)message.BaseStream.Length);
                string temp = Convert.ToString(m_text_data);
                data_available = true;

                //Sleep until the currently queued data is freed
                while (data_available == true)
                    Thread.Sleep(50);
            }
        }

        public void SendData(byte[] data)
        {
            while (data_available == true) ;

            m_text_data = data;
            m_thread_token = true;


            while (m_thread_token == true) ; //Wait until the Client Loop notices and clears the transaction
        }

        public byte[] ReceiveData()
        {
            if (data_available == true)
            {
                data_available = false;
                return m_text_data;
            }

            //Data is not available
            return null;
        }
    }
}
