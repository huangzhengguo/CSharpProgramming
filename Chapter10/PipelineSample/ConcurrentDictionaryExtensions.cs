using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter10.PipelineSample
{
    public static class ConcurrentDictionaryExtensions
    {
        public static void AddOrIncrementValue(this ConcurrentDictionary<string,int> dict,string key)
        {
            bool success = false;
            while (!success)
            {
                if (dict.TryGetValue(key, out int values))
                {
                    if (dict.TryUpdate(key, values + 1, values))
                    {
                        success = true;
                    }
                }
                else
                {
                    if (dict.TryAdd(key, 1))
                    {
                        success = true;
                    }
                }
            }
        }
    }
}
