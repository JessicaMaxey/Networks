import java.io.*;
public class HuffDecompressFile {
  public static void main(String[] args) throws IOException {
    if (args.length==0) {
      System.out.println("Usage: java HuffDecompressFile <path/file>");
      System.exit(1);
    }

    File inFile = new File(args[0]);
    FileInputStream fis = new FileInputStream(inFile);
    byte[] msg = new byte[(int) inFile.length()];
    fis.read(msg);
    fis.close();

    HuffDecoder hd=new HuffDecoder(msg);
    System.out.println(hd.getFrequencyTable().toString());
    System.out.println(hd.getHuffTree().toString());
    System.out.println("Number of bytes when decompressed: "+hd.getUncompressedByteLength());

    File outFile = new File(args[0]+".hd");
    FileOutputStream fos = new FileOutputStream(outFile);
    fos.write(hd.decompressed());
    fos.close();
  }
}