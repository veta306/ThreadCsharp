using System.Threading;

namespace Lab4Csharp
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            //StartPhilosopherChange(5, 5);
            StartPhilosopherWaiter(5, 5);
            }

        public static void StartPhilosopherWaiter(int numForks, int numPhilosophers)
        {
            Semaphore[] forks = new Semaphore[numForks];
            for (int i = 0; i < numForks; i++)
            {
                forks[i] = new Semaphore(1, 1);
            }
            Semaphore waiter = new Semaphore(numForks - 1, numForks - 1);

            PhilosopherWaiter[] philosophers = new PhilosopherWaiter[numPhilosophers];
            for (int i = 0; i < numPhilosophers; i++)
            {
                philosophers[i] = new PhilosopherWaiter(i, i, (i + 1) % numForks, forks, waiter);
            }
        }

        public static void StartPhilosopherChange(int numForks, int numPhilosophers)
        {
            Semaphore[] forks = new Semaphore[numForks];
            for (int i = 0; i < numForks; i++)
            {
                forks[i] = new Semaphore(1, 1);
            }

            PhilosopherChange[] philosophers = new PhilosopherChange[numPhilosophers];
            for (int i = 0; i < numPhilosophers; i++)
            {
                philosophers[i] = new PhilosopherChange(i, i, (i + 1) % numForks, forks);
            }
        }
    }
}
