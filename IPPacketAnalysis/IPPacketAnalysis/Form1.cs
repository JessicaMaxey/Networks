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
                //start collecting data
                if (!keep_collecting)
                {
                    start_btn.Text = "Stop";
                    keep_collecting = true;

                    //set up for the socket
                    IPHostEntry ip_host_info = Dns.Resolve(Dns.GetHostName());

                    main_socket = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.IP);

                    main_socket.Bind(new IPEndPoint(IPAddress.Parse(ip_host_info.AddressList[0].ToString()), 0));

                    main_socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.HeaderIncluded, true);

                    byte[] IN = new byte[4] { 1, 0, 0, 0 };
                    byte[] OUT = new byte[4] { 1, 0, 0, 0 };

                    main_socket.IOControl(IOControlCode.ReceiveAll, IN, OUT);

                    //start getting data
                    main_socket.BeginReceive(byte_data, 0, byte_data.Length, SocketFlags.None, new AsyncCallback(ReceivePacket), this);
                }
                else
                {
                    //stop collecting data
                    start_btn.Text = "Start";
                    keep_collecting = false;
                    main_socket.Close();
                }
                
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Connection Issue", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ReceivePacket(IAsyncResult ar)
        {
            try
            {
                //get the data received
                int received = main_socket.EndReceive(ar);

                //send the data off to be processed into IP header
                //TCP/UDP header, and the to left over hmtl data
                ParsePacket(byte_data, received);

                //check to see if still collecting data
                if (keep_collecting)
                {
                    //collect the next message in line
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
            IPHeader ipheader = new IPHeader(byte_data, received);
            IP(ipheader);

            switch (ipheader.Protocol)
            {
                case "TCP":
                    {
                        //sends the left overs from the IP header into TCP if that is the protocol
                        //found insided the ip header
                        TCPHeader tcpheader = new TCPHeader(ipheader.IPData, ipheader.MessageLength);
                        TCP(tcpheader);
                    }
                    break;

                case "UDP":
                    {
                        //sends the left overs from the IP header into UDP if that is the protocol
                        //found insided the ip header
                        UDPHeader udpheader = new UDPHeader(ipheader.IPData, ipheader.MessageLength);
                        UDP(udpheader);
                    }
                    break;

                default:
                    break;

            }


        }

        private void IP (IPHeader ipheader)
        {
            //output the IP header data
            try
            {
                packet_counter++;
                ip_lb.Invoke(new MethodInvoker(() => { ip_lb.Items.Add("XXXXX PACKET #" + packet_counter + " ARRIVING XXXXX"); }));

                ip_lb.Invoke(new MethodInvoker(() => { ip_lb.Items.Add("Version: " + ipheader.VersionAndHeaderLength); }));
                ip_lb.Invoke(new MethodInvoker(() => { ip_lb.Items.Add("Header Length: " + ipheader.HeaderLength); }));
                ip_lb.Invoke(new MethodInvoker(() => { ip_lb.Items.Add("Differentied Services: " + ipheader.DifferentiatedServices); }));
                ip_lb.Invoke(new MethodInvoker(() => { ip_lb.Items.Add("Total Length: " + ipheader.TotalLength); }));
                ip_lb.Invoke(new MethodInvoker(() => { ip_lb.Items.Add("Identification: " + ipheader.Identification); }));
                ip_lb.Invoke(new MethodInvoker(() => { ip_lb.Items.Add("Fragmentation flag #1: " + ipheader.DataOffAndFlags); }));
                ip_lb.Invoke(new MethodInvoker(() => { ip_lb.Items.Add("Fragmentation flag #2: " + ipheader.DataOffAndFlags); }));
                ip_lb.Invoke(new MethodInvoker(() => { ip_lb.Items.Add("Fragmentation Offset: " + ipheader.FragmentationOffset); }));
                ip_lb.Invoke(new MethodInvoker(() => { ip_lb.Items.Add("Time To Live: " + ipheader.TTL); }));
                ip_lb.Invoke(new MethodInvoker(() => { ip_lb.Items.Add("Protocol: " + ipheader.Protocol); }));
                ip_lb.Invoke(new MethodInvoker(() => { ip_lb.Items.Add("Header Checksum: " + ipheader.Checksum); }));
                ip_lb.Invoke(new MethodInvoker(() => { ip_lb.Items.Add("Source IP Address: " + ipheader.SourceIPAddress.ToString()); }));
                ip_lb.Invoke(new MethodInvoker(() => { ip_lb.Items.Add("Destination IP Address: " + ipheader.DestinationIPAddress.ToString()); }));
                ip_lb.Invoke(new MethodInvoker(() => { ip_lb.Items.Add("IP options: " + ipheader.Options); }));

                ip_lb.Invoke(new MethodInvoker(() => { ip_lb.Items.Add("XXXXX PACKET #" + packet_counter + " COMPLETE XXXXX"); }));
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message, "IP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TCP(TCPHeader tcpheader)
        {
            //output the TCP header data
            try
            {
                string data_list = null;

                trans_lb.Invoke(new MethodInvoker(() => { trans_lb.Items.Add("XXXXX PACKET #" + packet_counter + " ARRIVING XXXXX"); }));
                trans_lb.Invoke(new MethodInvoker(() => { trans_lb.Items.Add("XXXXXXXXXXXX  TCP  XXXXXXXXXXXX"); }));
                trans_lb.Invoke(new MethodInvoker(() => { trans_lb.Items.Add("Source Port: " + tcpheader.SourcePort); }));
                trans_lb.Invoke(new MethodInvoker(() => { trans_lb.Items.Add("Destination Port: " + tcpheader.DestinPort); }));
                trans_lb.Invoke(new MethodInvoker(() => { trans_lb.Items.Add("Sequence Number: " + tcpheader.SequenceNumber); }));
                trans_lb.Invoke(new MethodInvoker(() => { trans_lb.Items.Add("Acknowledgement Number: " + tcpheader.AcknowledgementNumber); }));
                trans_lb.Invoke(new MethodInvoker(() => { trans_lb.Items.Add("Data offset and Flags: " + tcpheader.DataOffsetFlags); }));
                trans_lb.Invoke(new MethodInvoker(() => { trans_lb.Items.Add("Window Size: " + tcpheader.WindowSize); }));
                trans_lb.Invoke(new MethodInvoker(() => { trans_lb.Items.Add("Checksum: " + tcpheader.Checksum); }));
                trans_lb.Invoke(new MethodInvoker(() => { trans_lb.Items.Add("Urgent Pointer: " + tcpheader.UrgentPointer); }));
                trans_lb.Invoke(new MethodInvoker(() => { trans_lb.Items.Add("Options: " + tcpheader.Options); }));


                //get the left over data, and get ready to output the http data
                if (tcpheader.MessageLength < 4096)
                {
                    StringBuilder hex_data = new StringBuilder();
                    for (int i = 24; i < tcpheader.MessageLength; i++)
                    {
                        hex_data.Append(tcpheader.TCPData[i]);
                        hex_data.Append(" ");
                    }
                    data_list = hex_data.ToString();
                }

                //Outputing the http data to the data label
                app_lb.Invoke(new MethodInvoker(() => { app_lb.Items.Add("XXXXX PACKET #" + packet_counter + " ARRIVING XXXXX"); }));
                if (data_list == "")
                {
                    //in case of no data
                    app_lb.Invoke(new MethodInvoker(() => { app_lb.Items.Add("No Data"); }));
                }
                else if (data_list != null)
                {
                    //in case of data
                    app_lb.Invoke(new MethodInvoker(() => { app_lb.Items.Add(data_list); }));
                }
                else
                {
                    //in case the data was too large for the buffer
                    app_lb.Invoke(new MethodInvoker(() => { app_lb.Items.Add("Data was too large"); }));
                }
                app_lb.Invoke(new MethodInvoker(() => { app_lb.Items.Add("XXXXX PACKET #" + packet_counter + " COMPLETE XXXXX"); }));


                trans_lb.Invoke(new MethodInvoker(() => { trans_lb.Items.Add("XXXXX PACKET #" + packet_counter + " COMPLETE XXXXX"); }));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "TCP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UDP(UDPHeader udpheader)
        {
            //output the UDP header data
            try
            {
                string data_list = null;

                trans_lb.Invoke(new MethodInvoker(() => { trans_lb.Items.Add("XXXXX PACKET #" + packet_counter + " ARRIVING XXXXX"); }));
                trans_lb.Invoke(new MethodInvoker(() => { trans_lb.Items.Add("XXXXXXXXXXXX  UDP  XXXXXXXXXXXX"); }));
                trans_lb.Invoke(new MethodInvoker(() => { trans_lb.Items.Add("Source Port: " + udpheader.SourcePort); }));
                trans_lb.Invoke(new MethodInvoker(() => { trans_lb.Items.Add("Destination Port: " + udpheader.DestinationPort); }));
                trans_lb.Invoke(new MethodInvoker(() => { trans_lb.Items.Add("Length: " + udpheader.Length); }));
                trans_lb.Invoke(new MethodInvoker(() => { trans_lb.Items.Add("Checksum: " + udpheader.Checksum); }));


                //get the left over data, and get ready to output the http data
                if (udpheader.Length < 4096)
                {
                    StringBuilder hex_data = new StringBuilder();
                    for (int i = 24; i < udpheader.Length; i++)
                    {
                        hex_data.Append(udpheader.UDPData[i]);
                        hex_data.Append(" ");
                    }
                    data_list = hex_data.ToString();
                }

                //Outputing the http data to the data label
                app_lb.Invoke(new MethodInvoker(() => { app_lb.Items.Add("XXXXX PACKET #" + packet_counter + " ARRIVING XXXXX"); }));
                if (data_list == "")
                {
                    //in case of no data
                    app_lb.Invoke(new MethodInvoker(() => { app_lb.Items.Add("No Data"); }));
                }
                else if (data_list != null)
                {
                    //in case of data
                    app_lb.Invoke(new MethodInvoker(() => { app_lb.Items.Add(data_list); }));
                }
                else
                {
                    //in case the data was too large for the buffer
                    app_lb.Invoke(new MethodInvoker(() => { app_lb.Items.Add("Data was too large"); }));
                }
                app_lb.Invoke(new MethodInvoker(() => { app_lb.Items.Add("XXXXX PACKET #" + packet_counter + " COMPLETE XXXXX"); }));


                trans_lb.Invoke(new MethodInvoker(() => { trans_lb.Items.Add("XXXXX PACKET #" + packet_counter + " COMPLETE XXXXX"); }));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "Besure you are running in Administrator Mode", "UDP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
