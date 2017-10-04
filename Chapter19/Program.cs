using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter19
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Main in domin {0} called", AppDomain.CurrentDomain.FriendlyName);

            // 调用Chapter 11
            AppDomain chapter10Domain = AppDomain.CreateDomain("chanper10");

            // 直接执行程序集
            // chapter10Domain.ExecuteAssembly("Chapter10.exe");
            chapter10Domain.CreateInstance("Chapter10", "Chapter10.Person", 
                true, 
                System.Reflection.BindingFlags.CreateInstance, 
                null, 
                new object[] { "Person" }, null, null);
            Console.ReadLine();
        }
    }

    public class Demo
    {
        public Demo(int val1, int val2)
        {
            Console.WriteLine("Constructor with the values{0}, {1} in domain {2} called", val1, val2, AppDomain.CurrentDomain.FriendlyName);
        }
    }
}
