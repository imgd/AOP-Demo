using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2
{
    public class DependencyResolver
    {
        private static IWindsorContainer _container;

        //Initialize the container
        public static void Initialize()
        {
            //
            _container = new WindsorContainer();

            //方法1直接注册添加拦截
            //_container.Register(new ComponentRegistration());

            //方法2：通过注册完成事件添加拦截，事件定义下面注册生效
            _container.Kernel.ComponentRegistered += (key, hander) =>
            {
                //Console.WriteLine(key);
                if (hander.ComponentModel.Implementation == typeof(OrderLogic))
                {
                    //根据需要添加业务的拦截
                    hander.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(TestInterceptor)));
                }
            };
            //ioc注册
            _container.Register(Component.For<TestInterceptor>().ImplementedBy<TestInterceptor>().LifestyleSingleton());
            _container.Register(Component.For<OrderLogic>().ImplementedBy<OrderLogic>().LifestyleSingleton());
        }


        //Resolve types
        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }
    }
}
