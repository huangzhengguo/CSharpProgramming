using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Chapter10.ObservableCollectionSample
{
    public class ObservableCollectionSample
    {
        public static void ObservableCollectionSampleMethod()
        {
            var data = new ObservableCollection<string>();

            data.CollectionChanged += Data_CollectionChanged;

            data.Add("one");
            data.Add("one");
            data.Insert(1, "three");
            data.Remove("one");
        }

        static void Data_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Console.WriteLine("action: {0}", e.Action.ToString());

            if (e.OldItems != null)
            {
                Console.WriteLine("starting index for old item(s): {0}", e.OldStartingIndex);
                foreach(var item in e.OldItems)
                {
                    Console.WriteLine(item);
                }
            }
            if (e.NewItems != null)
            {
                Console.WriteLine("starting index for new item(s): {0}", e.NewStartingIndex);
                foreach (var item in e.NewItems)
                {
                    Console.WriteLine(item);
                }
            }
            Console.WriteLine();
        }
    }
}
