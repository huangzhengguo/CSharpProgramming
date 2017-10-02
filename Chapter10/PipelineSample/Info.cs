using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter10.PipelineSample
{
    public class Info
    {
        public string Word { get; set; }
        public int Count { get; set; }
        public string Color { get; set; }

        public override string ToString()
        {
            return string.Format("Word :{0},Count:{1},Color:{2}", Word, Count, Color);
        }
        /*
        public Info(string word,int count)
        {
            this.Word = word;
            this.Count = count;
        }*/
    }
}
