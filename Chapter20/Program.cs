using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace Chapter20
{
    class Program
    {
        static void Main(string[] args)
        {
            MinMax(3, 2);

            Preconditions("null");
            Console.ReadLine();
        }

        static void MinMax(int min, int max)
        {
            Contract.Requires(min <= max);
        }

        static void Preconditions(object o)
        {
            Contract.Requires<ArgumentNullException>(o != null, "Procondetions, o may not be null");
        }
    }
}
