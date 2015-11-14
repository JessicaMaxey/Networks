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
using System.Xml.Serialization;


namespace Compression_Client
{
    class HTTP
    {
        SOAP soap_message = new SOAP();
        string ip_address = "";
        int port = 0;

        public HTTP (string ipaddress, int portnum, SOAP soapmessage)
        {
            soap_message = soapmessage;
            ip_address = ipaddress;
            port = portnum;
        }
            
        public async void Message ()
        {
            //create http client
            HttpClient client = new HttpClient();

            //create connection address
            Uri uri = new Uri(@"http://" + ip_address + ":" + port + @"/");

            var request_message = new HttpRequestMessage(HttpMethod.Post, uri);

            var multipart_content = new MultipartContent(soap_message.ToString());


            request_message.Content = multipart_content;


            var response = await client.SendAsync(request_message).ConfigureAwait(false);
        }  

    }
}
