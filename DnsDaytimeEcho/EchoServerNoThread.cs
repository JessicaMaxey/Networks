using System;
using System.Net.Sockets;
using System.IO;
/*
The echo protocol
An echo server binds to port 7
A: It listens for connection request from clients on that port
When a connection to a client is established
   B: Server waits for an incoming string from the client (this could be a long time)
   Server reads the string when it arrives
   If the string is only a period '.'
      close the connection with the client
      goto step A
   otherwise, 
      server immediately sends same string back to the client
      goto step B
*/
public class EchoServerNoThread {
  public static void Main(String[] args) {
    StreamReader sr=null;
    StreamWriter sw=null;
    TcpClient client=null;
    TcpListener server=null;
    try {
      //Echo servers listen on port 7
      int portNumber = 7;

      //Echo server first binds to port 7
      server = new TcpListener(portNumber);
      //Server starts listening
      server.Start();

      //Echo server loops forever, listening for clients
      for(;;) {
	Console.WriteLine("Waiting for a connection....");

	//Accept the pending client connection and return a client 
	//initialized for communication
	//This method will block until a connection is made
	client = server.AcceptTcpClient();
	Console.WriteLine("Connection accepted.");

	//Make a user-friendly StreamReader from the stream
	sr=new StreamReader(client.GetStream());

	//Make a user-friendly StreamWriter from the stream
	sw=new StreamWriter(client.GetStream());

	String incoming=sr.ReadLine();
	while (incoming!=".") {
		Console.WriteLine("Message received: "+incoming);
		sw.WriteLine(incoming);
		sw.Flush();
		Console.WriteLine("Message Sent back: " + incoming);
		incoming=sr.ReadLine();
	}
	Console.WriteLine("Client sent '.': closing connection.");
	sr.Close();
	sw.Close();
	client.Close();
      }
    } catch (Exception e) {
	Console.WriteLine(e+" "+e.StackTrace);
    } finally {
	if (sr!=null) sr.Close();//check if the stream reader is present - if it is, close it
	if (sw!=null) sw.Close();//check if the stream writer is present - if it is, close it
	if (client!=null) client.Close();
	//Release the port and stop the server
	server.Stop();
    }
  }
}
