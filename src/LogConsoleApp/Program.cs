using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Random logger!");

            var randomLogger = new RandomLogger();
            randomLogger.InitializeLogger();

            var token = new CancellationTokenSource();
            Task.Run(() =>
            {
                randomLogger.Start(token);
            });

            Console.ReadKey();
            token.Cancel();

        }
    }
}
