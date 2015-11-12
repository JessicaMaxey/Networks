import java.io.*;
public class HuffCodeTest {
  static BufferedReader keyboard=new BufferedReader(
    new InputStreamReader(System.in));

  public static void main(String[] args) throws IOException {
    System.out.print("Enter a message: ");
    String msgString=keyboard.readLine();
    byte[] msg=msgString.getBytes();

    HuffEncoder he=new HuffEncoder(msg);
    System.out.println("Frequencies:\n"+he.getFrequencyTable().toString());
    System.out.println("Huffman tree:\n"+he.getHuffTree().toString());
    System.out.println("Code Table:\n"+he.getHuffCodeTable().toString());
    byte[] compressedMessage=he.compressed();
    StringBuffer out=new StringBuffer("");
    System.out.println("Bit length of compressed message: "
      +he.compressedBitLength());
    System.out.print("Compressed message (including frequencies): ");
    for (int i=0;i<compressedMessage.length;i++) {
      String temp=Integer.toHexString(
        (compressedMessage[i]+256)%256);
      out.append(temp.length()==1?"0"+temp:temp);
      out.append(" ");
    }
    System.out.println(out.toString());    

    HuffDecoder hd=new HuffDecoder(compressedMessage);
    System.out.println("Frequencies:\n"+hd.getFrequencyTable().toString());
    System.out.println("Huffman tree:\n"+hd.getHuffTree().toString());
    byte[] decompressedMessage=hd.decompressed();
    System.out.println("Byte length of decompressed message: "
      +decompressedMessage.length);
    System.out.print("Decompressed message: "+new String(decompressedMessage));

    System.exit(0);
  }
}