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

namespace SoapClient
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
            var reader = new BinaryReader(new FileStream(file_name, FileMode.Open));
            byte[] fileData = new byte[reader.BaseStream.Length];
            reader.BaseStream.Read(fileData, 0, (int)reader.BaseStream.Length);

            XNamespace soap_envelope = "http://schemas.xmlsoap.org/soap/envelope/";

            //create the xml file to be sent out
            soap_message =  new XDocument(
                                new XDeclaration("1.0", "utf-8", string.Empty),
                                new XElement(soap_envelope + "Envelope",
                                    new XAttribute(XNamespace.Xmlns + "soapenv", soap_envelope),
                                    new XElement(soap_envelope + "Header",
                                        new XAttribute("contentlength", fileData.Length),
                                        new XAttribute("filename", filename)
                                                    ),
                                            new XElement(soap_envelope + "Body",
                                                new XAttribute("contentmessage", fileData)
                                                        )
                                                )
                                            );


        }


        public void ParseXDoc (XDocument returned_doc)
        {
            XElement node = returned_doc.Element("contentmessage");

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
