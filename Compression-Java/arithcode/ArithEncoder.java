public class ArithEncoder {

	//Encoder fields consist of a table, the message, and the encoded message
	ArithCodeTable table;
	byte[] msg;
	double encodedMessage;

	//A string buffer also records the encoding process for output purposes
	StringBuffer encodingProcess=new StringBuffer(
		"Symbol\tInterval\tCharLoVal\tCharHiVal\tLoVal\tHiVal\r\n");

	//The constructor actually encodes the message
	public ArithEncoder(byte[] msg) {

		table=new ArithCodeTable(msg);
		this.msg=msg;

		double hiVal=1.0;
		double loVal=0.0;

		//Determine the successive shrinking intervals into which to place the message
		for (int i=0;i<msg.length;i++) {
			byte symbol=msg[i];
			double interval=hiVal-loVal;
			double symbolLoVal=table.lowEndPoint(symbol);
			double symbolHiVal=symbolLoVal+table.probability(symbol);
			hiVal=loVal+interval*symbolHiVal;
			loVal=loVal+interval*symbolLoVal;

			//Record the values in each iteration
			encodingProcess.append(symbol+"\t");
			encodingProcess.append(interval+"\t");
			encodingProcess.append(symbolLoVal+"\t");
			encodingProcess.append(symbolHiVal+"\t");
			encodingProcess.append(loVal+"\t");
			encodingProcess.append(hiVal+"\r\n");

			//If the interval shrinks to zero, we have reached our limit
			if (loVal==hiVal) throw new ArithmeticException(
				"Floating point underflow-message too large to encode");
		}
		encodedMessage=(loVal+hiVal)/2;

	}

	public ArithCodeTable codeTable() {
		return table;
	}

	public String encodingProcess() {
		return encodingProcess.toString();
	}

	public double encodedMessage() {
		return encodedMessage;
	}
}
