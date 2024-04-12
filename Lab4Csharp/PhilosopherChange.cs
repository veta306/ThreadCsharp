using System;
using System.Threading;

namespace Lab4Csharp
{
    internal class PhilosopherChange
    {
        static Semaphore[] forks;
        int id;
        int idLeftFork;
        int idRightFork;

        public PhilosopherChange(int id, int idLeftFork, int idRightFork, Semaphore[] forks)
        {
            this.id = id;
            this.idLeftFork = idLeftFork;
            this.idRightFork = idRightFork;
            PhilosopherChange.forks = forks;
            new Thread(() => run()).Start();
        }

        public void run()
        {
            try
            {
                for (int i = 1; i <= 10; i++)
                {
                    Console.WriteLine("Philosopher " + id + " thinking " + i + " time");
                    if (idLeftFork < idRightFork)
                    {
                        forks[idLeftFork].WaitOne();
                        Console.WriteLine("Philosopher " + id + " took left fork");

                        forks[idRightFork].WaitOne();
                        Console.WriteLine("Philosopher " + id + " took right fork");
                    }
                    else
                    {
                        forks[idRightFork].WaitOne();
                        Console.WriteLine("Philosopher " + id + " took right fork");

                        forks[idLeftFork].WaitOne();
                        Console.WriteLine("Philosopher " + id + " took left fork");
                    }

                    Console.WriteLine("Philosopher " + id + " eating " + i + " time");

                    forks[idLeftFork].Release();
                    Console.WriteLine("Philosopher " + id + " put left fork");

                    forks[idRightFork].Release();
                    Console.WriteLine("Philosopher " + id + " put right fork");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
