public class HuffCodeTable {
  HuffNode root;
  String[] codewords=new String[256];

  public HuffCodeTable(HuffTree t) {
    root=t.tree;
    String code=new String();
    buildCodes(root,code);
  }

  public void buildCodes(HuffNode t, String code) {
    if (t!=null) {
      if (t.symbol==-1) {
        buildCodes(t.left,code+"0");
        buildCodes(t.right,code+"1");
      } else codewords[t.symbol]=code;
    }
  }

  public String getCode(byte symbol) {
    return codewords[(symbol+256)%256];
  }

  public String toString() {
    StringBuffer sb=new StringBuffer("Byte\tCodeword\n");
    for (int i=0;i<codewords.length;i++)
      if (codewords[i]!=null)
        sb.append(i+"\t"+getCode((byte)i)+"\n");
    return sb.toString();
  }
}