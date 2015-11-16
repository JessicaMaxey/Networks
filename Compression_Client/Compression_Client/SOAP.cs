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
        XDocument soap_message;

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


            string file_name_text = null;
            char[] just_name = file_name.ToCharArray();

            for (int i = (file_name.Length - 1); i > 0; i--)
            {
                //goes backwards and finds where the name begins
                if (just_name[i - 1] == '\\')
                {
                    while (i != file_name.Length)
                    {
                        //goes forwards and adds name to the end
                        file_name_text += just_name[i];
                        i++;
                    }
                    break;
                }
            }


            XNamespace soap_envelope = "http://schemas.xmlsoap.org/soap/envelope/";

            //create the xml file to be sent out
            soap_message =  new XDocument(
                                new XDeclaration("1.0", "utf-8", string.Empty),
                                new XElement(soap_envelope + "Envelope",
                                    new XAttribute(XNamespace.Xmlns + "soapenv", soap_envelope),
                                    new XElement(soap_envelope + "Header",
                                        new XAttribute("contentlength", byte_array.Length),
                                        new XAttribute("filename", file_name_text)
                                                    ),
                                            new XElement(soap_envelope + "Body",
                                                new XAttribute("contentmessage", byte_array)
                                                        )
                                                )
                                            );


        }


        public XDocument GetSoapMessage
        {
            get
            {
                return soap_message;
            }
        }

    }
}
