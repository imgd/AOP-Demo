using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
   public static class InterceptorInvokeHelper
    {
        /// <summary>
        /// 同步方法获取返回值
        /// </summary>
        /// <param name="invocation"></param>
        public static void SyncInterceptorInvoke(this IInvocation invocation)
        {
            try
            {
                Console.WriteLine("业务方法执行前");
                invocation.Proceed();
                Console.WriteLine("业务方法执行后");
            }
            catch (Exception e)
            {
                Console.WriteLine("业务方法执行异常");
                throw;
            }
            finally
            {
                //最终执行返回值
                var returnValue = invocation.ReturnValue;

                Console.WriteLine($"业务执行完毕，拿到返回值：{returnValue.M5_ObjectToJson()}");
            }
        }

        /// <summary>
        /// 异步方法
        /// </summary>
        /// <param name="invocation"></param>
        public static void AsyncInterceptorInvoke(this IInvocation invocation)
        {
            Console.WriteLine("业务方法执行前");
            invocation.Proceed();

            if (invocation.Method.ReturnType == typeof(Task))
            {
                invocation.ReturnValue = InternalAsyncHelper.AwaitTaskWithFinally(
                    (Task)invocation.ReturnValue,
                    exception =>
                    {
                        Console.WriteLine("业务方法执行后");
                        PrintCompletion(null);
                    }
                    );
            }
            else //Task<TResult>
            {
                invocation.ReturnValue = InternalAsyncHelper.CallAwaitTaskWithFinallyAndGetResult(
                    invocation.Method.ReturnType.GenericTypeArguments[0],
                    invocation.ReturnValue,
                    //task异步执行的返回值
                    (exception, task) =>
                    {
                        Console.WriteLine("业务方法执行后");
                        PrintCompletion(task);
                    }
                    );
            }

        }

        private static void PrintCompletion(Task task)
        {
            if (task != null && task.Status == TaskStatus.RanToCompletion)
            {
                var resurnValue = task.GetType().GetTypeInfo()
                    .GetProperty("Result")
                    ?.GetValue(task, null);

                Console.WriteLine($"业务执行完毕，拿到返回值：{resurnValue.M5_ObjectToJson()}");
            }
        }
    }
}
