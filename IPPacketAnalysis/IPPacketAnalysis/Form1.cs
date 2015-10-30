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
using System.Net;

namespace IPPacketAnalysis
{
    public partial class Form1 : Form
    {
        private Socket main_socket;
        private byte[] byte_data = new byte[4096];
        int packet_counter;
        bool keep_collecting = false;
        int protocol = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void start_btn_Click(object sender, EventArgs e)
        {
            packet_counter = 0;
            Run();
        }

        private void Run ()
        {
            try
            {
                //might need to add bool to stop collecting.
                if (!keep_collecting)
                {
                    start_btn.Text = "Stop";
                    keep_collecting = true;

                    int cout_receive_bytes;

                    IPHostEntry ip_host_info = Dns.Resolve(Dns.GetHostName());


                    main_socket = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.IP);

                    main_socket.Bind(new IPEndPoint(IPAddress.Parse(ip_host_info.AddressList[0].ToString()), 0));

                    main_socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.HeaderIncluded, true);

                    byte[] IN = new byte[4] { 1, 0, 0, 0 };
                    byte[] OUT = new byte[4] { 1, 0, 0, 0 };

                    main_socket.IOControl(IOControlCode.ReceiveAll, IN, OUT);

                    main_socket.BeginReceive(byte_data, 0, byte_data.Length, SocketFlags.None, new AsyncCallback(ReceivePacket), this);
                }
                else
                {
                    start_btn.Text = "Start";
                    keep_collecting = false;
                    main_socket.Close();
                }
                
            }
            catch (Exception)
            {

            }
        }

        private void ReceivePacket(IAsyncResult ar)
        {
            try
            {
                int received = main_socket.EndReceive(ar);

                ParsePacket(byte_data, received);

                if (keep_collecting)
                {
                    byte_data = new byte[4096];
                    main_socket.BeginReceive(byte_data, 0, byte_data.Length, SocketFlags.None, new AsyncCallback(ReceivePacket), this);
                }
            }
            catch(ObjectDisposedException)
            {
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Packet Sniffer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void ParsePacket (byte[] byte_data, int received)
        {
            //creates IP header
            IP(byte_data, received);

            switch(protocol)
            {
                case 6:
                    {
                        TCP(byte_data, received);
                    }
                    break;

                case 17:
                    {
                        UDP(byte_data, received);
                    }
                    break;

                default:
                    break;

            }


        }

        private void IP (byte[] byte_data, int received)
        {
            IPHeader ipheader = new IPHeader(byte_data, received);

            //Take the IP packet apart
            packet_counter++;
            ip_lb.Invoke(new MethodInvoker(() => { ip_lb.Items.Add("XXXXX PACKET #" + packet_counter + " ARRIVING XXXXX"); }));


            int version = byte_data[0] >> 4;
            ip_lb.Invoke(new MethodInvoker(() => { ip_lb.Items.Add("Version: " + version); }));
            ip_lb.Invoke(new MethodInvoker(() => { ip_lb.Items.Add("Version: " + ipheader.VersionAndHeaderLength); }));

            int hlen = byte_data[0] % 16;
            ip_lb.Invoke(new MethodInvoker(() => { ip_lb.Items.Add("Header Length: " + hlen); }));
            ip_lb.Invoke(new MethodInvoker(() => { ip_lb.Items.Add("Header Length: " + ipheader.HeaderLength); }));


            int precedence = byte_data[1] >> 5;
            ip_lb.Invoke(new MethodInvoker(() => { ip_lb.Items.Add("Precedence: " + precedence); }));
            ip_lb.Invoke(new MethodInvoker(() => { ip_lb.Items.Add("Differentied Services: " + ipheader.DifferentiatedServices); }));


            int delay = (byte_data[1] >> 4) % 2;
            ip_lb.Invoke(new MethodInvoker(() => { ip_lb.Items.Add("Delay: " + delay); }));


            int throughput = (byte_data[1] >> 3) % 2;
            ip_lb.Invoke(new MethodInvoker(() => { ip_lb.Items.Add("Throughput: " + throughput); }));


            int reliability = (byte_data[1] >> 2) % 2;
            ip_lb.Invoke(new MethodInvoker(() => { ip_lb.Items.Add("Reliability: " + reliability); }));


            int totalLength = (byte_data[2] << 8) + byte_data[3];
            ip_lb.Invoke(new MethodInvoker(() => { ip_lb.Items.Add("Total Length: " + totalLength); }));
            ip_lb.Invoke(new MethodInvoker(() => { ip_lb.Items.Add("Total Length: " + ipheader.TotalLength); }));


            int identification = (byte_data[4] << 8) + byte_data[5];
            ip_lb.Invoke(new MethodInvoker(() => { ip_lb.Items.Add("Identification: " + identification); }));
            ip_lb.Invoke(new MethodInvoker(() => { ip_lb.Items.Add("Identification: " + ipheader.Identification); }));


            int fragFlag1 = (byte_data[6] >> 6) % 2;
            ip_lb.Invoke(new MethodInvoker(() => { ip_lb.Items.Add("Fragmentation flag #1: " + fragFlag1); }));
            ip_lb.Invoke(new MethodInvoker(() => { ip_lb.Items.Add("Fragmentation flag #1: " + ipheader.DataOffAndFlags); }));


            int fragFlag2 = (byte_data[6] >> 5) % 2;
            ip_lb.Invoke(new MethodInvoker(() => { ip_lb.Items.Add("Fragmentation flag #2: " + fragFlag2); }));
            ip_lb.Invoke(new MethodInvoker(() => { ip_lb.Items.Add("Fragmentation flag #2: " + ipheader.DataOffAndFlags); }));


            int fragOffset = ((byte_data[6] % 64) << 8) + byte_data[7];
            ip_lb.Invoke(new MethodInvoker(() => { ip_lb.Items.Add("Fragmentation Offset: " + fragOffset); }));
            ip_lb.Invoke(new MethodInvoker(() => { ip_lb.Items.Add("Fragmentation Offset: " + ipheader.FragmentationOffset); }));



            int timeToLive = byte_data[8];
            ip_lb.Invoke(new MethodInvoker(() => { ip_lb.Items.Add("Time To Live: " + timeToLive); }));
            ip_lb.Invoke(new MethodInvoker(() => { ip_lb.Items.Add("Time To Live: " + ipheader.TTL); }));


            protocol = byte_data[9];
            ip_lb.Invoke(new MethodInvoker(() => { ip_lb.Items.Add("Protocol: " + protocol); }));
            ip_lb.Invoke(new MethodInvoker(() => { ip_lb.Items.Add("Protocol: " + ipheader.Protocol); }));


            int headerCheck = (byte_data[10] << 8) + byte_data[11];
            ip_lb.Invoke(new MethodInvoker(() => { ip_lb.Items.Add("Header Checksum: " + headerCheck); }));
            ip_lb.Invoke(new MethodInvoker(() => { ip_lb.Items.Add("Header Checksum: " + ipheader.Checksum); }));


            ip_lb.Invoke(new MethodInvoker(() => { ip_lb.Items.Add("Source IP Address: " + byte_data[12] + "." + byte_data[13] + "." + byte_data[14] + "." + byte_data[15]); }));
            ip_lb.Invoke(new MethodInvoker(() => { ip_lb.Items.Add("Source IP Address: " + ipheader.SourceIPAddress); }));


            ip_lb.Invoke(new MethodInvoker(() => { ip_lb.Items.Add("Destination IP Address: " + byte_data[16] + "." + byte_data[17] + "." + byte_data[18] + "." + byte_data[19]); }));
            ip_lb.Invoke(new MethodInvoker(() => { ip_lb.Items.Add("Destination IP Address: " + ipheader.DestinationIPAddress); }));


            ip_lb.Invoke(new MethodInvoker(() => { ip_lb.Items.Add("IP options: " + byte_data[20] + " " + byte_data[21] + " " + byte_data[22]); }));


            ip_lb.Invoke(new MethodInvoker(() => { ip_lb.Items.Add("Data follows (in decimal): "); }));
            StringBuilder dataLinebyte_data = new StringBuilder();
            //NOTE: we leave out the padding byte #23 in the display
            for (int i = 24; i < totalLength; i++)
            {
                dataLinebyte_data.Append(byte_data[i]);
                dataLinebyte_data.Append(" ");
            }
            String dataLine = dataLinebyte_data.ToString();
            ip_lb.Invoke(new MethodInvoker(() => { ip_lb.Items.Add(dataLine); }));
            ip_lb.Invoke(new MethodInvoker(() => { ip_lb.Items.Add("XXXXX PACKET #" + packet_counter + " COMPLETE XXXXX"); }));



        }

        private void TCP(byte[] byte_data, int received)
        {
            TCPHeader tcp_header = new TCPHeader(byte_data, received);
            try
            {
                trans_lb.Invoke(new MethodInvoker(() => { trans_lb.Items.Add("Source Port: " + tcp_header.SourcePort); }));
                trans_lb.Invoke(new MethodInvoker(() => { trans_lb.Items.Add("Destination Port: " + tcp_header.DestinPort); }));
                trans_lb.Invoke(new MethodInvoker(() => { trans_lb.Items.Add("Sequence Number: " + tcp_header.SequenceNumber); }));
                trans_lb.Invoke(new MethodInvoker(() => { trans_lb.Items.Add("Acknowledgement Number: " + tcp_header.AcknowledgementNumber); }));
                trans_lb.Invoke(new MethodInvoker(() => { trans_lb.Items.Add("Data offset and Flags: " + tcp_header.DataOffsetFlags); }));
                trans_lb.Invoke(new MethodInvoker(() => { trans_lb.Items.Add("Window Size: " + tcp_header.WindowSize); }));
                trans_lb.Invoke(new MethodInvoker(() => { trans_lb.Items.Add("Checksum: " + tcp_header.Checksum); }));
                trans_lb.Invoke(new MethodInvoker(() => { trans_lb.Items.Add("Urgent Pointer: " + tcp_header.UrgentPointer); }));




            }
            catch (Exception e)
            {

            }
        }

        private void UDP(byte[] byte_data, int received)
        {

        }

    }
}
