using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chapter21
{
    public class MyEventClass
    {
        private class Calculator
        {
            private ManualResetEventSlim mEvent;
            public int Result { get; private set; }

            public Calculator(ManualResetEventSlim ev)
            {
                this.mEvent = ev;
            }

            public void Calculation(int x,int y)
            {
                Console.WriteLine("Task {0} starts calculation", Task.CurrentId);
                Thread.Sleep(new Random().Next(3000));

                Result = x + y;

                // signal the event-completed
                Console.WriteLine("Task {0} is ready", Task.CurrentId);
                mEvent.Set();
            }
        }

        public static void MyEventMethod()
        {
            const int taskCount = 4;
            var mEvents = new ManualResetEventSlim[taskCount];
            var waitHandles = new WaitHandle[taskCount];
            var cacls = new Calculator[taskCount];

            for (int i = 0; i < taskCount; i++)
            {
                int i1 = i;
                mEvents[i] = new ManualResetEventSlim(false);
                waitHandles[i] = mEvents[i].WaitHandle;
                cacls[i] = new Calculator(mEvents[i]);

                Task.Run(() =>
                {
                    cacls[i1].Calculation(i1 + 1, i1 + 3);
                });
            }

            for (int i = 0;i < taskCount; i++)
            {
                int index = WaitHandle.WaitAny(waitHandles);
                if (index == WaitHandle.WaitTimeout)
                {
                    Console.WriteLine("Timeout");
                }
                else
                {
                    mEvents[index].Reset();
                    Console.WriteLine("finished task for {0},result: {1}", index, cacls[index].Result);
                }
            }
        }
    }
}
