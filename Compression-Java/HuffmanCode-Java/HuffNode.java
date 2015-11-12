public class HuffNode implements Comparable {
  int frequency=0;
  int symbol=-1;
  HuffNode left=null,right=null;

  public int compareTo(Object other) {
    HuffNode h=(HuffNode) other;
    if (this.frequency==h.frequency)
      return this.symbol-h.symbol;
    else return this.frequency-h.frequency;
  }
}