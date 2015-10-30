using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Text;

namespace IPPacketAnalyzerNET
{
    public partial class Form1 : Form
    {

        public Thread Listener;
        int packetCounter;

        public Form1()
        {
            InitializeComponent();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            packetCounter = 0;
            btnStart.Enabled = false;//grey out Start button
            btnStop.Enabled = true;//ungrey Stop
            Listener = new Thread(new ThreadStart(Run));
            Listener.Start();//Go
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = true;
            btnStop.Enabled = false;
            if (Listener != null)//if not null already, kill it
            {
                Listener.Abort();//stop the thread
                Listener.Join();
                Listener = null;//point reference to nothing
            }
        }

        public void Run()
        {
            //we will set the I/O buffer lengths at 4096 bytes - plenty big enough for a 802.3 packet
            int len_receive_buf = 4096;
            int len_send_buf = 4096;

            byte[] receive_buf = new byte[len_receive_buf];
            byte[] send_buf = new byte[len_send_buf];
            int cout_receive_bytes;

            /*Create a socket - the Socket class allows us to set many options for the socket
            
            The AddressFamily property specifies the addressing scheme that an instance of the Socket
            class can use.  This property is read-only and is set when the Socket is created.
            In C#, AddressFamily is an enumeration; each value it can take corresponds to some integer
            The InterNetwork value represents IPv4.

            A raw socket supports access to the underlying transport protocol. Using the SocketType Raw, 
            you can communicate using protocols like Internet Control Message Protocol (Icmp). 
            Your application must provide a complete IP header when sending. Received datagrams return 
            with the IP header and options intact.
            Note: SocketType is also an enumeration and Raw is one of its values
            
            ProtocolType is another enumeration; the IP value sets the protocol to IP
            
            */
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.IP);

            /*If you are in blocking mode, and you make a method call which does not complete immediately, 
            your application will block execution until the requested operation completes.*/
            socket.Blocking = false;
            
            //Get the IPAddress address list of the local host
            IPHostEntry IPHost = Dns.GetHostByName(Dns.GetHostName());

            //Bind the socket to the first IP address returned above
            socket.Bind(new IPEndPoint(IPAddress.Parse(IPHost.AddressList[0].ToString()), 0));

            //Set the socket options - IP means Socket options apply only to IP sockets
            //SocketOptionName.HeaderIncluded-Indicates that the application provides the IP header 
            //for outgoing datagrams.
            socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.HeaderIncluded, 1);

            //The following mods change the socket so that it can receive all IP packets on the network
            byte[] IN = new byte[4] { 1, 0, 0, 0 };
            byte[] OUT = new byte[4];
            int SIO_RCVALL = unchecked((int)0x98000001);

