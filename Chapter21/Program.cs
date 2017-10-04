using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chapter21
{
    class Program
    {
        static void Main(string[] args)
        {
            // ParallelMethod();
            // ParallelBreakMethod();
            // ParallelInitMethod();
            // ParallelForeachMethod();
            // ParallelInvokeMethod();
            // MyTaskClass.TaskUsingThreadPool();
            // MyTaskClass.RunSynchronousTask();
            // MyTaskClass.LongRunningTask();
            // MyTaskClass.InvokeTaskWithResult();
            // MyTaskClass.DoContinueTask();
            // MyTaskClass.ParentAndChild();
            // MyCancelClass.ParallelCancelMethod();
            // MyCancelClass.CancelTask();
            // MyThreadPoolClass.ThreadPoolMethod();
            // MyThreadClass.CreateNewThread();
            // MyThreadClass.ParameterMethod();
            MyThreadClass.BackgroundThread();


            Console.ReadLine();
        }

        // 1.多线程for循环
        static void ParallelMethod()
        {
            ParallelLoopResult result = Parallel.For(0, 9, async i =>
            {
                Console.WriteLine("i={0}, task:{1},thread:{2}", i, Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
                await Task.Delay(10);
                Console.WriteLine("i={0}, task:{1},thread:{2}", i, Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
            });

            Console.WriteLine("Is completed: True");
        }

        // 2.中断多线程循环
        static void ParallelBreakMethod()
        {
            ParallelLoopResult result = Parallel.For(10, 60, async (i, pls) =>
              {
                  Console.WriteLine("i: {0} task: {1} ", i, Task.CurrentId);
                  await Task.Delay(10);
                  if (i > 15)
                  {
                      pls.Break();
                  }
              });

            Console.WriteLine("Is completed:{0}", result.IsCompleted);
            Console.WriteLine("lowest break iteration: {0}", result.LowestBreakIteration);
        }

        // 3.线程初始化
        static void ParallelInitMethod()
        {
            Parallel.For<string>(0, 20, () =>
            {
                // Invoked once for each thread
                Console.WriteLine("init thread {0}, task {1}", Thread.CurrentThread.ManagedThreadId, Task.CurrentId);
                return string.Format("t{0}", Thread.CurrentThread.ManagedThreadId);
            },
            (i, pls, str1) =>
            {
                // invoked for each memeber
                Console.WriteLine("body i {0} str1 {1} thread {2} task {3}", i, str1, Thread.CurrentThread.ManagedThreadId, Task.CurrentId);
                Thread.Sleep(10);

                return string.Format("i {0}", i);
            },
            (str1) =>
            {
                // final action on each thread
                Console.WriteLine("final {0}", str1);
            });
        }

        // 4.Foreach
        static void ParallelForeachMethod()
        {
            string[] data = { "zero", "one", "two", "three", "four", "five", "six", "seven","eight","nine","ten","evleven","tweleven" };
            ParallelLoopResult result = Parallel.ForEach<string>(data, str =>
            {
                Console.WriteLine(str);
            });

            result = Parallel.ForEach<string>(data, (s, pls, index) =>
            {
                Console.WriteLine("第{0}个数据 {1}", index, s);
            });
        }

        // 5.并行调用多个方法
        static void ParallelInvokeMethod()
        {
            Parallel.Invoke(Foo, Bar);
        }

        static void Foo()
        {
            Console.WriteLine("Foo");
        }

        static void Bar()
        {
            Console.WriteLine("Bar");
        }
    }
}
