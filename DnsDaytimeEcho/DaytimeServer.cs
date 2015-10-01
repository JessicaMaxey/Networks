/*
The daytime protocol:
Server binds to port 13
A:  Server listens for daytime requests on that port
When a client connects:
   Server immediately sends date & time to client as a string
   The server closes the connection with the client
Server goes back to step A:
*/

using System;
using System.Net.Sockets;
using System.IO;
public class DaytimeServer {
  public static void Main(String[] args) {
    TcpListener server=null;
    try {
      //Daytime servers listen on standard port 13
      int portNumber = 13;
      
      //Daytime server first binds to port 13
      //The server will be of type TcpListener; 
      //an object which accepts TCP connections
      server = new TcpListener(portNumber);
      //Server starts listening on specified port
      server.Start();

      //Daytime server loops forever, listening for clients
      for(;;) {
	Console.WriteLine("Waiting for a connection....");
	//Accept the pending client connection and return a client 
	//initialized for communication.
	//This method will BLOCK until a connection is made
	TcpClient client = server.AcceptTcpClient();
	Console.WriteLine("Connection accepted.");

	//Prepare response - note there is no particular 'format' in the daytime protocol!
        //The DateTime class gets the current time
	String responseString = DateTime.Now.ToString();

	//Make a user-friendly StreamWriter from the stream
        //client.GetStream() is how we get the data stream to the client
        //Note that 'streams' only make sense in the context of protocols like TCP
        //There would be no such thing as a UDP stream, for example
	StreamWriter sw=new StreamWriter(client.GetStream());

        //Send the message
	sw.WriteLine(responseString);
	Console.WriteLine("Message Sent: " + responseString);

	//Close the StreamWriter, then the stream & connection
        //and in that order...
	sw.Close();
	client.Close();
      }
    } catch (Exception e) {
        //A catch-all exception handler
        //Just display the entire stack trace
	Console.WriteLine(e+" "+e.StackTrace);
    } finally {
	//Release the port and stop the server
        //finally is ALWAYS the last thing to run in a try...catch
	server.Stop();
    }
  }
}
