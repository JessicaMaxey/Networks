using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoapServer
{
    class DictionaryCompression
    {

        Dictionary<int, byte[]> dict = new Dictionary<int, byte[]>();
        byte[] m_data = null;

        private DictionaryCompression (byte[] data) { m_data = data; }

        public void Compression ()
        {
            /*
            
            
            */
            byte[] p = null;
            byte c = 0;



            for (int i = 0; i < m_data.Length; i++)
            {
                //read in char
                c = m_data[i];

                //combine
                var new_entry = new byte[p.Length + 1];
                Buffer.BlockCopy(p, 0, new_entry, 0, p.Length);
                new_entry[p.Length] = c;

                //check to see if it is in dict
                if (dict.ContainsValue(new_entry))
                {

                }

            }

        }

        public void Decompression()
        {

        }
    }
}
