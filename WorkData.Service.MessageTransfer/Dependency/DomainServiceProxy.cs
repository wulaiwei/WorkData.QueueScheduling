// ------------------------------------------------------------------------------
// Copyright  吴来伟个人 版权所有。
// 项目名：WorkData.Service.Application
// 文件名：DomainServiceProxy.cs
// 创建标识：吴来伟 2018-04-17 17:20
// 创建描述：
//
// 修改标识：吴来伟2018-04-19 15:32
// 修改描述：
//  ------------------------------------------------------------------------------

#region

using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;
using WorkData.Dependency;
using WorkData.Service.Core.Config;
using WorkData.Service.Core.DomainServices;
using WorkData.Service.Core.Entity;
using WorkData.Service.Core.Settings;
using WorkData.Util.Common.Logs;

#endregion

namespace WorkData.Service.MessageTransfer.Dependency
{
    /// <summary>
    ///     DomainServiceProxy
    /// </summary>
    public class DomainServiceProxy
    {
        private readonly IDomainService _callBack;
        private readonly IConnection _bus;

        public static CallBackQueueConfig CallBackQueueConfig { get; } =
            IocManager.Instance.Resolve<CallBackQueueConfig>();

        public DomainServiceProxy(IDomainService callBack, IConnection bus)
        {
            _callBack = callBack;
            _bus = bus;
        }

        /// <summary>
        ///     Execute
        /// </summary>
        /// <param name="message"></param>
        public void Execute(Message message)
        {
            try
            {
                _callBack.Execute(message);
                SuccessStandardCallBack(message);
            }
            catch (Exception ex)
            {
                FailStandardCallBack(message, ex);
                LoggerHelper.SystemLog.Error(ex.Message);
            }
        }

        /// <summary>
        ///     标准成功回调函数
        /// </summary>
        /// <param name="message"></param>
        public void SuccessStandardCallBack(Message message)
        {
            var handlingResult = new HandlingResult { Result = message };
            PublisQueue(handlingResult);
        }

        /// <summary>
        ///     标准失败回调函数
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public void FailStandardCallBack(Message message, Exception ex)
        {
            var handlingResult = new HandlingResult
            {
                Result = message,
                Exception = ex
            };
            PublisQueue(handlingResult);
        }

        /// <summary>
        /// 发布队列
        /// </summary>
        /// <param name="message"></param>
        private void PublisQueue(HandlingResult message)
        {
            using (var channel = _bus.CreateModel())
            {
                channel.QueueDeclare(CallBackQueueConfig.QueueName,
                    true,
                    false,
                    false,
                    null);

                var content = JsonConvert.SerializeObject(message);
                var body = Encoding.UTF8.GetBytes(content);

                var properties = channel.CreateBasicProperties();
                //消息持久性
                properties.Persistent = true;

                channel.BasicPublish("",
                    CallBackQueueConfig.RoutingKey,
                    properties,
                    body);
            }
        }
    }
}