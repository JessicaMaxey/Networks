/*Class to determine frequency of individual byte 
values in a byte array.  These frequencies can be
used to determine the behaviour of a compression 
scheme.
*/
public class FrequencyTable {
  //There are 256 possible byte values; each slot in 
  //array determines the frequency of that value in 
  //a byte array
  int[] frequencies=new int[256];

  public FrequencyTable(byte[] msg,boolean freqsInFront) {
    //If this message is compressed, the frequencies
    //may be simply stored in the front 1024 bytes
    if (freqsInFront) 
      for (int i=0;i<256;i++)
        frequencies[i]=Utilities.intFromByteArray(msg,4*i);
    //If not, count the frequency of each byte
    else for (int i=0;i<msg.length;i++)
      frequencies[(msg[i]+256)%256]++;
  }

  //Return the frequencies as an int array
  public int[] frequencies() {return frequencies;}

  //Display the frequencies
  public String toString() {
    StringBuffer out=new StringBuffer("Byte\tFrequency\n");
    for (int i=0;i<frequencies.length;i++)
      if (frequencies[i]!=0)
        out.append(i+"\t"+frequencies[i]+"\n");
    return out.toString();
  }
}