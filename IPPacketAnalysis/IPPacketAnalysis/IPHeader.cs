using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private int flags_and_offset;
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
    }
}
