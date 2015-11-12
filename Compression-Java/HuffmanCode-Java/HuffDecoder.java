public class HuffDecoder {
  int messageLength;
  int cmsgBitLength;
  HuffTree ht;
  FrequencyTable ft;
  byte[] cmsg;
  byte[] msg;

  public HuffDecoder(byte cmsg[]) {
    this.cmsg=cmsg;
    ft=new FrequencyTable(cmsg,true);
    ht=new HuffTree(ft.frequencies());
    cmsgBitLength=Utilities.intFromByteArray(cmsg,1024);
    messageLength=Utilities.intFromByteArray(cmsg,1028);
    msg=new byte[messageLength];

    HuffNode theNode=ht.tree;
    int shifty=0;
    int index=0;
    for (int counter=0;counter<cmsgBitLength;counter++) {
      if (counter%8==0) shifty=((cmsg[1032+counter/8]+256)%256);
      shifty=shifty<<1;
      int theBit=shifty/256%2;
      theNode=ht.advanceAndGetSymbol(theNode,theBit);
      if (theNode.symbol!=-1) {
        msg[index]=(byte)theNode.symbol;
        index++;
        theNode=ht.tree;
      }
    }
  }

  public FrequencyTable getFrequencyTable() {return ft;}
  public HuffTree getHuffTree() {return ht;}
  public int getUncompressedByteLength() {return messageLength;}
  public byte[] decompressed() {return msg;}

}