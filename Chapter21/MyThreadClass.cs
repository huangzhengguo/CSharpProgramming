using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chapter21
{
    public class MyThreadClass
    {
        // 1.Thread创建新线程
        public static void CreateNewThread()
        {
            var t1 = new Thread(ThreadMain);
            t1.Start();
            Console.WriteLine("This is the main thread.");
        }

        static void ThreadMain()
        {
            Console.WriteLine("Running in a thread.");
        }

        // 2.给线程传递数据
        // 1)使用带ParameterizedThreadStart委托参数的Thread参数
        // 2)创建一个自定义类，把线程的方法定义为实例方法，这样就可以初始化实例的数据，只后启动线程
        public struct Data
        {
            public string Message;
        }

        public static void ParameterMethod()
        {
            // 通过使用参数
            var d = new Data { Message = "Info" };
            var t2 = new Thread(ThreadMainWithParameters);
            t2.Start(d);

            // 通过定义类
            var obj = new MyThread("定义类实现");
            var t3 = new Thread(obj.ThreadMain);
            t3.Start();
        }

        static void ThreadMainWithParameters(object o)
        {
            Data d = (Data)o;
            Console.WriteLine("Running in a thread, received {0}", d.Message);
        }

        // 定义类
        public class MyThread
        {
            private string data;
            public MyThread(string data)
            {
                this.data = data;
            }

            public void ThreadMain()
            {
                Console.WriteLine("Running in a thread, data: {0}", data);
            }
        }

        // 3.后台线程
        public static void BackgroundThread()
        {
            var t1 = new Thread(ThreadMain2)
            {
                Name = "MyNewThread",
                IsBackground = false
            };
            t1.Start();
            Console.WriteLine("Main thread ending now.");
        }

        static void ThreadMain2()
        {
            Console.WriteLine("Thread {0} started", Thread.CurrentThread.Name);
            Thread.Sleep(3000);
            Console.WriteLine("Thread {0} completed", Thread.CurrentThread.Name);
        }

        // 线程问题
        // 1.争用条件
        public class StateObject
        {
            private int state = 5;
            public void ChangeState(int loop)
            {
                if (state == 5)
                {
                    state++;
                    Trace.Assert(state == 6, "Race condition occurred after " + loop + " loops");
                }
                state = 5;
            }
        }

        public class SampleTask
        {
            public void RaceCondition(object o)
            {
                Trace.Assert(o is object, "o must be of type StateObject");
                StateObject state = o as StateObject;

                int i = 0;
                while (true)
                {
                    lock (state)
                    {
                        state.ChangeState(i++);
                    }
                }
            }
        }

        public static void RaceCondition()
        {
            var state = new StateObject();
            for (int i = 0; i < 2; i++)
            {
                Task.Run(() => new SampleTask().RaceCondition(state));
            }
        }
    }
}
