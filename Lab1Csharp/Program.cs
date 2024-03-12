using System.Threading;

namespace Lab1Csharp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ControlThread controlThread = new ControlThread(10000);
            for (int i = 1; i <= 4; i++)
            {
                new MainThread(i, 1, controlThread).Start();
            }
            controlThread.Start();
        }
    }
}