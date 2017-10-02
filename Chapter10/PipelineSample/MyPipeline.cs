using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter10.PipelineSample
{
    public class MyPipeline
    {
        public static async void StartPipeline()
        {
            var fileNames = new BlockingCollection<string>();
            var lines = new BlockingCollection<string>();
            var words = new ConcurrentDictionary<string, int>();
            var items = new BlockingCollection<Info>();
            var coloredItems = new BlockingCollection<Info>();

            Task t1 = PipelineStages.ReadFilenamesAsync(@"../../..", fileNames);
            ConsoleHelper.WriteLine("started stage 1");

            Task t2 = PipelineStages.LoadContentAsync(fileNames, lines);
            ConsoleHelper.WriteLine("started stage 2");

            Task t3 = PipelineStages.ProcessContentAsync(lines, words);
            await Task.WhenAll(t1, t2, t3);

            ConsoleHelper.WriteLine("stages 1 2 3 completed.");

            Task t4 = PipelineStages.TransferContentAsync(words, items);
            Task t5 = PipelineStages.AddColorAsync(items, coloredItems);
            Task t6 = PipelineStages.ShowContentAsync(coloredItems);

            ConsoleHelper.WriteLine("stages 4 5 6 completed.");
            await Task.WhenAll(t4, t5, t6);
            ConsoleHelper.WriteLine("All stages completed.");

        }
    }
}
