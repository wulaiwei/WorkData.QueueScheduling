// ------------------------------------------------------------------------------
// Copyright  吴来伟个人 版权所有。
// 项目名：WorkData.Service.MessageTransfer
// 文件名：QueueSchedulingService.cs
// 创建标识：吴来伟 2018-04-19 15:59
// 创建描述：
//
// 修改标识：吴来伟2018-04-19 17:32
// 修改描述：
//  ------------------------------------------------------------------------------

#region

using Newtonsoft.Json;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WorkData.Dependency;
using WorkData.Service.Core.Config;
using WorkData.Service.Core.Entity;
using WorkData.Service.MessageTransfer.Extensions;
using WorkData.Util.Common.Logs;
using WorkData.Util.Common.ResponseExtensions;
using WorkData.Util.Redis.Entity;
using WorkData.Util.Redis.Impl;

#endregion

namespace WorkData.Service.MessageTransfer
{
    public class QueueSchedulingService
    {
        /// <summary>
        ///     CancellationTokenSource
        /// </summary>
        private readonly CancellationTokenSource _cancelTokenSource =
            new CancellationTokenSource();

        public static ApiHostConfig ApiHostConfig { get; } =
            IocManager.Instance.Resolve<ApiHostConfig>();

        /// <summary>
        ///     RedisService
        /// </summary>
        private readonly BaseRedisServiceManager _baseRedisServiceManager;

        private readonly SubscriptionManage _subscriptionManage;

        public QueueSchedulingService(BaseRedisServiceManager baseRedisServiceManager,
            SubscriptionManage subscriptionManage)
        {
            _baseRedisServiceManager = baseRedisServiceManager;
            _subscriptionManage = subscriptionManage;
        }

        /// <summary>
        ///     Start
        /// </summary>
        public bool Start()
        {
            var json = ApiHostConfig.QueryMessage.GetStringFromUrl();
            var result = JsonConvert.DeserializeObject<ServerResponse<List<Message>>>(json);
            //清除队列
            _baseRedisServiceManager.RemoveList("QueueScheduling");

            foreach (var item in result.Result)
            {
                var queue = new RedisQueue<Message>
                {
                    Key = "QueueScheduling",
                    Entity = item
                };
                _baseRedisServiceManager.AddQueue(queue);
            }
            //新进程
            Task.Factory.StartNew(DelayStart, _cancelTokenSource.Token);

            return true;
        }

        /// <summary>
        ///     Stop
        /// </summary>
        public bool Stop()
        {
            //关闭新开线程
            _cancelTokenSource.Cancel();
            _cancelTokenSource.Dispose();

            return true;
        }

        #region 开启新线程，避免服务启动失败

        /// <summary>
        ///     延迟启动
        /// </summary>
        private void DelayStart()
        {
            //新进程
            while (!_cancelTokenSource.IsCancellationRequested) // Worker thread loop
            {
                try
                {
                    var item = _baseRedisServiceManager
                        .BlockingPopQueue<Message>("QueueScheduling", null);

                    Task.Run(() =>
                    {
                        var subscription = _subscriptionManage.CreateSubscription(item);

                        subscription.CreateTask(item);
                    });
                }
                catch (Exception ex)
                {
                    //异常处理
                    LoggerHelper.SystemLog.Error(ex.Message);
                }
            }
        }

        #endregion
    }
}