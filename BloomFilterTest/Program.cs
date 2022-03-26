using Ben.BloomFilter.Utils;
using Ben.HQ;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace BloomFilterTest
{
    class Program
    {
       
        static void Main(string[] args)
        {
            MyBloomFilter myBloomFilter = new MyBloomFilter();
            if (args.Length==0)
            {
                #region  Read start

                ///**********************   Read start
                string path = @"BloomFilter";

                var t = (uint[])BinarySaveWrite.BinaryFileOpen(path);

                MyBloomFilter.Bitset = new BoolArray(t, MyBloomFilter.Size);
                while (true)
                {
                    string path_test = Console.ReadLine();
                    Console.WriteLine(MyBloomFilter.Contains(path_test));
                }
                ///********************************* Read end


                #endregion
            }
            else if (args[0].ToUpper() == "write")
            {
                #region write start

                string path = @"name";
                string pathBloomFilter = "BloomFilter";

                using (StreamReader sr = File.OpenText(path))
                {
                    string s;
                    while ((s = sr.ReadLine()) != null)
                    {
                        MyBloomFilter.Add(s);
                    }
                }

                if (File.Exists(pathBloomFilter))
                {
                    File.Delete(pathBloomFilter);

                    BinarySaveWrite.BinaryFileSav(pathBloomFilter, MyBloomFilter.Bitset.bits);
                }

                #endregion
            }

            Console.Read();

        }

    }
}
