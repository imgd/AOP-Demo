using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class Order
    {
        public string number { get; set; }

        public decimal price { get; set; }

        public string name { get; set; }
    }
    public interface IOrderLogic
    {
        Order SubmitOrder();

        Task<Order> SubmitOrderAsync();
    }
    public class OrderLogic : IOrderLogic
    {
        /// <summary>
        /// 注意需要监听的方法必须 public 修饰符 且 virtual标识
        /// </summary>
        public virtual Order SubmitOrder()
        {
            var order = new Order
            {
                name = $"货品{new Random().Next(100, 999)}",
                price = new Random().Next(1, 99999),
                number = Guid.NewGuid().ToString()
            };
            Console.WriteLine("创建了一个订单");
            return order;
        }

        /// <summary>
        /// 注意需要监听的方法必须 public 修饰符 且 virtual标识
        /// </summary>
        public async virtual Task<Order> SubmitOrderAsync()
        {
            var order = new Order
            {
                name = $"货品{new Random().Next(100, 999)}",
                price = new Random().Next(1, 99999),
                number = Guid.NewGuid().ToString()
            };

            return await Task.Run(() =>
             {
                 //这里暂停几秒模拟异步执行顺序场景
                 Thread.Sleep(3000);
                 Console.WriteLine("创建了一个订单");
                 return order;
             });

        }
    }
}
