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
                    screenname = screenname + "(" + (namelist.Count + 1) + ")";
                    namelist.Add(screenname);
                    input = ("Screen name already exsists, updated to " + screenname);

                    EchoMessage(screenname, input);
                }


                //keeps track of the echoserver instances, updates the current instance with
                //this clients screen name
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
                

                if (input.Contains("_finduser_") == true)
                {
                    bool found = false;
                    //remove keyword from input
                    input = input.Remove(0, 10);

                    //search list to see if user name is in the list of clients already connected
                    lock (lockthis)
                    {
                        for (int i = 0; i < namelist.Count; i++)
                        {
                            if (namelist[i] == input)
                            {
                                //if true, send messagebox to that user asking if they want to
                                //start a chat with the requesting user
                                SendPrivateBox();
                                found = true;
                            }
                        }
                        //if false, the user is not found, then send message back to client 
                        //indicating that there is no such user

                    }
                    input = "";
                }
                else if (input != "")
                {
                    Console.WriteLine(screenname + ": " + input);

                    //sends message out to everyone
                    SendMessage(screenname, input);
                    //sends the same massage back to client into their chatbox
                    //to see what they just wrote
                    EchoMessage(screenname, input);

                    input = sreader.ReadLine();
                }
            }

            //gets rid of the clients screenname when they leave the room
            //because they are no longer active in the chat room
            lock(lockthis)
            {
                for (int i = 0; i < esnamelist.Count; i++)
                {
                    if (esnamelist[i].Item1 == this)
                    {
                        esnamelist.Remove(esnamelist[i]);
                    }
                }

                namelist.Remove(screenname);
                
            }

            //sends out a message when the client leaves the room
            input = (screenname + " left the chat room.");

            //sends message to all clients that are connected
            SendMessage(screenname, input);

            Console.WriteLine(screenname + " left the chat room.");

            //closes the connection streams
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
            //closes the connection streams
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

    public void SendMessage (string screenname, string input)
    {
        //locks the thread so no one else can use it while it
        //is sending messages to everyone
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
    }

    public void SendPrivateBox ()
    {

    }
    


    public void EchoMessage (string screenname, string input)
    {
        //sends a message to only this client 
        lock (lockthis)
        {
            for (int i = 0; i < esnamelist.Count; i++)
            {
                if (esnamelist[i].Item1 == this)
                {
                esnamelist[i].Item1.Message(screenname, input);
                }
            }
        }
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

                //stores the es to keep track of all the clients instances
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
