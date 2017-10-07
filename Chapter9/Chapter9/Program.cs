using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter9
{
    class Program
    {
        static void Main(string[] args)
        {
            // EncodeString();
            // FormatStringMethod();

            //FormatForStruct();

            RegularExpressionPlayaround.FindIon();
            Console.Read();
        }

        // 为类添加格式化方法
        static void FormatForStruct()
        {
            Vector v1 = new Vector(1, 32, 5);
            Vector v2 = new Vector(845.4, 54.3, -7.8);
            Console.WriteLine("\n In IJK fomat \n v1 is {0,30:ijk} \n v2 is {1,30:ijk}", v1, v2);

            Console.WriteLine("\n In default fomat \n v1 is {0,30} \n v2 is {1,30}", v1, v2);
            Console.WriteLine("\n In VE fomat \n v1 is {0,30:ve} \n v2 is {1,30:ve}", v1, v2);
            Console.WriteLine("\n Norms \n v1 is {0,30:n} \n v2 is {1,30:n}", v1, v2);
        }

        // 格式化字符串
        static void FormatStringMethod()
        {
            double d = 13.45;
            int i = 45;
            Console.WriteLine("The double is {0,10:E} and the int is {1,4:0000}", d, i);
        }

        // 简单加密字符串
        static void EncodeString()
        {
            string greetingText = "Hello from all the guys at Wrox Press. ";
            greetingText += "We do hope you enjoy this book as much as we enjoyed writing it.";

            Console.WriteLine("start1:{0}",DateTime.Now.ToLongTimeString());
            for(int i = 'z'; i >= 'a'; i--)
            {
                char old1 = (char)i;
                char new1 = (char)(i+1);
                greetingText = greetingText.Replace(old1, new1);
            }

            for(int i = 'Z'; i >= 'A'; i--)
            {
                char old2 = (char)i;
                char new2 = (char)(i+1);
                greetingText = greetingText.Replace(old2, new2);
            }
            Console.WriteLine("start1:{0}", DateTime.Now.ToLongTimeString());
            Console.WriteLine(greetingText);

            StringBuilder greetingBuilder = new StringBuilder("Hello from all the guys at Wrox Press. ");
            greetingBuilder.AppendFormat("We do hope you enjoy this book as much as we enjoyed writing it.");
            Console.WriteLine("start1:{0}", DateTime.Now.ToLongTimeString());
            for (int i = 'z'; i >= 'a'; i--)
            {
                char old1 = (char)i;
                char new1 = (char)(i + 1);
                greetingBuilder = greetingBuilder.Replace(old1, new1);
            }

            for (int i = 'Z'; i >= 'A'; i--)
            {
                char old2 = (char)i;
                char new2 = (char)(i + 1);
                greetingBuilder = greetingBuilder.Replace(old2, new2);
            }
            Console.WriteLine("start1:{0}", DateTime.Now.ToLongTimeString());
        }
    }
}
