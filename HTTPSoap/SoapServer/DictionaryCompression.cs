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
        byte[] m_datab = null;
        List<int> m_datai = new List<int>();
        List<int> compressed_data = new List<int>();
        List<byte[]> decompressed_data = new List<byte[]>();

        public Dictionary<int, byte[]> Dictionary
        {
            get
            {
                return dict;
            }

            set
            {
                dict = value;
            }
        }

        public List<int> CompressedData
        {
            get
            {
                return compressed_data;
            }

            set
            {
                compressed_data = value;
            }
        }

        public List<byte[]> DecompressedData
        {
            get
            {
                return decompressed_data;
            }
            set
            {
                decompressed_data = value;
            }
        }

        public DictionaryCompression (byte[] data) { m_datab = data; }
        public DictionaryCompression (List<int> data) { m_datai = data; }

        public void Compression()
        {
            byte[] p = null;
            byte c = 0;
            
            for (int i = 0; i < m_datab.Length + 1; i++)
            {
                //combine
                if (i != m_datab.Length)
                {
                    //read in char
                    c = m_datab[i];

                    if (p != null)
                    {
                        var new_entry = new byte[p.Length + 1];
                        Buffer.BlockCopy(p, 0, new_entry, 0, p.Length);
                        new_entry[p.Length] = c;
                        bool answer = false;

                        //check to see if dictionary contains the byte array already
                        for (int j = 0; j < dict.Count(); j++)
                        {
                            answer = dict[j].SequenceEqual(new_entry);

                            if (answer == true)
                                break;
                        }

                        //check to see if p is in dict
                        if (answer == true)
                        {
                            //p now = p + c
                            p = new_entry;

                        }
                        else
                        {
                            int code_word = 0;
                            //check to see if p is already in the dictionary, if not add it
                            //so when we add p to the codestream it will have it's codeword
                            //that way we can decrypt it
                            for (int j = 0; j < dict.Count(); j++)
                            {
                                answer = dict[j].SequenceEqual(p);

                                if (answer == true)
                                {
                                    code_word = j;
                                    break;
                                }
                            }

                            //if it doesn't already exsist, add it
                            if (answer == false)
                            {
                                dict.Add(dict.Count(), p);
                                code_word = dict.Count() - 1;
                            }

                            //p gets added to the codestream (compressed data string)
                            compressed_data.Add(code_word);


                            //add p+c to dictionary
                            dict.Add(dict.Count(), new_entry);

                            //now set p = c
                            p = new byte[1];
                            p[0] = c;
                        }
                    }
                    else
                    {
                        //now set p = c
                        p = new byte[1];
                        p[0] = c;

                        //add p+c to dictionary
                        dict.Add(dict.Count(), p);

                        //p gets added to the codestream (compressed data string)
                        //compressed_data.Add(dict.Count() - 1);

                    }

                }
                else
                {
                    bool answer = false;
                    int code_word = 0;
                    //check to see if p is already in the dictionary, if not add it
                    //so when we add p to the codestream it will have it's codeword
                    //that way we can decrypt it
                    for (int j = 0; j < dict.Count(); j++)
                    {
                        answer = dict[j].SequenceEqual(p);

                        if (answer == true)
                        {
                            code_word = j;
                            break;
                        }
                    }

                    //if it doesn't already exsist, add it
                    if (answer == false)
                    {
                        dict.Add(dict.Count(), p);
                        code_word = dict.Count() - 1;
                    }

                    //p gets added to the codestream (compressed data string)
                    compressed_data.Add(code_word);
                }
            }
        }

        public void Decompression()
        {
            byte[] answer = { };
            int cw = 0;

            for (int i = 0; i < m_datai.Count; i++)
            {
                //get the codeword
                cw = (int)m_datai[i];

                //find codeword in dictionary
                answer = dict[cw];

                //output that data to charstream
                decompressed_data.Add(answer);
            }

        }
    }
}
