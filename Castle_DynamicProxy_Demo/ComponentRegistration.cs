using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2
{
    public class ComponentRegistration : IRegistration
    {
        public void Register(IKernelInternal kernel)
        {            
            kernel.Register(
                Component.For<TestInterceptor>()
                    .ImplementedBy<TestInterceptor>());

            kernel.Register(
                Component.For<OrderLogic>()
                         .ImplementedBy<OrderLogic>()
                         .Interceptors(InterceptorReference.ForType<TestInterceptor>()).Anywhere);
        }
    }



}
