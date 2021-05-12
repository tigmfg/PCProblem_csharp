using System;
using System.Threading;

namespace 生产者_消费者问题
{
    class Program
    {
        static void Main(string[] args)
        {
            //PCproblem good = new PCproblem();
            PCProblem2 good = new PCProblem2();

            Thread producer1 = new Thread(new ThreadStart(good.produce));
            producer1.Name = "生产者1";
            Thread producer2 = new Thread(new ThreadStart(good.produce));
            producer2.Name = "生产者2";
            Thread consumer1 = new Thread(new ThreadStart(good.consume));
            consumer1.Name = "消费者1";
            Thread consumer2 = new Thread(new ThreadStart(good.consume));
            consumer2.Name = "消费者2";
            producer1.Start();
            consumer1.Start();
            producer2.Start();
            consumer2.Start();
            producer1.Join();
            consumer1.Join();   
            producer2.Join();
            consumer2.Join();
        }
    }
}
