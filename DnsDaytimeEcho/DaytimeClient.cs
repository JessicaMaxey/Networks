using System;
using System.Net.Sockets;
using System.Windows.Forms;
using System.IO;

//DaytimeClient will inherit from TcpClient class
public class DaytimeClient:TcpClient {

   //Our Daytime client connects when the constructor is called
   public DaytimeClient(String host) {
	//Uses the Connect method inherited from TcpClient
	//Daytime protocol is thru port 13 on server
	base.Connect(host,13);
   }
   public static void Main(String[] args) {

     DaytimeClient dtreceiver=null;
     StreamReader sr=null;

     try {
	//Host name comes from command line
	//If no host specified, local machine is host
	String host=args.Length==1?args[0]:"127.0.0.1";
        //Try running it for time.nist.gov

	//Connect to Daytime server (if it exists at specified address)
	dtreceiver=new DaytimeClient(host);

	//Get the stream between server and client
        //Note: the stream is created when the connection is made
	//Create a user-friendly StreamReader
	//Daytime servers send ASCII only strings
	sr=new StreamReader(dtreceiver.GetStream());

	//Get the daytime from the server - read the entire stream contents
	String returndata=sr.ReadToEnd();
	MessageBox.Show("Time at "+host+": "+returndata);
     } catch (Exception e) {
	MessageBox.Show(e+" "+e.StackTrace);
     } finally {
        //The server SHOULD be closing these on ITS end
        //so, we check first if they're already gone
        //If NOT, close them
	if (sr!=null) sr.Close();
	if (dtreceiver!=null) dtreceiver.Close();	
     }
   }
}