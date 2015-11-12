import java.io.*;
public class RLDecompressFile {
  public static void main(String[] args) throws IOException {
    if (args.length==0) {
      System.out.println("Usage: java RLDecompressFile <path/file name>.");
      System.exit(1);
    }
    File inFile = new File(args[0]);
    FileInputStream fis = new FileInputStream(inFile);
    byte[] theData = new byte[(int) inFile.length()];
    fis.read(theData);
    fis.close();

    RunLengthDecoder rld=new RunLengthDecoder(theData);
    byte[] dm=rld.decompressed();

    File outFile = new File(args[0]+".rld");
    FileOutputStream fos = new FileOutputStream(outFile);
    fos.write(dm);
    fos.close();
  }
}