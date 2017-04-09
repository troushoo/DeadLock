using System;
using System.Threading.Tasks;

namespace DeadLockSample
{
    class Program
    {
        static void Main(string[] args)
        {
            object obj1 = new object();
            object obj2 = new object();
            
            Task task1 = Task.Run(
                () =>
                {
                    lock (obj1)
                    {
                        System.Threading.Thread.Sleep(2000);
                        Console.WriteLine("Trying to get obj2 from task1");

                        lock (obj2)
                        {
                            Console.WriteLine("task1 get obj2");
                        }
                    }

                }
                );

            Task task2 = Task.Run(
                () =>
                {
                    lock (obj2)
                    {
                        System.Threading.Thread.Sleep(1000);
                        Console.WriteLine("Trying to get obj1 from task2");

                        lock (obj1)
                        {
                            Console.WriteLine("task2 get obj1");
                        }
                    }

                }
                );


            Console.ReadLine();
        }
    }
}
