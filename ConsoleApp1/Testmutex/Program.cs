using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Testmutex
{
    public class MutexSample
    {
        static Mutex m1;
        static Mutex m2;

        const int ITERS = 100;
        static AutoResetEvent event1 = new AutoResetEvent(false);
        static AutoResetEvent event2 = new AutoResetEvent(false);
        static AutoResetEvent event3 = new AutoResetEvent(false);
        static AutoResetEvent event4 = new AutoResetEvent(false);

        public static void Main()
        {
            Console.WriteLine("Mutex Sample...");
            m1 = new Mutex(true, "MyMutex");
            m2 = new Mutex(true);
            Console.WriteLine(" - Main Owns m1 and m2");
            AutoResetEvent[] evs = new AutoResetEvent[4];
            evs[0] = event1;
            evs[1] = event2;
            evs[2] = event3;
            evs[3] = event4;

            MutexSample ms = new MutexSample();
            Thread t1 = new Thread(new ThreadStart(ms.t1Start));
            Thread t2 = new Thread(new ThreadStart(ms.t2Start));
            Thread t3 = new Thread(new ThreadStart(ms.t3Start));
            Thread t4 = new Thread(new ThreadStart(ms.t4Start));

            t1.Start();
            t2.Start();
            t3.Start();
            t4.Start();

            Thread.Sleep(2000);
            Console.WriteLine(" - Main releases m1");
            m1.ReleaseMutex();

            Thread.Sleep(1000);
            Console.WriteLine(" - Main releases m2");
            m2.ReleaseMutex();

            WaitHandle.WaitAll(evs);
            Console.WriteLine("... Mutex Sample");
            Console.ReadLine();

        }

        public void t1Start()
        {
            Console.WriteLine("t1Start started, Mutex.WaitAll(Mutex[])");
            Mutex[] gMs = new Mutex[2];
            gMs[0] = m1;
            gMs[1] = m2;
            Mutex.WaitAll(gMs);
            Thread.Sleep(2000);
            Console.WriteLine("t1Start finished, Mutex.WaitAll(Mutex[]) satified.");
            event1.Set();
        }

        public void t2Start()
        {
            Console.WriteLine("t2Start started, m1.WaitOne()");
            m1.WaitOne();
            Console.WriteLine("t2Start finished, Mutex.WaitAll(Mutex[]) satified.");
            event2.Set();
        }

        public void t3Start()
        {
            Console.WriteLine("t3Start started, Mutex.WaitAny(Mutex[])");
            Mutex[] gMs = new Mutex[2];
            gMs[0] = m1;
            gMs[1] = m2;
            Mutex.WaitAny(gMs);
            Console.WriteLine("t3Start finished, Mutex.WaitAny(Mutex[]) satified.");
            event3.Set();
        }

        public void t4Start()
        {
            Console.WriteLine("t4Start started, m2.WaitOne()");
            m2.WaitOne();
            Console.WriteLine("t4Start finished, m2.WaitOne()");
            event4.Set();
        }
    }
}
