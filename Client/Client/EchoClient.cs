using System;
using System.Net.Sockets;
using System.IO;
using System.Collections.Generic;
public class EchoClient : TcpClient
{

    //Echo client connects when constructor called
    public EchoClient(String host)
    {
        //Uses the Connect method from TcpClient
        //Echo protocol is thru port 7 on server
        base.Connect(host, 7);
    }

    public static void Main(String[] args)
    {
        EchoClient conversant = null;
        StreamWriter swriter = null;
        StreamReader sreader = null;
        try
        {
            //Host name comes from command line
            //If no host specified, local machine is host
            String host = args.Length == 1 ? args[0] : "127.0.0.1";

            //Connect to Echo server
            conversant = new EchoClient(host);

            //Get the stream between server and client
            NetworkStream ns = conversant.GetStream();

            //Create a user-friendly StreamWriter
            swriter = new StreamWriter(ns);

            //Create a user-friendly StreamReader
            sreader = new StreamReader(ns);

            //Prompt user for message to send to server
            //Period "exit" tells server to end session
            String input;
            Console.Write("Enter screen name: ");

            while ((input = Console.ReadLine()) != "exit")
            {
                //Send message to server
                swriter.WriteLine(input);
                swriter.Flush();

                //Get the Echo from the server
                String returndata = sreader.ReadLine();
                Console.WriteLine("Reply from " + host + ": " + returndata);
                Console.Write("Enter text: \"exit\" to stop: ");
            }

            //Send final message and scram
            swriter.WriteLine(".");
            swriter.Flush();

        }
        catch (Exception e)
        {
            Console.WriteLine(e + " " + e.StackTrace);
        }
        finally
        {
            //Close the connection whether exception thrown or not
            if (swriter != null) swriter.Close();
            if (sreader != null) sreader.Close();
            if (conversant != null) conversant.Close();
        }
    }

}