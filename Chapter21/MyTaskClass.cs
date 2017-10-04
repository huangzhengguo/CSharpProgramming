using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chapter21
{
    public class MyTaskClass
    {
        static object taskMethodLock = new object();
        static void TaskMethod(object title)
        {
            lock (taskMethodLock)
            {
                Console.WriteLine(title);
                Console.WriteLine("Task id: {0}, thread: {1}", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
                Console.WriteLine("is pooled thread: {0}", Thread.CurrentThread.IsThreadPoolThread);
                Console.WriteLine("is background thread: {0}", Thread.CurrentThread.IsBackground);
                Console.WriteLine();
            }
        }

        // 1.使用线程池的任务
        public static void TaskUsingThreadPool()
        {
            var tf = new TaskFactory();

            Task t1 = tf.StartNew(TaskMethod, "using a task factory");

            Task t2 = Task.Factory.StartNew(TaskMethod, "factory via a task");

            Task t3 = new Task(TaskMethod,"using a task constructor and start");
            t3.Start();

            Task t5 = Task.Run(() =>
            {
                TaskMethod("using the Run method");
            });
        }

        // 2.同步任务
        public static void RunSynchronousTask()
        {
            TaskMethod("just the main thread");
            var t1 = new Task(TaskMethod, "run async");
            t1.RunSynchronously();
        }

        // 3.使用单独线程的任务
        public static void LongRunningTask()
        {
            var t1 = new Task(TaskMethod, "long running",TaskCreationOptions.LongRunning);

            t1.Start();
        }

        // 4.future：任务的结果
        public static void InvokeTaskWithResult()
        {
            var t1 = new Task<Tuple<int, int>>(TaskWithResult, Tuple.Create<int, int>(8, 3));
            t1.Start();
            Console.WriteLine(t1.Result);
            t1.Wait();
            Console.WriteLine("result from task: {0} {1}", t1.Result.Item1, t1.Result.Item2);
        }

        private static Tuple<int,int> TaskWithResult(object division)
        {
            Tuple<int, int> div = (Tuple<int, int>)division;

            int result = div.Item1 / div.Item2;
            int reminder = div.Item1 % div.Item2;
            Console.WriteLine("task creates a result...");

            return Tuple.Create<int, int>(result, reminder);
        }

        // 5.连续的任务
        public static void DoContinueTask()
        {
            Task t1 = new Task(DoOnFirst);
            t1.Start();
            Task t2 = t1.ContinueWith(DoOnSecond);
            Task t3 = t1.ContinueWith(DoOnSecond);
            Task t4 = t3.ContinueWith(DoOnSecond);
        }

        static void DoOnFirst()
        {
            Console.WriteLine("doing some task {0}", Task.CurrentId);
            Thread.Sleep(3000);
        }

        static void DoOnSecond(Task t)
        {
            Console.WriteLine("task {0} finished", t.Id);
            Console.WriteLine("this task id {0}", Task.CurrentId);
            Console.WriteLine("do some cleaning");
            Thread.Sleep(3000);
        }

        // 6.任务层次结构
        public static void ParentAndChild()
        {
            var parent = new Task(ParentTask);
            parent.Start();

            Thread.Sleep(2000);
            Console.WriteLine(parent.Status);
            Thread.Sleep(4000);
            Console.WriteLine(parent.Status);
        }

        static void ParentTask()
        {
            Console.WriteLine("task id {0}", Task.CurrentId);
            var child = new Task(ChildTask);

            child.Start();
            Thread.Sleep(1000);
            Console.WriteLine("parent is finished");
        }

        static void ChildTask()
        {
            Console.WriteLine("child");
            Thread.Sleep(5000);
            Console.WriteLine("child is finished");
        }
    }
}
