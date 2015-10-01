using System;
using System.Net;
public class FindHosts {
	public static void Main(String[] args) {

	    try {

		//Get name of local machine
		//Dns.GetHostName returns a string
		String thisMachine = Dns.GetHostName();
		Console.WriteLine("Local machine's name: "+thisMachine);

		//Use Dns.Resolve to return IPHostEntry object
		//We get the IP addresses using its AddressList property
		IPHostEntry localHost = Dns.Resolve(thisMachine);

		//AddressList is an array since a name may have 
		//multiple addresses
		IPAddress[] localAddresses = localHost.AddressList;

		//Display these addresses
		Console.Write("Local machine's IP address(es): ");
		foreach (IPAddress ipa in localAddresses) 
			Console.Write(ipa+" ");
		Console.WriteLine();

		//A host may have multiple names as well
		String[] localNames = localHost.Aliases;

		//Display these aliases
		Console.Write("Local machine's aliases: ");
		foreach (String name in localNames) 
			Console.Write(name+" ");
		Console.WriteLine();

		//Do this for a host specified by user
		//Pass host name in thru command line
		//www.oit.edu is default
		String host;
		if (args.Length>0) host=args[0]; 
		else host="www.oit.edu";

		IPHostEntry inputHost = Dns.Resolve(host);
		IPAddress[] inputAddresses = inputHost.AddressList;
		Console.Write(host+"'s IP address(es): ");
		foreach (IPAddress ipa in inputAddresses) 
			Console.Write(ipa+" ");
		Console.WriteLine();

		String[] inputNames = inputHost.Aliases;

		Console.Write(host+"'s aliases: ");
		foreach (String name in inputNames) 
			Console.Write(name+" ");
		Console.WriteLine();

	    } catch (Exception e) {
		Console.WriteLine(e+" "+e.StackTrace);
	    }
	}
}