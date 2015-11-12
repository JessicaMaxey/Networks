import java.io.*;
public class ArithCodeTest {
    static BufferedReader keyboard=new BufferedReader(new InputStreamReader(System.in));

    public static void main(String[] args) throws IOException {

	//User inputs a message string from a dialog box
	System.out.print("Enter a message: ");
	String smsg=keyboard.readLine();

	System.out.println("Message before encoding: "+smsg);

	//Message is converted to an array of bytes
	byte[] msg=smsg.getBytes();

	//Create the Arithmetic Encoder
	ArithEncoder ae=null;
	try {
		ae=new ArithEncoder(msg);

		//Display the code table, encoding process, and encoded message
		ArithCodeTable act=ae.codeTable();
		System.out.println("Interval Distribution:");
		System.out.println(act.toString());
		System.out.println("Encoding table:");
		System.out.println(ae.encodingProcess());
		System.out.println("Encoded Message: "+ae.encodedMessage());

	} catch (Exception oops) {
		oops.printStackTrace();
		System.exit(1);
	}
	System.exit(0);
    }
}