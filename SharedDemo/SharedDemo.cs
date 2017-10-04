using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Assembiles
{
    public class SharedDemo
    {
        private string[] quotes;
        private Random random;

        public SharedDemo(string filename)
        {
            quotes = File.ReadAllLines(filename);
            random = new Random();
        }

        public string GetQouteOfTheDay()
        {
            int index = random.Next(1, quotes.Length);

            return quotes[index];
        }
    }
}
