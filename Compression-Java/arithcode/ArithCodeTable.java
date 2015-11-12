import java.util.*;
public class ArithCodeTable {

	//Entries in the code table are:
	//relative frequency a symbol appears, and
	//left end-point of interval this symbol occupies
	double[] prob = new double[256];
	double[] intervallo = new double[256];

	//The size of the message
	int numBytes;

	//Generate the code table
	public ArithCodeTable(byte[] msg) {
		//Count each symbol in message
		numBytes=msg.length;
		for (int i=0;i<numBytes;i++) {
			int index=(msg[i]+256)%256;
			prob[index]+=1.0/numBytes;
		}
		//Construct the interval
		makeEndpoints();
	}

	private void makeEndpoints() {
		double lo=0.0;
		double hi;
		for (int i=0;i<prob.length;i++) {
			//If entry is 0, it is not part of the interval
			if (prob[i]!=0) {
				//Otherwise, left endpoint is the previous value of lo
				intervallo[i]=lo;
				//Increment lo by the relative frequency of the symbol
				lo+=prob[i];
			}
		}
	}

	//Return the relative frequency of a symbol
	public double probability(byte symbol) {
		return prob[(symbol+256)%256];
	}

	//Return the left-hand endpoint the symbol occupies in [0,1]
	public double lowEndPoint(byte symbol) {
		return intervallo[(symbol+256)%256];
	}

	//Convert the table to a string representation for output purposes
	public String toString() {
		String output="";
		for (int i=0;i<prob.length;i++) {
			if (prob[i]!=0)	output+=(i+"\t"+prob[i]+"\t"+intervallo[i]+"\r\n");
		}
		return output;
	}
}
