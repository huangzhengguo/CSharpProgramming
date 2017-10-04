using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chapter21
{
    public class MyCancelClass
    {

        // 1.Parallel取消操作
        public static void ParallelCancelMethod()
        {
            var cts = new CancellationTokenSource();
            cts.Token.Register(() =>
            {
                Console.WriteLine("*** token canceled");
            });

            // send a cancel after 500ms
            cts.CancelAfter(500);

            try
            {
                ParallelLoopResult result = Parallel.For(0, 100, new ParallelOptions()
                {
                    CancellationToken = cts.Token
                },
                x =>
                {
                    Console.WriteLine("loop {0} started", x);
                    int sum = 0;
                    for (int i = 0; i < 100; i++)
                    {
                        Thread.Sleep(2);
                        sum += i;
                    }
                    Console.WriteLine("loop {0} finished", x);
                });
            }catch(OperationCanceledException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // 2.Task 取消操作
        public static void CancelTask()
        {
            var cts = new CancellationTokenSource();
            cts.Token.Register(() => { Console.WriteLine("*** task cancelled"); });

            // 500ms后发送取消
            cts.CancelAfter(500);

            Task t1 = Task.Run(() =>
            {
                Console.WriteLine("in task");
                for (int i = 0; i < 20; i++)
                {
                    Thread.Sleep(100);
                    CancellationToken token = cts.Token;
                    if (token.IsCancellationRequested)
                    {
                        Console.WriteLine("cancelling was requested, cancelling from within the task");
                        token.ThrowIfCancellationRequested();

                        break;
                    }

                    Console.WriteLine("task finished without cancellation");
                }
            },
            cts.Token);

            try
            {
                t1.Wait();
            }
            catch (AggregateException ex)
            {
                Console.WriteLine("exception: {0}, {1}", ex.GetType().Name, ex.Message);
                foreach (var innerException in ex.InnerExceptions)
                {
                    Console.WriteLine("inner exception: {0} {1}", ex.InnerException.GetType().Name, ex.InnerException.Message);
                }
            }
        }
    }
}
