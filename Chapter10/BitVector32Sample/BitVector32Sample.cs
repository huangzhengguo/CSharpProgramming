using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter10.BitVector32Sample
{
    public class BitVector32Samples
    {
        public static void BitVector32SampleMethod()
        {
            var bits1 = new BitVector32();
            int bit1 = BitVector32.CreateMask();
            int bit2 = BitVector32.CreateMask(bit1);
            int bit3 = BitVector32.CreateMask(bit2);
            int bit4 = BitVector32.CreateMask(bit3);
            int bit5 = BitVector32.CreateMask(bit4);

            bits1[bit1] = true;
            bits1[bit2] = false;
            bits1[bit3] = true;
            bits1[bit4] = true;
            bits1[bit5] = true;

            // Console.WriteLine(bit5);
            int received = 0x79abcdef;
            var bits2 = new BitVector32(received);
            Console.WriteLine(bits2);

            // 创建片段:FF EEE DDD CCCC BBBBBBBB AAAAAAAAAAAA
            BitVector32.Section sectionA = BitVector32.CreateSection(0xfff);
            BitVector32.Section sectionB = BitVector32.CreateSection(0xff, sectionA);
            BitVector32.Section sectionC = BitVector32.CreateSection(0xf, sectionB);
            BitVector32.Section sectionD = BitVector32.CreateSection(0x7, sectionC);
            BitVector32.Section sectionE = BitVector32.CreateSection(0x7, sectionD);
            BitVector32.Section sectionF = BitVector32.CreateSection(0x3, sectionE);

            Console.WriteLine("Section A: {0}", IntToBinaryString(bits2[sectionA], false));
            Console.WriteLine("Section B: {0}", IntToBinaryString(bits2[sectionB], false));
            Console.WriteLine("Section C: {0}", IntToBinaryString(bits2[sectionC], false));
            Console.WriteLine("Section D: {0}", IntToBinaryString(bits2[sectionD], false));
            Console.WriteLine("Section E: {0}", IntToBinaryString(bits2[sectionE], false));
            Console.WriteLine("Section F: {0}", IntToBinaryString(bits2[sectionF], false));
        }

        static string IntToBinaryString(int bits,bool removeTrailingZero)
        {
            var sb = new StringBuilder(32);
            for(int i = 0; i < 32; i++)
            {
                if ((bits & 0x80000000) != 0)
                {
                    sb.Append("1");
                }
                else
                {
                    sb.Append("0");
                }

                bits = bits << 1;
            }

            string s = sb.ToString();
            if (removeTrailingZero)
            {
                return s.TrimStart('0');
            }
            else
            {
                return s;
            }
        }
    }
}
