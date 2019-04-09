using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsumerProducer
{
    internal class Account
    {
        int balance;
        Random r = new Random();

        internal Account(int initial)
        {
            balance = initial;
        }

        internal int Withdraw(int amount)
        {
            if(balance < 0)
            {
                throw new Exception("Negative Balance");
            }

            lock (this)
            {
                Console.WriteLine("Current Thread: " + Thread.CurrentThread.Name);
                if(balance >= amount)
                {
                    Thread.Sleep(5);
                    balance = balance - amount;
                    return amount;
                }
                else
                {
                    return 0;
                }
            }
        }

        internal void DoTransanction()
        {
            for(int i=0; i<100; i++)
            {
                Withdraw(r.Next(-50, 100));
            }
        }
    }

    internal class Program
    {
        static internal Thread[] threads = new Thread[10];
        static void Test(string[] args)
        {
            Account acc = new Account(0);
            for(int i=0; i<10; i++)
            {
                Thread t = new Thread(new ThreadStart(acc.DoTransanction));
                threads[i] = t;
            }

            for(int i=0; i<10; i++)
            {
                threads[i].Name = i.ToString();
            }

            for(int i=0; i<10; i++)
            {
                threads[i].Start();
            }

            Console.ReadLine();

        }

        static void Main()
        {
            int result = 0;
            Cell cell = new Cell();
            CellProd prod = new CellProd(cell, 20);
            CellCons cons = new CellCons(cell, 20);

            Thread producer = new Thread(new ThreadStart(prod.ThreadRun));
            Thread consumer = new Thread(new ThreadStart(cons.ThreadRun));

            try
            {
                producer.Start();
                consumer.Start();
                producer.Join();
                consumer.Join();
                Console.ReadLine();
            }catch(ThreadStateException e)
            {
                Console.WriteLine(e);
                result = 1;
            }catch(ThreadInterruptedException e)
            {
                Console.WriteLine(e);
                result = 1;
            }

            Environment.ExitCode = result;
        }
    }
}
