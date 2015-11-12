import java.io.*;
public class HuffCompressFile2 {
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

    inFile = new File(args[0]+".he");
    fis = new FileInputStream(inFile);
    msg = new byte[(int) inFile.length()];
    fis.read(msg);
    fis.close();

    HuffDecoder hd=new HuffDecoder(msg);
    System.out.println(hd.getFrequencyTable().toString());
    System.out.println(hd.getHuffTree().toString());
    System.out.println("Number of bytes when decompressed: "+hd.getUncompressedByteLength());
    byte[] decompressedMsg=hd.decompressed();

    outFile = new File(args[0]+".hd");
    fos = new FileOutputStream(outFile);
    fos.write(decompressedMsg);
    fos.close();
  }
}