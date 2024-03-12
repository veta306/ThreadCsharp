using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1Csharp
{
    internal class MainThread
    {
        private readonly int id;
        private readonly int step;
        private readonly ControlThread controlThread;

        public MainThread(int id, int step, ControlThread controlThread)
        {
            this.id = id;
            this.step = step;
            this.controlThread = controlThread;
        }

        public void Start()
        {
            Thread thread = new Thread(new ThreadStart(Run));
            thread.Start();
        }

        private void Run()
        {
            long sum = 0;
            long elements = 0;

            while (!controlThread.canBreak)
            {
                sum += elements * step;
                elements++;
            }
            Console.WriteLine("Thread " + id + ": Sum = " + sum + ", Elements = " + (elements - 1));
        }
    }
}
