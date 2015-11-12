public class HuffEncoder {
  int messageLength;
  byte[] cmsg;
  int cmsgBitLen;

  //Actual compressed msg starts 1032 bytes
  //from beginning of array
  int offset=1032;
  //1024 bytes for frequencies, 
  //4 bytes for compressed msg bit length
  //4 bytes for uncompressed msg byte length

  FrequencyTable ft;
  HuffTree ht;
  HuffCodeTable hct;
  int[] frequencies;

  public HuffEncoder(byte[] msg) {
    ft=new FrequencyTable(msg,false);
    frequencies=ft.frequencies();
    ht=new HuffTree(frequencies);
    hct=new HuffCodeTable(ht);
    messageLength=msg.length;

    //Determine bit length of compressed message
    //by counting length of all codewords in message
    for (int i=0;i<msg.length;i++) 
      cmsgBitLen+=hct.getCode(msg[i]).length();

    //Create a byte array of the size determined above
    cmsg=new byte[(cmsgBitLen-1)/8+1+offset];

    //Write the frequencies,compressed bit length, and
    //uncompressed byte length at start of msg
    for (int i=0;i<256;i++)
      if (frequencies[i]!=0)
        Utilities.intToByteArray(frequencies[i],cmsg,4*i);
    Utilities.intToByteArray(cmsgBitLen,cmsg,1024);
    Utilities.intToByteArray(messageLength,cmsg,1028);

    //Then write each codeword into the byte array
    int bitIndex=0,index=0,codewordIndex=0,pos=offset;
    while (bitIndex<cmsgBitLen) {
      String code=hct.getCode(msg[codewordIndex]);
      index=0;
      while (index<code.length()) {
        pos=bitIndex++/8+offset;
        cmsg[pos]=(byte)(2*cmsg[pos]+(code.charAt(index++)=='1'?1:0));
      }
      codewordIndex++;
    }
    //Last byte may need to be shifted left
    if (cmsgBitLen%8!=0) for (int i=0;i<8-cmsgBitLen%8;i++) cmsg[pos]=(byte)(2*cmsg[pos]);
  }

  public FrequencyTable getFrequencyTable() {return ft;}
  public HuffTree getHuffTree() {return ht;}
  public HuffCodeTable getHuffCodeTable() {return hct;}
  public byte[] compressed() {return cmsg;}
  public int compressedBitLength() {return cmsgBitLen;}
  public double compressionFactor() {return 1-(double)cmsg.length/messageLength;}
}