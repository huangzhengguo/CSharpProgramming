using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter10
{
    [Serializable]
    public class Person
    {
        private string name;
        public Person(string name)
        {
            Console.WriteLine("实例化person类");
            this.name = name;
        }

        public override string ToString()
        {
            return this.name;
        }
    }
}
