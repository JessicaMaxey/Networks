using System.Collections.Generic;
using System;
using System.Net.Sockets;
using System.IO;
using System.Threading;
public class EchoServer
{

    StreamReader sreader = null;
    StreamWriter swriter = null;
    TcpClient tcpclient = null;
    private static object lockthis = new object();
    static List<string> namelist = new List<string>();
    static List<Tuple<EchoServer, string>> esnamelist = new List<Tuple<EchoServer, string>>();

    public EchoServer(TcpClient tcpc) { tcpclient = tcpc; }

    public void Messaging()
    {
        try
        {
            Console.WriteLine("Connected... Getting screen name... ");

            sreader = new StreamReader(tcpclient.GetStream());

            swriter = new StreamWriter(tcpclient.GetStream());

            string screenname = "";
            String input = sreader.ReadLine();

            //Lock the thread while name is added
            lock (lockthis)
            {
                //Gets the name from the client, which will be the first message they send through
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
                    input = ("Screen name already exsists, updated to " + screenname);

                }

                var es_name_enum = esnamelist.GetEnumerator();

                for (int i= 0; i < esnamelist.Count; i++)
                {
                    if(esnamelist[i].Item1 == this)
                    {
                        esnamelist[i] = new Tuple<EchoServer, string>(esnamelist[i].Item1, screenname);
                    }
                }
                
                //sends out a message that the client has joined the room
                input = (screenname + " has joined the chatroom");

            }



            //displays the message in the chat room
            while (input != "exit")
            {
                Console.WriteLine(screenname + ": " + input);
                swriter.WriteLine(input);
                swriter.Flush();

                lock (lockthis)
                {
                    for (int i = 0; i < esnamelist.Count; i++)
                    {
                        if (esnamelist[i].Item1 != this)
                        {
                            esnamelist[i].Item1.Message(screenname, input);
                        }
                    }
                }

                input = sreader.ReadLine();
            }

            //gets rid of the clients screenname when they leave the room
            //because they are no longer active in the chat room
            lock(lockthis)
            {
                namelist.Remove(screenname);
            }

            //sends out a message when the client leaves the room
            Console.WriteLine(screenname + " left the chat room.");
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

    public void Message(string screenname, string input)
    {
        //displays the message in the other chat rooms
        swriter.WriteLine(screenname + ": " + input);
        swriter.Flush();
    }

    public static void Main(String[] args)
    {

        TcpListener server = null;

        try
        {
            int portNumber = 7;

            server = new TcpListener(portNumber);

            server.Start();

            //loops listening for clients
            for (;;)
            {
                //gets the clients, will wait here listening for clients
                EchoServer es = new EchoServer(server.AcceptTcpClient());

                //stores the es to keep track of all the clients
                Tuple<EchoServer, string> new_es = new Tuple<EchoServer, string>(es, "");
                esnamelist.Add(new_es);

                //creates threads
                Thread serverThread = new Thread(new ThreadStart(es.Messaging));
                serverThread.Start();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e + " " + e.StackTrace);
        }
        finally
        {
            server.Stop();
        }

    }

}
