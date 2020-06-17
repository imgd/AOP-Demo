using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    /// <summary>
    /// 测试监听拦截器
    /// </summary>
    public class TestInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {

            if (invocation.Method.IsAsync())
            {
                invocation.AsyncInterceptorInvoke();
            }
            else
            {
                invocation.SyncInterceptorInvoke();
            }

        }

    }
}
