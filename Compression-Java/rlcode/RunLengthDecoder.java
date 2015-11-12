import java.util.*;
public class RunLengthDecoder {
  Vector dmsgv=new Vector();
  int index=0;
  byte[] msg;

  public RunLengthDecoder(byte[] msg) {
    this.msg=msg;
    while (index<msg.length) {
      if (msg[index]==0) {
        processTheRun();
        index+=3;
      } else {
        dmsgv.addElement(new Byte(msg[index++]));
      }
    }
  }

  private void processTheRun() {
    Byte theValue=new Byte(msg[index+1]);
    int runlen=((int)msg[index+2]+256)%256;
    for (int i=0;i<=runlen;i++) dmsgv.addElement(theValue);
  }

  public byte[] decompressed() {
    int size=dmsgv.size();
    byte[] dmsg=new byte[size];
    for (int i=0;i<size;i++) dmsg[i]=((Byte)dmsgv.elementAt(i)).byteValue();
    return dmsg;
  }
}