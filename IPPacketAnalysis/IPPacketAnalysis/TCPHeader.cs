using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Windows;
using System.Windows.Forms;

namespace IPPacketAnalysis
{
    class TCPHeader
    {
        //16 bits
        private uint source_port;
        //16 bits
        private uint destin_port;
        //32 bits
        private uint sequence_number;
        //32 bits
        private uint acknowledgement_number;
        //16 bits
        private uint data_offset_and_flags;
        //16 bits
        private uint window_size;
        //16 bits
        private int checksum;
        //16 bits
        private uint urgent_pointer;
        //32 bits
        private byte[] options;

        //Header length
        private byte byte_header_length;

        //Length of data
        private uint message_length;
        //data carried by TCP packet
        private byte[] tcp_data = new byte[4096];


        public TCPHeader (byte[] byte_data, int received)
        {
            try
            {
                MemoryStream memory_stream = new MemoryStream(byte_data, 0, received);
                BinaryReader binary_reader = new BinaryReader(memory_stream);

                //first 16 bits contain the source port
                source_port = (uint)IPAddress.NetworkToHostOrder(binary_reader.ReadInt16());

                //next 16 bits contain the destination port
                destin_port = (uint)IPAddress.NetworkToHostOrder(binary_reader.ReadInt16());

                //next 32 bits contain the sequence number
                sequence_number = (uint)IPAddress.NetworkToHostOrder(binary_reader.ReadInt32());

                //next 32 bits contain the acknoledgement number
                acknowledgement_number = (uint)IPAddress.NetworkToHostOrder(binary_reader.ReadInt32());

                //next 16 bits contain the date offset and flags
                data_offset_and_flags = (uint)IPAddress.NetworkToHostOrder(binary_reader.ReadInt16());

                //next 16 contain the window size
                window_size = (uint)IPAddress.NetworkToHostOrder(binary_reader.ReadInt16());

                //next 16 contain the checksum
                checksum = (int)IPAddress.NetworkToHostOrder(binary_reader.ReadInt16());

                //next 16 of the header contains the urgent pointer
                urgent_pointer = (uint)IPAddress.NetworkToHostOrder(binary_reader.ReadInt16());

                //next 32 bits for IP options and padding
                options = binary_reader.ReadBytes(3);

                //calculates the header length so that we can find the tcp data and scoop it out
                byte_header_length = (byte)(data_offset_and_flags >> 12);
                byte_header_length *= 4;


                //stores how long the message is
                message_length = (uint)(received - byte_header_length);

                if (message_length < 4096)
                {
                    //stores the tcp message data in this array
                    Array.Copy(byte_data, byte_header_length, tcp_data, 0, received - byte_header_length);
                }
                else
                {
                    tcp_data[0] = Convert.ToByte(0);
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message, "TCP Header", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public string SourcePort
        {
            get
            {
                return source_port.ToString();
            }
        }

        public string DestinPort
        {
            get
            {
                return destin_port.ToString();
            }
        }

        public string SequenceNumber
        {
            get
            {
                return sequence_number.ToString();
            }
        }
        public string AcknowledgementNumber
        {
            get
            {
                //if the ack flag is set the we have a valid value
                //so we much check before returning
                if ((data_offset_and_flags & 0x10) != 0)
                {
                    return acknowledgement_number.ToString();
                }
                else
                    return "";
            }
        }
        public string DataOffsetFlags
        {
            get
            {
                //The last 6 bits of data offset and flags contain the flag values
                //so we must extract them from the data
                uint flags = data_offset_and_flags & 0x3F;

                //creating the format of the flag
                string str_flags = string.Format("0x{0:x2} (", flags);

                //now check to see if the bits are set
                if((flags & 0x01) != 0)
                {
                    str_flags += "FIN, ";
                }
                if ((flags & 0x02) != 0)
                {
                    str_flags += "SYN, ";
                }
                if ((flags & 0x04) != 0)
                {
                    str_flags += "RST, ";
                }
                if ((flags & 0x08) != 0)
                {
                    str_flags += "PSH, ";
                }
                if ((flags & 0x10) != 0)
                {
                    str_flags += "ACK, ";
                }
                if ((flags & 0x20) != 0)
                {
                    str_flags += "URG, ";
                }
                str_flags += ")";

                //If the flag is empty, remove it
                if (str_flags.Contains("()"))
                {
                    str_flags = str_flags.Remove(str_flags.Length - 3);
                }
                else if (str_flags.Contains(", )"))
                {
                    str_flags = str_flags.Remove(str_flags.Length - 3, 2);
                }


                //return the beautifully formatted flag
                return str_flags;
            }
        }
        public string WindowSize
        {
            get
            {
                return window_size.ToString();
            }
        }

        public string Checksum
        {
            get
            {
                //format in hexdecimal 
                return string.Format("0x{0:x2}", checksum);
            }
        }

        public string UrgentPointer
        {
            get
            {
                //if the ack flag is set the we have a valid value
                //so we much check before returning
                if ((data_offset_and_flags & 0x20) != 0)
                {
                    return urgent_pointer.ToString();
                }
                return "";
            }
        }

        public string ByteHeaderLength
        {
            get
            {
                return byte_header_length.ToString();
            }
        }


        public uint MessageLength
        {
            get
            {
                return message_length;
            }
        }

        public string Options
        {
            get
            {
                string temp = null;

                for (int i = 0; i < options.Length; i++)
                {
                    temp += options[i];
                    temp += " ";
                }
                return temp;
            }
        }


        public byte[] TCPData
        {
            get
            {
                return tcp_data;
            }
        }
    }
}
