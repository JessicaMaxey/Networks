import java.io.*;
public class HuffCompressFile {
  public static void main(String[] args) throws IOException {
    if (args.length==0) {
      System.out.println("Usage: java HuffCompressFile <path/file>");
      System.exit(1);
    }

    File inFile = new File(args[0]);
    FileInputStream fis = new FileInputStream(inFile);
    byte[] msg = new byte[(int) inFile.length()];
    fis.read(msg);
    fis.close();

    HuffEncoder he=new HuffEncoder(msg);
    byte[] cm=he.compressed();
    System.out.println(he.getFrequencyTable().toString());
    System.out.println(he.getHuffTree().toString());
    System.out.println(he.getHuffCodeTable().toString());
    System.out.println("Bit length of compressed message: "+he.compressedBitLength());

    File outFile = new File(args[0]+".he");
    FileOutputStream fos = new FileOutputStream(outFile);
    fos.write(cm);
    fos.close();

    System.out.println("Compression factor: "+100*he.compressionFactor()+"%");

  }
}