            //IOControl(Int32, Byte[], Byte[]) - Sets low-level operating modes for the Socket
            //using numerical control codes.
            //For more information look up the Windows WSAIoctl function
            int ret_code = socket.IOControl(SIO_RCVALL, IN, OUT);
            while (true)
            {
                /*BeginReceive(Byte[], Int32, Int32, SocketFlags, AsyncCallback, Object)
                Begins to asynchronously receive data from a connected Socket.
                Parameters:
                buffer-An array of type Byte that is the storage location for the received data. 
                offset-The zero-based position in the buffer parameter at which to store the received data. 
                size-The number of bytes to receive. 
                socketFlags-A bitwise combination of the SocketFlags values. 
                callback-An AsyncCallback delegate that references the method to invoke when the operation is complete. 
                state-A user-defined object that contains information about the receive operation. 
                    This object is passed to the EndReceive delegate when the operation is complete.
                Return Value: An IAsyncResult that references the asynchronous read.*/
                IAsyncResult ar = socket.BeginReceive(receive_buf, 0, len_receive_buf, SocketFlags.None, null, this);

                //EndReceive raps up the read, and returns an array of bytes
                cout_receive_bytes = socket.EndReceive(ar);
                //output the packet (to the listbox on the form)
                ProcessPacket(receive_buf, cout_receive_bytes);
            }
        }

        public void ProcessPacket(byte[] buf, int len)
        {
            //Take the IP packet apart
            packetCounter++;
            lbPackets.Invoke(new MethodInvoker(() => { lbPackets.Items.Add("XXXXX PACKET #"+packetCounter+" ARRIVING XXXXX"); }));
            int version = buf[0] >> 4;
            lbPackets.Invoke(new MethodInvoker(() => { lbPackets.Items.Add("Version: "+version); }));
            int hlen = buf[0] % 16;
            lbPackets.Invoke(new MethodInvoker(() => { lbPackets.Items.Add("Header Length: " + hlen); }));
            int precedence = buf[1] >> 5;
            lbPackets.Invoke(new MethodInvoker(() => { lbPackets.Items.Add("Precedence: " + precedence); }));
            int delay = (buf[1] >> 4) % 2;
            lbPackets.Invoke(new MethodInvoker(() => { lbPackets.Items.Add("Delay: " + delay); }));
            int throughput = (buf[1] >> 3) % 2;
            lbPackets.Invoke(new MethodInvoker(() => { lbPackets.Items.Add("Throughput: " + throughput); }));
            int reliability = (buf[1] >> 2) % 2;
            lbPackets.Invoke(new MethodInvoker(() => { lbPackets.Items.Add("Reliability: " + reliability); }));
            int totalLength = (buf[2] << 8) + buf[3];
            lbPackets.Invoke(new MethodInvoker(() => { lbPackets.Items.Add("Total Length: " + totalLength); }));
            int identification = (buf[4] << 8) + buf[5];
            lbPackets.Invoke(new MethodInvoker(() => { lbPackets.Items.Add("Identification: " + identification); }));
            int fragFlag1 = (buf[6] >> 6) % 2;
            lbPackets.Invoke(new MethodInvoker(() => { lbPackets.Items.Add("Fragmentation flag #1: " + fragFlag1); }));
            int fragFlag2 = (buf[6] >> 5) % 2;
            lbPackets.Invoke(new MethodInvoker(() => { lbPackets.Items.Add("Fragmentation flag #2: " + fragFlag2); }));
            int fragOffset = ((buf[6] % 64) << 8) + buf[7];
            lbPackets.Invoke(new MethodInvoker(() => { lbPackets.Items.Add("Fragmentation Offset: " + fragOffset); }));
            int timeToLive = buf[8];
            lbPackets.Invoke(new MethodInvoker(() => { lbPackets.Items.Add("Time To Live: " + timeToLive); }));
            int protocol = buf[9];
            lbPackets.Invoke(new MethodInvoker(() => { lbPackets.Items.Add("Protocol: " + protocol); }));
            int headerCheck = (buf[10] << 8) + buf[11];
            lbPackets.Invoke(new MethodInvoker(() => { lbPackets.Items.Add("Header Checksum: " + headerCheck); }));
            lbPackets.Invoke(new MethodInvoker(() => { lbPackets.Items.Add("Source IP Address: " + buf[12]+"."+ buf[13]+"."+ buf[14]+"."+ buf[15]); }));
            lbPackets.Invoke(new MethodInvoker(() => { lbPackets.Items.Add("Destination IP Address: " + buf[16]+"."+ buf[17]+"."+ buf[18]+"."+ buf[19]); }));
            lbPackets.Invoke(new MethodInvoker(() => { lbPackets.Items.Add("IP options: " + buf[20]+" "+ buf[21]+" "+ buf[22]); }));

            lbPackets.Invoke(new MethodInvoker(() => { lbPackets.Items.Add("Data follows (in decimal): "); }));
            StringBuilder dataLineBuf = new StringBuilder();
            //NOTE: we leave out the padding byte #23 in the display
            for (int i = 24; i < totalLength; i++)
            {
                dataLineBuf.Append(buf[i]);
                dataLineBuf.Append(" ");
            }
            String dataLine = dataLineBuf.ToString();
            lbPackets.Invoke(new MethodInvoker(() => { lbPackets.Items.Add(dataLine); }));
            lbPackets.Invoke(new MethodInvoker(() => { lbPackets.Items.Add("XXXXX PACKET #" + packetCounter + " COMPLETE XXXXX"); }));

        }
    }
}
