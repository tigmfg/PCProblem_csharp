using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace 生产者_消费者问题
{
    class PCproblem
    {
        Queue<int> goods = new Queue<int>();
        int maxBuffer = 3;
        int goodNum = 0;

        Semaphore good = new Semaphore(1, 1);
        Semaphore producer = new Semaphore(1, 1);
        Semaphore consumer = new Semaphore(0, 1);

        bool consumerLocked = true;
        bool produserLocked = false;

        public void produce()
        {
            while (true)
            {
                while (produserLocked == false)
                {
                    Console.WriteLine(Thread.CurrentThread.Name + "准备生产物品");
                    Thread.Sleep(1000);
                    good.WaitOne();
                    if (goods.Count == maxBuffer)
                    {
                        Console.WriteLine("当前物品已满，生产者停止生产");
                        Thread.Sleep(1000);
                        good.Release();
                        producer.WaitOne();
                        produserLocked = true;
                        break;
                    }
                    goodNum++;
                    goods.Enqueue(goodNum);
                    Console.WriteLine(Thread.CurrentThread.Name + "生产了物品" + goodNum.ToString());
                    Console.WriteLine("当前物品数为" + goods.Count.ToString());
                    Thread.Sleep(1000);
                    if (consumerLocked)
                    {
                        consumerLocked = false;
                        consumer.Release();
                    }
                    good.Release();
                }
            }
        }

        public void consume()
        {
            while (true)
            {
                while (consumerLocked == false)
                {
                    good.WaitOne();
                    Console.WriteLine(Thread.CurrentThread.Name + "准备购买物品");
                    Thread.Sleep(1000);
                    if (goods.Count == 0)
                    {
                        Console.WriteLine("当前物品为空，消费者无法购买");
                        Thread.Sleep(1000);
                        good.Release();
                        consumer.WaitOne();
                        consumerLocked = true;
                        break;
                    }
                    int thisGood = goods.Dequeue();
                    Console.WriteLine(Thread.CurrentThread.Name + "购买了物品" + thisGood.ToString());
                    Console.WriteLine("当前物品数为" + goods.Count.ToString());
                    Thread.Sleep(1000);
                    if (produserLocked)
                    {
                        produserLocked = false;
                        producer.Release();
                    }
                    good.Release();
                }
            }
        }
    }
}
