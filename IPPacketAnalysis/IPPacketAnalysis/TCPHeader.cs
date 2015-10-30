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
        private int source_port;
        //16 bits
        private int destin_port;
        //32 bits
        private int sequence_number;
        //32 bits
        private int acknowledgement_number;
        //16 bits
        private int data_offset_and_flags;
        //16 bits
        private int window_size;
        //16 bits
        private int checksum;
        //16 bits
        private int urgent_pointer;

        //Header length
        private byte byte_header_length;

        //Length of data
        private int message_length;
        //data carried by TCP packet
        private byte[] tcp_data = new byte[4096];


        public TCPHeader (byte[] byte_data, int received)
        {
            try
            {
                MemoryStream memory_stream = new MemoryStream(byte_data, 0, received);
                BinaryReader binary_reader = new BinaryReader(memory_stream);

                //first 16 bits contain the source port
                source_port = (int)IPAddress.NetworkToHostOrder(binary_reader.ReadInt16());

                //next 16 bits contain the destination port
                destin_port = (int)IPAddress.NetworkToHostOrder(binary_reader.ReadInt16());

                //next 32 bits contain the sequence number
                sequence_number = (int)IPAddress.NetworkToHostOrder(binary_reader.ReadInt32());

                //next 32 bits contain the acknoledgement number
                acknowledgement_number = (int)IPAddress.NetworkToHostOrder(binary_reader.ReadInt32());

                //next 16 bits contain the date offset and flags
                data_offset_and_flags = (int)IPAddress.NetworkToHostOrder(binary_reader.ReadInt16());

                //next 16 contain the window size
                window_size = (int)IPAddress.NetworkToHostOrder(binary_reader.ReadInt16());

                //next 16 contain the checksum
                checksum = (int)IPAddress.NetworkToHostOrder(binary_reader.ReadInt16());

                //the last 16 of the header contains the urgent pointer
                urgent_pointer = (int)IPAddress.NetworkToHostOrder(binary_reader.ReadInt16());

                //calculates the header length so that we can find the tcp data and scoop it out
                byte_header_length = (byte)(data_offset_and_flags >> 12);
                byte_header_length *= 4;

                //stores how long the message is
                message_length = (int)(received - byte_header_length);

                //stores the tcp message data in this array
                Array.Copy(byte_data, byte_header_length, tcp_data, 0, received - byte_header_length);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message, "TCP Header", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public string SoursePort
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
                int flags = data_offset_and_flags & 0x3F;

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


        public int MessageLength
        {
            get
            {
                return message_length;
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
