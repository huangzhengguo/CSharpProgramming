using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter15
{
    class Program
    {
        static void Main(string[] args)
        {
            Type t = typeof(double);
            TypeView.AnalyzeType(t);
        }
    }
}
