using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1Csharp
{
    internal class ControlThread
    {
        public bool canBreak = false;
        private readonly int time;

        public ControlThread(int time)
        {
            this.time = time;
        }
        public void Start()
        {
            new Thread(new ThreadStart(Run)).Start();
        }
        public void Run()
        {
            try
            {
                Thread.Sleep(time);
            }
            catch (ThreadInterruptedException e)
            {
                Console.WriteLine(e.StackTrace);
            }
            canBreak = true;
        }
    }
}
