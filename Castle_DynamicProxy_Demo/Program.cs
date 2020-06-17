using System;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    /// <summary>
    /// Castle DynamicProxy 动态代理（AOP） demo
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            //初始化
            DependencyResolver.Initialize();

            //获取订单业务依赖注入实例
            var orderlogic = DependencyResolver.Resolve<OrderLogic>();

            var order1 = orderlogic.SubmitOrder();

            var order2 = orderlogic.SubmitOrderAsync();

            Console.ReadLine();
        }
    }
}
