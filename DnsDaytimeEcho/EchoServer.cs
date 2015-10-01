using System;
using System.Net.Sockets;
using System.IO;
using System.Threading;
public class EchoServer {

  StreamReader sr=null;
  StreamWriter sw=null;
  TcpClient client=null;

  public EchoServer(TcpClient tcpc) {client=tcpc;}

  public void Conversation() {
    try {
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
    } catch (Exception e) {
	Console.WriteLine(e+" "+e.StackTrace);
    } finally {
	if (sr!=null) sr.Close();
	if (sw!=null) sw.Close();
	if (client!=null) client.Close();
    }
  }

  public static void Main(String[] args) {

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
	//Accept the pending client connection and return a client 
	//initialized for communication
	//This method will block until a connection is made
	EchoServer es = new EchoServer(server.AcceptTcpClient());

	//Allow this conversation to run in a new thread
	Thread serverThread = new Thread(
		new ThreadStart(es.Conversation));
	serverThread.Start();

	//Loop back up and wait for another client
	//Another thread is servicing this client
      }
    } catch (Exception e) {
	Console.WriteLine(e+" "+e.StackTrace);
    } finally {
	//Release the port and stop the server
	server.Stop();
    }

  }

}
