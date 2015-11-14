using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Net.Http;
using System.Net;
using System.Threading;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace Compression_Client
{
    class SOAP
    {
        string file_name = "";
        XElement soap_message;

        public SOAP (string filename)
        {
            //set file name
            file_name = filename;

            int num_of_letters = 0;
            StreamReader reader;

            //gets the stream
            reader = new StreamReader(file_name);

            //finds out how many letters are in the file to set the size of the array
            while (!reader.EndOfStream)
            {
                reader.Read();
                num_of_letters++;
            }

            reader.Close();

            //sets the size of the array to however many characters there are in the file
            byte[] byte_array = new byte[num_of_letters];


            //reads in all the data 
            using (FileStream file_stream = new FileStream(file_name, FileMode.Open))
            {
                for (int i = 0; i < byte_array.Length; i++)
                {
                    file_stream.WriteByte(byte_array[i]);
                }
            }


            //create the xml file to be sent out
            soap_message =
                new XElement("soap:Envelope",
                    new XElement("soap:Header",
                    new XElement("contentlength:" + byte_array.Length),
                    new XElement("filename:" + filename)
                                    ),
                        new XElement("soap:Body",
                        new XElement("contentmessage:" + byte_array)
                                        )
                            );


        }


        public byte[] GetSoapMessage
        {
            get
            {
                byte[] message = Encoding.UTF8.GetBytes(soap_message.ToString());

                return message;
            }
        }

    }
}
