using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chapter21
{
    public class MySyncClass
    {

        // 1.Lock语句和线程安全
        class SharedState
        {
            private int state = 0;
            private object syncRoot = new object();
            public int State
            {
                get
                {
                    // ref指定为引用而不是值类型：自动增加
                    return Interlocked.Increment(ref state);
                }
            }

            public int IncrementState()
            {
                lock (syncRoot)
                {
                    return ++state;
                }
            }
        }

        class Job
        {
            private SharedState sharedState;
            public Job(SharedState sharedState)
            {
                this.sharedState = sharedState;
            }

            public void DoTheJob()
            {
                    for (int i = 0; i < 50000; i++)
                    {
                        Console.WriteLine(sharedState.State);
                    }
            }
        }

        public static void LockMethod()
        {
            int numTasks = 20;
            var state = new SharedState();
            var tasks = new Task[numTasks];

            for (int i = 0; i < numTasks; i++)
            {
                tasks[i] = Task.Run(() => new Job(state).DoTheJob());
            }

            for (int i = 0; i < numTasks; i++)
            {
                tasks[i].Wait();
            }

            Console.WriteLine("summarized {0}", state.State);
        }

        public class Demo
        {
            private class SynchronizedDemo : Demo
            {
                private object syncRoot = new object();
                private Demo d;

                public SynchronizedDemo(Demo d)
                {
                    this.d = d;
                }

                public override bool IsSynchronized => true;
                public override void DoThis()
                {
                    lock (syncRoot)
                    {
                        d.DoThis();
                    }
                }

                public override void DoThat()
                {
                    lock (syncRoot)
                    {
                        d.DoThat();
                    }
                }
            }

            public virtual bool IsSynchronized
            {
                get { return false; }
            }
            public static Demo Synchronized(Demo d)
            {
                if (!d.IsSynchronized)
                {
                    return new SynchronizedDemo(d);
                }

                return d; 
            }

            public virtual void DoThis()
            {

            }

            public virtual void DoThat()
            {

            }
        }
    }
}
