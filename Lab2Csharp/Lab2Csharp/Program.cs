using System;

namespace Lab2Csharp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int length = 10000000;
            int threadCount = 4;
            ArrayProcessor arrayProcessor = new ArrayProcessor(length, threadCount);
            Console.WriteLine(arrayProcessor.FindMin());
        }
    }
}