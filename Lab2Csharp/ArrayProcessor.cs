using System;
using System.Threading;

namespace Lab2Csharp
{
    internal class ArrayProcessor
    {
        int length;
        int threadCount;
        int[] array;
        int completedCount = 0;
        int minIndex = 0;
        object lockObject = new object();

        public ArrayProcessor(int length, int threadCount) {
            this.length = length;
            this.threadCount = threadCount;
            array = new int[length];
            Random r = new Random();
            for (int i = 0; i < length; i++)
            {
                array[i] = r.Next();
            }
            array[length / 2] = -1000;
        }
        public string FindMin()
        {
            for (int i = 0; i < threadCount; i++)
            {
                int start = i * length / threadCount;
                int end = i == threadCount - 1 ? length - 1 : (i + 1) * length / threadCount;
                new Thread(() => Run(start, end)).Start();
            }
            return ReturnMin();
        }
        public void Run(int start, int end)
        {
            int index = MinOfPart(start, end);
            UpdateMin(index);
            UpdateCompleted();
        }
        public int MinOfPart(int start, int end)
        {
            int min = int.MaxValue;
            int index = -1;
            for (int i = start; i <= end; i++)
            {
                if (array[i] < min)
                {
                    min = array[i];
                    index = i;
                }
            }
            return index;
        }
        public string ReturnMin()
        {
            lock (lockObject)
            {
                while (completedCount < threadCount)
                {
                    Monitor.Wait(lockObject);
                }
            }
            return "Index: " + minIndex + " Value: " + array[minIndex];
        }
        public void UpdateMin(int index)
        {
            lock (lockObject)
            {
                if (array[index] < array[minIndex]) minIndex = index;
            }
        }
        public void UpdateCompleted()
        {
            lock (lockObject)
            {
                completedCount++;
                Monitor.Pulse(lockObject);
            }
        }
    }
}
