// ------------------------------------------------------------------------------
// Copyright  吴来伟个人 版权所有。
// 项目名：WorkData.Service.Application
// 文件名：SubscriptionManage.cs
// 创建标识：吴来伟 2018-04-17 17:20
// 创建描述：
//
// 修改标识：吴来伟2018-04-18 9:44
// 修改描述：
//  ------------------------------------------------------------------------------

#region

using RabbitMQ.Client;
using System;
using WorkData.Dependency;
using WorkData.Service.Core.Entity;
using WorkData.Service.MessageTransfer.Dependency;
using WorkData.Util.RabbitMQ.RealTime;
using WorkData.Util.Redis.Interface;
using Message = WorkData.Service.Core.Entity.Message;

#endregion

namespace WorkData.Service.MessageTransfer.Extensions
{
    public class SubscriptionManage
    {
        private readonly IIocManager _iocManager;
        private readonly IRedisService _redisService;
        public IConnection Bus { get; set; }

        public SubscriptionManage(IIocManager iocManager, IRedisService redisService)
        {
            _iocManager = iocManager;
            _redisService = redisService;
            Bus = RabbitMqDataSource.CreateInstance();
        }

        /// <summary>
        ///     订阅
        /// </summary>
        public IWorkDataTask CreateSubscription(Message message)
        {
            switch (message.MessageType)
            {
                case MessageType.定时回调:
                    return new TimingWorkDataTask(Bus);

                case MessageType.直接执行:
                    return new ImmediateTask(_iocManager, Bus);

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}