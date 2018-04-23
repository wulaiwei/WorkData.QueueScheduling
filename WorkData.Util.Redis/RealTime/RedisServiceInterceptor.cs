// ------------------------------------------------------------------------------
// Copyright  吴来伟个人 版权所有。
// 项目名：WorkData.Util.Redis
// 文件名：RedisServiceInterceptor.cs
// 创建标识：吴来伟 2018-03-21 14:22
// 创建描述：
//
// 修改标识：吴来伟2018-03-21 14:24
// 修改描述：
//  ------------------------------------------------------------------------------

#region

using Castle.Core.Logging;
using Castle.DynamicProxy;
using Polly;
using System;

#endregion

namespace WorkData.Util.Redis.RealTime
{
    /// <summary>
    ///     RedisServiceInterceptor（AOP）
    /// </summary>
    public class RedisServiceInterceptor : IInterceptor
    {
        /// <summary>
        ///     日志
        /// </summary>
        private readonly ILogger _logger;

        public RedisServiceInterceptor()
        {
            _logger = NullLogger.Instance;
        }

        /// <summary>
        ///     Intercept
        /// </summary>
        /// <param name="invocation"></param>
        public void Intercept(IInvocation invocation)
        {
            try
            {
                //需设置为Config
                var retryPolicy =
                    Policy
                        .Handle<Exception>()
                        .Retry(3, (ex, count) =>
                        {
                            _logger.Error($"访问接口 {ex.GetType().Name}异常，错误原因为{ex.Message}");
                        });

                retryPolicy
                    .Execute(invocation.Proceed);
            }
            catch (Exception e)
            {
                _logger.Error($"执行异常，错误原因为: ({e.Message})");
            }
        }
    }
}