using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic;
using System.Collections.Concurrent;

public static class NetworkController
{
    static Dictionary<string, Func<string, bool>> listeners = new Dictionary<string, Func<string, bool>>();
    static BlockingCollection<string> messages = new BlockingCollection<string>();
    static private TcpClient client;
    static private StreamWriter swriter = null;
    static private StreamReader sreader = null;
    static private Thread receive_thread = null;
    static private Thread send_thread = null;

    const int port = 7;

    public static bool Initialize(string ip)
    {
        bool connection_failure = false;

        if(client != null)
        {
            receive_thread.Abort();
            send_thread.Abort();
            sreader.Close();
            swriter.Close();

        }

        client = new TcpClient();
        client.Connect(ip, port);
        var stream = client.GetStream();

        swriter = new StreamWriter(stream);
        sreader = new StreamReader(stream);

        receive_thread = new Thread(new ThreadStart(Receiver));
        receive_thread.Start();

        send_thread = new Thread(new ThreadStart(Sender));
        send_thread.Start();

        return connection_failure;
    }

    public static void AddListener(string token, Func<string, bool> d)
    {
        try
        {
            listeners.Add(token, d);
        }
        catch(Exception e)
        {
            MessageBox.Show(e.Message, "Can not start another private chat with someone you're already in chat with!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    public static void RemoveListener(string token)
    {
        listeners.Remove(token);
    }

    public static void SendMessage(string message)
    {
        messages.Add(message);
    }

    private static void Receiver ()
    {
        while(true)
        {
            try
            {
                string returndata = sreader.ReadLine();
                int tag_end = 0;

                string key = "";
                if (returndata.Contains("_"))
                {
                    int first_tag = returndata.IndexOf('_');
                    tag_end = returndata.IndexOf('_', first_tag + 1) + 1;

                    key = returndata.Substring(first_tag, tag_end - first_tag);
                }

                if (listeners.ContainsKey(key))
                {
                    var d = listeners[key];
                    d(returndata.Substring(tag_end));
                }
            }
            catch
            {

            }
        }
    }

    private static void Sender ()
    {
        while (true)
        {
            swriter.WriteLine(messages.Take());
            swriter.Flush();
        }
    }


}
