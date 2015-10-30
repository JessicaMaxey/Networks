using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Net;

namespace IPPacketAnalysis
{
    class IPHeader
    {
        //8 bits
        private byte version_and_header_length;   
        //8 bits
        private byte differentiated_services;
        //16 bits
        private int total_length;
        //16 bits
        private int identification;
        //8 bits
        private int data_offset_and_flags;
        //8 bits
        private byte ttl;
        //8 bits
        private byte protocol;
        //16 bits
        private int checksum;
        //32 bits
        private int source_ip_address;
        //32 bits
        private int destination_ip_address;

        //header length
        private byte header_length;

        //data carried by the packet
        private byte[] ip_data = new byte[4096];  


        public IPHeader (byte[] byte_data, int received)
        {
            try
            {
                MemoryStream memory_stream = new MemoryStream(byte_data, 0, received);
                BinaryReader binary_reader = new BinaryReader(memory_stream);

                //First 8 bits contain the version and header length
                version_and_header_length = binary_reader.ReadByte();

                //next 8 bits contain the differentiated services
                differentiated_services = binary_reader.ReadByte();

                //next 16 bits  contain the total length of the packet
                total_length = (int)IPAddress.NetworkToHostOrder(binary_reader.ReadInt16());

                //next 16 bits contains identification 
                identification = (int)IPAddress.NetworkToHostOrder(binary_reader.ReadInt16());

                //next 16 bits contain the data offset and the flags
                data_offset_and_flags = (int)IPAddress.NetworkToHostOrder(binary_reader.ReadInt16());

                //next 8 bits contain the TTL value
                ttl = binary_reader.ReadByte();

                //next 8 bits contain the protocol 
                protocol = binary_reader.ReadByte();

                //next 16 bits contain the checksum
                checksum = IPAddress.HostToNetworkOrder(binary_reader.ReadInt16());

                //next 32 bits containthe source IP address
                source_ip_address = (int)binary_reader.ReadInt32();
                
                //next 32 bits containthe destination IP address
                destination_ip_address = (int)binary_reader.ReadInt32();


                //get header length
                header_length = version_and_header_length;
                //do binary math to extract the last 4 bits which contain the header length
                header_length <<= 4;
                header_length >>= 4;
                header_length *= 4;

                //store the data
                Array.Copy(byte_data, header_length, ip_data, 0, total_length - header_length);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "IP Header", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public string VersionAndHeaderLength
        {
            get
            {
                if ((version_and_header_length >> 4) == 4)
                {
                    return "IPv4";
                }
                else if ((version_and_header_length >> 4) == 6)
                {
                    return "IPv6";
                }
                else
                {
                    return "Unknown";
                }
            }
        } 


        public string HeaderLength
        {
            get
            {
               return header_length.ToString();
            }
        }

        public int MessageLength
        {
            get
            {
                return (int)(total_length - header_length);
            }
        }

        public string DifferentiatedServices
        {
            get
            {
                return string.Format("0x{0:x2} ({1})", differentiated_services);
            }
        }
        
        public string DataOffAndFlags
        {
            get
            {
                //get the 1st 3 bits of the flag to see if the data is to be fragmented or not
                int flags = data_offset_and_flags >> 13;
                if (flags == 2)
                {
                    return "Don't fragment";
                }
                else if (flags == 1)
                {
                    return "More fragments to come";
                }
                else
                {
                    return flags.ToString();
                }

            }
        }

        public string FragmentationOffset
        {
            get
            {
                //last 13 bits of data offset field contains the fragmentation offset
                int offset = data_offset_and_flags << 3;
                offset >>= 3;

                return offset.ToString();
            }

        }


        public string TTL
        {
            get
            {
                return ttl.ToString();
            }
        }

        public string Protocol
        {
            get
            {
                if(protocol == 6)
                {
                    return "TCP";
                }
                else if (protocol == 17)
                {
                    return "UDP";
                }
                else
                {
                    return "Unknown";
                }
            }
        }

        public string Checksum
        {
            get
            {
                return string.Format("0x:{0:x2}", checksum);
            }

        }

        public string Identification
        {
            get
            {
                return identification.ToString();
            }
        }

        public IPAddress SourceIPAddress
        {
            get
            {
                return new IPAddress(source_ip_address);
            }
        }

        public IPAddress DestinationIPAddress
        {
            get
            {
                return new IPAddress(destination_ip_address);
            }
        }

        public int TotalLength
        {
            get
            {
                return total_length;
            }
        }

        public byte[] IPData
        {
            get
            {
                return ip_data;
            }
        }
    }
}
