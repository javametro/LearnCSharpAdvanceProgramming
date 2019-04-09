using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsumerProducer
{
    public class Cell
    {
        int cellContents;
        bool readerFlag = false;
        public int ReadFromCell()
        {
            lock (this)
            {
                if (!readerFlag)
                {
                    try
                    {
                        Monitor.Wait(this);
                    }catch(SynchronizationLockException e)
                    {
                        Console.WriteLine(e);
                    }catch(ThreadInterruptedException e)
                    {
                        Console.WriteLine(e);
                    }
                }

                Console.WriteLine("Consumer: {0}", cellContents);
                readerFlag = false;
                Monitor.Pulse(this);
            }

            return cellContents;
        }

        public void WriteToCell(int n)
        {
            lock (this)
            {
                if (readerFlag)
                {
                    try
                    {
                        Monitor.Wait(this);
                    }catch(SynchronizationLockException e)
                    {
                        Console.WriteLine(e);
                    }catch(ThreadInterruptedException e)
                    {
                        Console.WriteLine(e);
                    }
                }

                cellContents = n;
                Console.WriteLine("Produce: {0}", cellContents);
                readerFlag = true;
                Thread.Sleep(1000);
                Monitor.Pulse(this);
            }
        }
    }

   
}
