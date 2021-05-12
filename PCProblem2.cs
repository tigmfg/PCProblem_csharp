using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace 生产者_消费者问题
{
    class PCProblem2
    {
        Queue<int> goods = new Queue<int>();
        int goodNum = 0;

        Semaphore buffer = new Semaphore(1,1);   //缓冲区
        Semaphore empty = new Semaphore(3, 3);   //生产者能够生产的物品数量
        Semaphore fill = new Semaphore(0, 3);   //消费者可以购买的物品数量

        public void produce()
        {
            while (true)
            { 
                empty.WaitOne();   //等待能够生产物品的信号量
                buffer.WaitOne();
                goodNum++;
                goods.Enqueue(goodNum);
                Console.WriteLine(Thread.CurrentThread.Name + "生产者生产了物品" + goodNum.ToString());
                Thread.Sleep(1000);
                Console.WriteLine("当前物品数为" + goodNum.ToString());
                Thread.Sleep(1000);
                fill.Release();   //释放有物品被放入信号量
                buffer.Release();
            }
        }

        public void consume()
        {
            while (true)
            { 
                fill.WaitOne();   //等待物品被放入
                buffer.WaitOne();
                int thisGood = goods.Dequeue();
                Console.WriteLine(Thread.CurrentThread.Name + "购买了物品" + thisGood.ToString());
                Thread.Sleep(1000);
                Console.WriteLine("当前物品数为" + goods.Count.ToString());
                Thread.Sleep(1000);
                empty.Release();   //可以生产物品信号量
                buffer.Release();
            }
        }
    }
}
