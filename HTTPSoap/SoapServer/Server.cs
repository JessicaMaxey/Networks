using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace SoapServer
{
    class ServerContext
    {
        HttpListenerContext mContext { get; set; }
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
            var inputStream = new BinaryReader(dataBody);

            var comp_or_decomp = inputStream.ReadByte();

            if (comp_or_decomp == (byte)'c')
            {
                byte[] data = inputStream.ReadBytes((int)request.ContentLength64);
                DictionaryCompression dict_comp = new DictionaryCompression(data);

                try
                {
                    dict_comp.Compression();
                }
                catch (Exception e)
                {
                    Console.Write(e.Message);
                }
                //Handle output from listener context
                var response = mContext.Response;
                var responseStream = response.OutputStream;
                var responseWrite = new StreamWriter(responseStream);

                IFormatter temp = new BinaryFormatter();

                MemoryStream compressed_file = new MemoryStream();

                var serialDictionary = dict_comp.Dictionary;
                var serialData = dict_comp.CompressedData;

                for (int i = 0; i < serialDictionary.Count; i++)
                {
                    byte[] output = serialDictionary.ElementAt(i).Value;
                    byte length = (byte)output.Length;

                    compressed_file.WriteByte(length);
                    compressed_file.Write(output, 0, output.Length);
                }

                compressed_file.WriteByte(0xFF);

                for (int i = 0; i < serialData.Count; i++)
                {
                    var int_to_byte = BitConverter.GetBytes(serialData[i]);
                    compressed_file.Write(int_to_byte, 0, int_to_byte.Length);
                }

                var serializedData = compressed_file.ToArray();

                //compressed_file now goes into returning XML document
                responseStream.Write(serializedData, 0, serializedData.Length);
                responseWrite.Flush();
                
                //End everything we put into it
                responseWrite.Close();
                response.Close();
                responseStream.Close();

                inputStream.Close();
                dataBody.Close();
            }
            else
            {
                BinaryFormatter formatter = new BinaryFormatter();
                var binaryCompressedFile = inputStream.ReadBytes((int)inputStream.BaseStream.Length);

                Dictionary<int, byte[]> dictionary = new Dictionary<int, byte[]>();
                List<int> data = new List<int>();

                int count = 0;
                int i;
                for (i = 0; i < binaryCompressedFile.Length; i++)
                {
                    if (binaryCompressedFile[i] == 0xFF)
                        break;
                    var sequence = new byte[binaryCompressedFile[i]];
                    for (int x = 0; x < binaryCompressedFile[i]; i++, x++)
                        sequence[x] = binaryCompressedFile[i];
                    dictionary.Add(count++, sequence);
                }

                for (; i < binaryCompressedFile.Length; i += 4)
                {
                    data.Add(BitConverter.ToInt32(binaryCompressedFile, i));
                }

                //Handle output from listener context
                var response = mContext.Response;
                var responseStream = response.OutputStream;
                var responseWrite = new StreamWriter(responseStream);

                IFormatter temp = new BinaryFormatter();

                Stream compressed_file = new MemoryStream();

                //compressed_file now goes into returning XML document
                responseWrite.Write(compressed_file);

                //End everything we put into it
                responseWrite.Close();
                response.Close();
                responseStream.Close();

                inputStream.Close();
                dataBody.Close();
            }
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
