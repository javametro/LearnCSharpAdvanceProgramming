using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    
    public class Alpha
    {
        public void Beta()
        {
            while (true)
            {
                Console.WriteLine("Beta is running  in its own thread.");
            }
        }
    }
       
 

    public class Simple
    {
        public static int Main()
        {
            Console.WriteLine("Thread Start/Stop/Join/ Sample");
            Alpha oAlpha = new Alpha();
            Thread oThread = new Thread(new ThreadStart(oAlpha.Beta));
            oThread.Start();
            while (!oThread.IsAlive)
            {
                Thread.Sleep(1);
            }

            oThread.Abort();
            oThread.Join();
            Console.WriteLine();
            Console.WriteLine("Alpha.Beta is finished.");
            try
            {
                Console.WriteLine("Try to restart the Alpha.Beta thread");
                oThread.Start();
            }catch(ThreadStateException e)
            {
                Console.WriteLine("cannot restart");
                Console.ReadLine();
            }

            return 0;
        }
    }
}
