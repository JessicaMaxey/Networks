import java.io.*;
public class RLCompressFile {
  public static void main(String[] args) throws IOException {
    if (args.length==0) {
      System.out.println("Usage: java RLCompressFile <path/file name>.");
      System.exit(1);
    }
    File inFile = new File(args[0]);
    FileInputStream fis = new FileInputStream(inFile);
    byte[] theData = new byte[(int) inFile.length()];
    fis.read(theData);
    fis.close();

    RunLengthEncoder rle=new RunLengthEncoder(theData);
    byte[] cm=rle.compressed();

    File outFile = new File(args[0]+".rle");
    FileOutputStream fos = new FileOutputStream(outFile);
    fos.write(cm);
    fos.close();

    System.out.println("Compression factor: "+100*rle.compressionFactor()+"%");
  }
}