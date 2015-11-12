import java.util.*;
public class RunLengthEncoder {
  Vector cmsgv=new Vector();
  Byte leadRunSymbol=new Byte((byte)0);
  Byte maxRunSymbol=new Byte((byte)255);
  int runlen=0;
  int theByte;
  byte[] msg;

  public RunLengthEncoder(byte[] msg) {
    this.msg=msg;
    int index=0;
    theByte=msg[0];
    while(index<msg.length-1) {
      if (theByte==msg[index+1]) runlen++;
      else {
        processTheRun();
        runlen=0;
      }
      theByte=msg[++index];
    }
    processTheRun();
  }

  private void processTheRun() {
    Byte theSymbol=new Byte((byte)theByte);
    for (int i=0;i<runlen/256;i++) {
      cmsgv.addElement(leadRunSymbol);
      cmsgv.addElement(theSymbol);
      cmsgv.addElement(maxRunSymbol);
    }
    runlen%=256;
    if (theByte==0||runlen>2) {
      cmsgv.addElement(leadRunSymbol);
      cmsgv.addElement(theSymbol);
      cmsgv.addElement(new Byte((byte)runlen));
    } else {
      Byte symbol=new Byte((byte)theByte);
      for (int i=0;i<=runlen;i++) cmsgv.addElement(symbol);
    }
  }

  public byte[] compressed() {
    int size=cmsgv.size();
    byte[] cmsg=new byte[size];
    for (int i=0;i<size;i++) cmsg[i]=((Byte)cmsgv.elementAt(i)).byteValue();
    return cmsg;
  }

  public double compressionFactor() {
    return 1-(double)cmsgv.size()/msg.length;
  }
}