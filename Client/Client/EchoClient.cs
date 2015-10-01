using System;
using System.Net.Sockets;
using System.IO;
using System.Collections.Generic;
using System.Threading;

public class EchoClient : TcpClient
{
    static StreamWriter swriter = null;
    static StreamReader sreader = null;
    static List<EchoClient> clientlist = new List<EchoClient>();
    static bool endreader = false;
    private static object lockthis = new object();

    public EchoClient(String host)
    {
        //must be the same port as the server
        int port = 7;

        base.Connect(host, port);
    }

    public void Reader ()
    {
        //while the client is still alive, read for data
        //if client has been closed, the reading will end
        while (endreader != true )
        {
            String returndata = sreader.ReadLine();
            Console.WriteLine(returndata);
            returndata = sreader.ReadLine();
        }

    }

    public static void Main(String[] args)
    {
        EchoClient client = null;

        try
        {
            //Host name comes from command line
            //If no host specified, local machine is host
            String host = args.Length == 1 ? args[0] : "127.0.0.1";

            //creates connection
            client = new EchoClient(host);

            //gets stream between server and client
            NetworkStream ns = client.GetStream();

            //creates writer and reader
            swriter = new StreamWriter(ns);
            sreader = new StreamReader(ns);

            //tells the user how to exit the chat properly 
            Console.Write("Enter text: \"exit\" to leave the chat: \n");

            //prompts the user the screen name for this client
            String input;
            Console.Write("Enter screen name: ");

            //starts thread to read messages being sent from other clients
            //from the server
            Thread readthread = new Thread(new ThreadStart(client.Reader));
            readthread.Start();


            while ((input = Console.ReadLine()) != "exit")
            {
                //sends the message to the sever to be sent to 
                //other clients
                swriter.WriteLine(input);
                swriter.Flush();
            }

            //tells the server that the client has left
            swriter.WriteLine("exit");
            swriter.Flush();


            //tell reader to stop reading
            endreader = true;

        }
        catch (Exception e)
        {
            Console.WriteLine(e + " " + e.StackTrace);
        }

    }
}