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

            source_port = (uint)IPAddress.NetworkToHostOrder(binary_reader.ReadInt16());
            destination_port = (uint)IPAddress.NetworkToHostOrder(binary_reader.ReadInt16());
            length = (uint)IPAddress.NetworkToHostOrder(binary_reader.ReadInt16());
            checksum = (int)IPAddress.NetworkToHostOrder(binary_reader.ReadInt16());

            //chops off the header and leaves the data
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
                return string.Format("0x:{0:x2}", checksum);
            }

        }

        public byte[] UDPData
        {
            get
            {
                return UDP_data;
            }

        }
    }
}
