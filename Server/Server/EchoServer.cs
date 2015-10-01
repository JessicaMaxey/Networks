using System;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Collections.Generic;


//

public class EchoServer
{
    TcpClient tcpclient = null;
    List<string> namelist = new List<string>();

    public EchoServer(TcpClient tcpc) { tcpclient = tcpc; }
    private static object lockthis = new object();

    public void Messaging()
    {
        StreamReader sreader = null;
        StreamWriter swriter = null;

        try
        {
            ////Make a user-friendly StreamReader from the stream
            sreader = new StreamReader(tcpclient.GetStream());
     
            ////Make a user-friendly StreamWriter from the stream
            swriter = new StreamWriter(tcpclient.GetStream());
            string input = sreader.ReadLine();
            string screenname = "";

            Console.WriteLine("Connected...");

           
            //Lock the thread while name is added
            lock (lockthis)
            {
                //Gets the name from the client
                Console.WriteLine("Enter screen name: " + input);
                screenname = input;

                //checks to see if name is already in list
                if (!namelist.Contains(screenname))
                    namelist.Add(screenname);
                //if name is already in list server will add a number to the end
                //to keep names unique
                else
                {
                    screenname = screenname + (namelist.Count + 1);
                    namelist.Add(screenname);
                    Console.WriteLine("Screen name already exsists, updated to " + (screenname + (namelist.Count + 1)));
                }
            }

            input = "";

            while (input != "exit")
            {
                Console.WriteLine(screenname + ": " + input);
                swriter.WriteLine(input);
                swriter.Flush();
                Console.WriteLine(screenname + ": " + input);
                input = sreader.ReadLine();
            }

            Console.WriteLine("Client connection exited");
            sreader.Close();
            swriter.Close();
            tcpclient.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e + " " + e.StackTrace);
        }
        finally
        {
            if (sreader != null) sreader.Close();
            if (swriter != null) swriter.Close();
            if (tcpclient != null) tcpclient.Close();
        }
    }


    public static void Main(string[] args)
    {

        TcpListener server = null;

        try
        {
            //Echo servers listen on port 7
            int portNumber = 7;

            //Echo server first binds to port 7
            server = new TcpListener(portNumber);
            //Server starts listening
            server.Start();

            //Echo server loops forever, listening for clients
            for (;;)
            {
                //Accept the pending client connection and return a client 
                //initialized for communication
                //This method will block until a connection is made
                EchoServer es = new EchoServer(server.AcceptTcpClient());

                //Allow this conversation to run in a new thread
                Thread serverThread = new Thread(
                    new ThreadStart(es.Messaging));
                serverThread.Start();

                //Loop back up and wait for another client
                //Another thread is servicing this client
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e + " " + e.StackTrace);
        }
        finally
        {
            //Release the port and stop the server
            server.Stop();
        }

    }

}
