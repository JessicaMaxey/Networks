using System;
using System.Collections.Generic;

public static class NetworkController
{
    static Dictionary<string, object> listeners = new Dictionary<string, object>();
    static Queue<string> messages = new Queue<string>();

    public static bool Initialize(string ip, string port)
    {
        bool connection_failure = false;

        return connection_failure;
    }

    public static void AddListener (string token, object d)
    {
        listeners.Add(token, d);
    }

    public static void RemoveListener(string token)
    {
        listeners.Remove(token);
    }

   
}
