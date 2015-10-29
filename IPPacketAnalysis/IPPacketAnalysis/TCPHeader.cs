using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IPPacketAnalysis
{
    class TCPHeader
    {

        public TCPHeader (byte[] byte_data, int received)
        {
            //byte byte_header_length;

            MemoryStream memory_stream = new MemoryStream(byte_data, 0, received);
            BinaryReader binary_reader = new BinaryReader(memory_stream);

            int source_port = (int)IPAddress.NetworkToHostOrder(binary_reader.ReadInt16());
            trans_lb.Invoke(new MethodInvoker(() => { trans_lb.Items.Add("Source Port" + source_port); }));


            int destin_port = (int)IPAddress.NetworkToHostOrder(binary_reader.ReadInt16());
            trans_lb.Invoke(new MethodInvoker(() => { trans_lb.Items.Add("Destination Port" + destin_port); }));


            int sequence_number = (int)IPAddress.NetworkToHostOrder(binary_reader.ReadInt32());
            trans_lb.Invoke(new MethodInvoker(() => { trans_lb.Items.Add("Sequence Number" + sequence_number); }));


            int acknowledgement_number = (int)IPAddress.NetworkToHostOrder(binary_reader.ReadInt32());
            trans_lb.Invoke(new MethodInvoker(() => { trans_lb.Items.Add("Acknowledgement Number" + acknowledgement_number); }));


            int data_offset_and_flags = (int)IPAddress.NetworkToHostOrder(binary_reader.ReadInt16());
            trans_lb.Invoke(new MethodInvoker(() => { trans_lb.Items.Add("Data Offset and Flags" + data_offset_and_flags); }));


            int window_size = (int)IPAddress.NetworkToHostOrder(binary_reader.ReadInt16());
            trans_lb.Invoke(new MethodInvoker(() => { trans_lb.Items.Add("Window Size" + window_size); }));


            int checksum = (int)IPAddress.NetworkToHostOrder(binary_reader.ReadInt16());
            trans_lb.Invoke(new MethodInvoker(() => { trans_lb.Items.Add("Checksum" + checksum); }));


            int urgent_pointer = (int)IPAddress.NetworkToHostOrder(binary_reader.ReadInt16());
            trans_lb.Invoke(new MethodInvoker(() => { trans_lb.Items.Add("Urgent Pointer" + urgent_pointer); }));


            //byte_header_length = (byte)(data_offset_and_flags >> 12);
            //byte_header_length *= 4;

            //int message_length = (int)(received - byte_header_length);
        }
    }
}
