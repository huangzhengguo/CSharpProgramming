using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chapter21
{
    public class MyThreadPoolClass
    {
        public static void ThreadPoolMethod()
        {
            int nWorkerThread;
            int nCompletionPortThreads;
            ThreadPool.GetMaxThreads(out nWorkerThread, out nCompletionPortThreads);

            Console.WriteLine("Max worker threads: {0}, I/O completion threads: {1}", nWorkerThread, nCompletionPortThreads);

            for (int i = 0; i < 5; i++)
            {
                ThreadPool.QueueUserWorkItem(JobForAThread);
            }

            Thread.Sleep(3000);
        }

        static void JobForAThread(object state)
        {
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("loop {0},running inside pooled thread {1}", i, Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(50);
            }
        }
    }
}
