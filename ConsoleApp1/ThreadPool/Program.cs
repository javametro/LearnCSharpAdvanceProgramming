using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadPoolTest
{
    public class SomeState
    {
        public int Cookie;
        public SomeState(int iCookie)
        {
            Cookie = iCookie;
        }
    }

    public class Alpha
    {
        public Hashtable HashCount;
        public ManualResetEvent eventX;
        public static int iCount = 0;
        public static int iMaxCount = 0;

        public Alpha(int MaxCount)
        {
            HashCount = new Hashtable(MaxCount);
            iMaxCount = MaxCount;
        }

        public void Beta(Object state)
        {
            Console.WriteLine("{0} {1}:", Thread.CurrentThread.GetHashCode(), ((SomeState)state).Cookie);
            Console.WriteLine("HashCount.Count =={0}, Thread.CurrentThread.GetHashCode()=={1}", HashCount.Count, Thread.CurrentThread.GetHashCode());
            lock (HashCount)
            {
                if (!HashCount.ContainsKey(Thread.CurrentThread.GetHashCode()))
                {
                    HashCount.Add(Thread.CurrentThread.GetHashCode(), 0);
                    HashCount[Thread.CurrentThread.GetHashCode()] = ((int)HashCount[Thread.CurrentThread.GetHashCode()] + 1);
                }

                int iX = 200;
                Thread.Sleep(iX);

                Interlocked.Increment(ref iCount);

                if(iCount == iMaxCount)
                {
                    Console.WriteLine();
                    Console.WriteLine("Setting eventX");
                    eventX.Set();
                }
            }
        }
    }

    public class SimplePool
    {
        public static int Main()
        {
            Console.WriteLine("Thread pool Sample:");
            bool W2K = false;
            int MaxCount = 10;
            ManualResetEvent eventX = new ManualResetEvent(false);
            Console.WriteLine("Queuing {0} items to Thread Pool", MaxCount);
            Alpha oAlpha = new Alpha(MaxCount);
            oAlpha.eventX = eventX;
            Console.WriteLine("Queue to Thread Pool 0");
            try
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(oAlpha.Beta), new SomeState(0));
                W2K = true;
                
            }catch(NotSupportedException e)
            {
                Console.WriteLine("These API's may fail when called on a non-Windows 2000 system");
                W2K = false;
            }

            if (W2K)
            {
                for(int iItem=1; iItem<MaxCount; iItem++)
                {
                    Console.WriteLine("Queue to Thread Pool {0}", iItem);
                    ThreadPool.QueueUserWorkItem(new WaitCallback(oAlpha.Beta), new SomeState(iItem));
                }

                Console.WriteLine("Waiting for Thread Pool to drain");
                eventX.WaitOne(Timeout.Infinite, true);
                Console.WriteLine("Thread pool has been drained (Event fired)");
                Console.WriteLine();
                Console.WriteLine("Load across threads");
                foreach(object o in oAlpha.HashCount.Keys)
                {
                    Console.WriteLine("{0} {1}", o, oAlpha.HashCount[o]);
                }

                Console.ReadLine();
                
            }

            return 0;
        }
    }
}
