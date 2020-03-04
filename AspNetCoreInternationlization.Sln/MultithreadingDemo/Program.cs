using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace MultithreadingDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pt-BR");
            //
            ThreadInfo();
            //
            TaskFactory tf = new TaskFactory();
            tf.StartNew(BackgroundTask);
            Task.Delay(3000).Wait();
            ///////
            CultureInfo.CurrentCulture = new CultureInfo("ru-RU");
            ///////
            ThreadInfo();
            Task.Delay(3000).Wait();

        }

        static void ThreadInfo()
        {
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}:{CultureInfo.CurrentCulture.DisplayName}");
        }

        static void BackgroundTask()
        {
            while (true)
            {
                ThreadInfo();
                Task.Delay(1000).Wait();
            }
        }
    }
}
