using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Windows.Forms;
using System.IO;


namespace IPPacketAnalysis
{
    class UDPHeader
    {
        //16 bits
        private uint source_port;
        //16 bits
        private uint destination_port;
        //16 bits
        private uint length;
        //16 bits
        private int checksum;

        private byte[] UDP_data = new byte[4096];

        public UDPHeader (byte[] byte_data, int received)
        {
            MemoryStream memory_stream = new MemoryStream(byte_data, 0, received);
            BinaryReader binary_reader = new BinaryReader(memory_stream);

            //first 16 bits are the source port
            source_port = (uint)IPAddress.NetworkToHostOrder(binary_reader.ReadInt16());

            //next 16 bits are the destination port
            destination_port = (uint)IPAddress.NetworkToHostOrder(binary_reader.ReadInt16());

            //next 16 bits are the length
            length = (uint)IPAddress.NetworkToHostOrder(binary_reader.ReadInt16());

            //last 16bits are the checksum
            checksum = (int)IPAddress.NetworkToHostOrder(binary_reader.ReadInt16());

            //chops off the header and leaves the http data
            Array.Copy(byte_data, 8, UDP_data, 0, received - 8);
        }
        
        public string SourcePort
        {
            get
            {
                return source_port.ToString();
            }
        } 

        public string DestinationPort
        {
            get
            {
                return destination_port.ToString();
            }
        }

        public uint Length
        {
            get
            {
                return length;
            }
        }

        public string Checksum
        {
            get
            {
                //format into hexdecimal data
                return string.Format("0x:{0:x2}", checksum);
            }

        }

        public byte[] UDPData
        {
            get
            {
                //stores all the rest of the data that is not in the TCPheader to be later formated into
                //http data

                //string temp = null;
                ////lumps all the htm values into on string formated to hexdec
                //for (int i = 0; i < UDP_data.Length; i++)
                //{
                //    temp += string.Format("0x{0:x2}", UDP_data[i]);
                //    temp += " ";
                //}
                //return temp;

                return UDP_data;
            }

        }
    }
}
