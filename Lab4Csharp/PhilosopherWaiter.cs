using System;
using System.Threading;

namespace Lab4Csharp
{
    internal class PhilosopherWaiter
    {
        static Semaphore[] forks;
        static Semaphore waiter;
        int id;
        int idLeftFork;
        int idRightFork;

        public PhilosopherWaiter(int id, int idLeftFork, int idRightFork, Semaphore[] forks, Semaphore waiter)
        {
            this.id = id;
            this.idLeftFork = idLeftFork;
            this.idRightFork = idRightFork;
            PhilosopherWaiter.forks = forks;
            PhilosopherWaiter.waiter = waiter;
            new Thread(() => run()).Start();
        }

        public void run()
        {
            try
            {
                for (int i = 1; i <= 10; i++)
                {
                    Console.WriteLine("Philosopher " + id + " thinking " + i + " time");

                    waiter.WaitOne();
                    Console.WriteLine("Philosopher " + id + " got permission from waiter");

                    forks[idLeftFork].WaitOne();
                    Console.WriteLine("Philosopher " + id + " took left fork");

                    forks[idRightFork].WaitOne();
                    Console.WriteLine("Philosopher " + id + " took right fork");

                    Console.WriteLine("Philosopher " + id + " eating " + i + " time");

                    forks[idLeftFork].Release();
                    Console.WriteLine("Philosopher " + id + " put left fork");

                    forks[idRightFork].Release();
                    Console.WriteLine("Philosopher " + id + " put right fork");

                    waiter.Release();
                    Console.WriteLine("Philosopher " + id + " released permission to waiter");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
