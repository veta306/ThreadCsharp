using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace ProducerConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            Program program = new Program();
            program.Start(3, 10, 4, 5); 
        }

        private Semaphore Access;
        private Semaphore Full;
        private Semaphore Empty;

        private ConcurrentQueue<string> storage = new ConcurrentQueue<string>();
        private int producedItemCount = 0;

        private void Start(int storageSize, int itemNumbers, int producersCount, int consumersCount)
        {
            Access = new Semaphore(1, 1);
            Full = new Semaphore(storageSize, storageSize);
            Empty = new Semaphore(0, storageSize);

            Thread[] producerThreads = new Thread[producersCount];
            Thread[] consumerThreads = new Thread[consumersCount];

            for (int i = 0; i < producersCount; i++)
            {
                int producerNumber = i;
                int items = i == producersCount - 1
                    ? itemNumbers - i * (itemNumbers / producersCount)
                    : itemNumbers / producersCount;
                producerThreads[i] = new Thread(() => Producer(items, producerNumber));
                producerThreads[i].Start();
            }

            for (int i = 0; i < consumersCount; i++)
            {
                int consumerNumber = i;
                int items = i == consumersCount - 1
                    ? itemNumbers - i * (itemNumbers / consumersCount)
                    : itemNumbers / consumersCount;
                consumerThreads[i] = new Thread(() => Consumer(items, consumerNumber));
                consumerThreads[i].Start();
            }
        }

        private void Producer(int itemsToProduce, int producerNumber)
        {
            for (int i = 0; i < itemsToProduce; i++)
            {
                Full.WaitOne();
                Access.WaitOne();
                string item = "item " + producedItemCount;
                storage.Enqueue(item);
                Console.WriteLine("-> Producer " + producerNumber + " produced " + item);
                producedItemCount++;
                Access.Release();
                Empty.Release();
            }
        }

        private void Consumer(int itemsToConsume, int consumerNumber)
        {
            for (int i = 0; i < itemsToConsume; i++)
            {
                Empty.WaitOne();
                Thread.Sleep(1000);
                Access.WaitOne();
                string item;
                storage.TryDequeue(out item);
                Console.WriteLine("<- Consumer " + consumerNumber + " consumed " + item);
                Access.Release();
                Full.Release();
            }
        }
    }
}
