import java.io.*;
public class RunLengthCodeTest {
  static BufferedReader keyboard=new BufferedReader(new InputStreamReader(System.in));
  public static void main(String[] args) throws IOException {
    System.out.print("Enter a message: ");
    String message=keyboard.readLine();
    RunLengthEncoder rle=new RunLengthEncoder(message.getBytes());
    byte[] cm=rle.compressed();
    System.out.println("The compressed message follows-each signed byte in base 10:");
    for (int i=0;i<cm.length;i++) System.out.print(" "+(int)cm[i]);
    System.out.println("\nThe message was compressed by "+rle.compressionFactor()*100+"%");
    RunLengthDecoder rld=new RunLengthDecoder(cm);
    String recovered=new String(rld.decompressed());
    System.out.println("Decompressed message: "+recovered);
    if (message.compareTo(recovered)==0) System.out.println(
		"The decompressed message matches the original.");
  }
}