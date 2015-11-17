using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Diagnostics;

namespace SoapServer
{
    class ServerContext
    {
        HttpListenerContext mContext {get; set; }
        public ServerContext(HttpListenerContext listernerContext)
        {
            //Save context and spawn a new thread to handle stuff
            mContext = listernerContext;
            Thread newCom = new Thread(this.ServerComm);
            newCom.Start();
        }

        //Do server things here
        void ServerComm()
        {
            //Handle input from listener context
            var request = mContext.Request;
            var dataBody = request.InputStream;
            var inputStream = new StreamReader(dataBody);

            Console.WriteLine(inputStream.ReadToEnd());

            //Handle output from listener context
            var response = mContext.Response;
            var responseStream = response.OutputStream;
            var responseWrite = new StreamWriter(responseStream);

            responseWrite.Write(Console.ReadLine());

            //End everything we put into it
            responseWrite.Close();
            response.Close();
            responseStream.Close();

            inputStream.Close();
            dataBody.Close();
        }
    }

    class Server
    {
        static void Main(string[] args)
        {
            //Make the listener
            HttpListener server = new HttpListener();
            server.Prefixes.Add(@"http://*:8080/");
            server.Start();

            while (true)
            {
                //Get a context and make a new server connection
                var ServerContext = new ServerContext(server.GetContext());
            }
        }
    }
}
