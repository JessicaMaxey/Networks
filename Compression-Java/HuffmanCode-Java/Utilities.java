public class Utilities {
  //Write an int into a byte array, starting at
  //startIndex, Big-Endian order
  public static void intToByteArray(int number,
      byte[] array,int startIndex) {
    array[startIndex+3]=(byte)number;
    array[startIndex+2]=(byte)(number>>>8);
    array[startIndex+1]=(byte)(number>>>16);
    array[startIndex]=(byte)(number>>>24);
  }

  //Read an int from a byte array, starting at
  //specified startIndex, stored in Big-Endian order
  public static int intFromByteArray(byte[] array,
      int startIndex) {
    int result=0;
    for (int i=startIndex;i<startIndex+4;i++) result=(result<<8)+((array[i]+256)%256);
    return result;
  }
}