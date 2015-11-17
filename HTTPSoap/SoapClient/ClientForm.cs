using System;
using System.Threading;
using System.Net.Http;
using System.Windows.Forms;
using System.IO;

namespace SoapClient
{
    public partial class ClientForm : Form
    {
        ClientContext mClient;
        public ClientForm()
        {
            InitializeComponent();
        }

        private void ClientForm_Load(object sender, EventArgs e)
        {

        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            mClient = new ClientContext(@"http://localhost:8080/");

            mClient.SendData(ClientText.Text);

            //Bad. Do not use while loop to wait for response, handle this with a trigger somehow. Maybe a timer to ping in every so often.
            //Don't sit and spin in a GUI thread
            while (mClient.DataAvailable == false)
                Thread.Sleep(50);

            ClientText.Text = mClient.ReceiveData();
        }
    }

    //Class to handle the client communication stuff without worrying about threads and shit
    class ClientContext
    {
        private HttpClient mContext;
        private bool mThreadToken = false;
        private byte[] mTxData;

        public bool DataAvailable {get; private set;}
        
        public ClientContext(string uri)
        {
            DataAvailable = false;
            mContext = new HttpClient();

            mContext.BaseAddress = new Uri(uri);
            mContext.Timeout = TimeSpan.FromSeconds(500); //Basically never timeout
            mContext.MaxResponseContentBufferSize = 500000; //Never too much data

            Thread clientRun = new Thread(ClientLoop);
            clientRun.Start();
        }

        private void ClientLoop()
        {
            while (true)
            {
                //Sleep until the thread is awoken for tx
                while (mThreadToken == false)
                    Thread.Sleep(50);

                //Send the request
                var responseTask = mContext.PostAsync("", new ByteArrayContent(mTxData));

                //Clear sleep token
                mThreadToken = false;

                responseTask.Wait();
                var response = responseTask.Result;

                //Get the request
                var messageTask = response.Content.ReadAsStreamAsync();
                messageTask.Wait();
                MemoryStream message = messageTask.Result as MemoryStream;

                mTxData = message.ToArray();
                DataAvailable = true;

                //Sleep until the currently queued data is freed
                while (DataAvailable == true)
                    Thread.Sleep(50);
            }
        }

        public void SendData(byte[] data)
        {
            while (DataAvailable == true) ;

            mTxData = data;
            mThreadToken = true;


            while (mThreadToken == true) ; //Wait until the Client Loop notices and clears the transaction
        }

        public byte[] ReceiveData()
        {
            if (DataAvailable == true)
            {
                DataAvailable = false;
                return mTxData;
            }

            //Data is not available
            return null;
        }
    }
}
