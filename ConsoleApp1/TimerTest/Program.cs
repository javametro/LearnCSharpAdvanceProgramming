using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TimerTest
{
    public class TimerExampleTest
    {
        public int counter = 0;
        public Timer timer;
    }

    class App
    {
        public static void Main()
        {
            TimerExampleTest s = new TimerExampleTest();
            TimerCallback timeDelegate = new TimerCallback(CheckStatus);
            Timer timer = new Timer(timeDelegate, s, 1000, 1000);
            s.timer = timer;

            while(s.timer != null)
            {
                Thread.Sleep(0);
            }

            Console.WriteLine("Timer example done");
            Console.ReadLine();
        }

        static void CheckStatus(Object state)
        {
            TimerExampleTest s = (TimerExampleTest)state;
            s.counter++;
            Console.WriteLine("{0} Checking Status {1}.", DateTime.Now.TimeOfDay, s.counter);

            if(s.counter == 5)
            {
                (s.timer).Change(10000, 2000);
                Console.WriteLine("changed...");
            }

            if(s.counter == 10)
            {
                Console.WriteLine("disposing  of timer...");
                s.timer.Dispose();
                s.timer = null;
            }
        }
    }
}
