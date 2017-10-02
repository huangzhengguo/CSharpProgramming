using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter10.LinkedListSample
{
    public class Documents
    {
        public string Title { get; private set; }
        public string Content { get; private set; }
        public byte Priority{ get; private set; }

        public Documents(string titile,string content,byte priority)
        {
            this.Title = titile;
            this.Content = content;
            this.Priority = priority;
        }
    }
}
