import java.util.Vector;
public class HuffTree {
  HuffNode tree;
  public HuffTree(int[] frequencies) {
    Vector nodes=createSortedVector(frequencies);
    while(nodes.size()>1) {
      HuffNode left=(HuffNode)nodes.remove(0);
      HuffNode right=(HuffNode)nodes.remove(0);
      HuffNode root=new HuffNode();
      root.frequency=left.frequency+right.frequency;
      root.left=left;
      root.right=right;
      insertInOrder(nodes,root);
    }
    tree=(HuffNode)nodes.elementAt(0);
  }

  private Vector createSortedVector(int[] freqs) {
    Vector v=new Vector();
    for (int i=0;i<freqs.length;i++) {
      if (freqs[i]!=0) {
        HuffNode hn=new HuffNode();
        hn.symbol=i;
        hn.frequency=freqs[i];
        insertInOrder(v,hn);
      }
    }
    return v;
  }

  private void insertInOrder(Vector v, HuffNode h) {
    int j=-1;
    while (++j<v.size()
      && h.compareTo((HuffNode)v.elementAt(j))>0);
    v.insertElementAt(h,j);
  }

  public String toString() {
    StringBuffer sb=new StringBuffer();
    sb.append("Symbol\tfrequency\n");
    travSubTree(tree,sb);
    return sb.toString();
  }

  private void travSubTree(HuffNode t,StringBuffer sb) {
    if (t!=null) {
      sb.append(t.symbol+"\t"+t.frequency+"\n");
      travSubTree(t.left,sb);
      travSubTree(t.right,sb);
    }
  }

  public HuffNode advanceAndGetSymbol(HuffNode hn,int goRight) {
    return (goRight==1)?hn.right:hn.left; 
  }

}