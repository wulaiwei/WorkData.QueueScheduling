// ------------------------------------------------------------------------------
// Copyright  吴来伟个人 版权所有。
// 项目名：WorkData.Service.MessageTransfer
// 文件名：TimingWorkDataTask.cs
// 创建标识：吴来伟 2018-04-19 15:59
// 创建描述：
//
// 修改标识：吴来伟2018-04-20 9:23
// 修改描述：
//  ------------------------------------------------------------------------------

#region

using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using WorkData.Dependency;
using WorkData.Service.Core.Config;
using WorkData.Service.Core.Entity;

#endregion

namespace WorkData.Service.MessageTransfer.Dependency
{
    /// <summary>
    ///     TimingWorkDataSubscription
    /// </summary>
    public class TimingWorkDataTask : IWorkDataTask
    {
        private readonly IConnection _bus;
        public static TimingConfig TimingConfig { get; } = IocManager.Instance.Resolve<TimingConfig>();

        public TimingWorkDataTask(IConnection bus)
        {
            _bus = bus;
        }

        /// <summary>
        ///     订阅
        /// </summary>
        public void CreateTask(Message message)
        {
            var content = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(content);
            using (var channel = _bus.CreateModel())
            {
                var differenceTime = (long)((DateTime)message.ExpireTime - DateTime.Now).TotalMilliseconds;
                var messageTimeSpan = differenceTime > 0 ? differenceTime : 1000;
                var queueTimeSpan = messageTimeSpan + 10000;
                //dic.Add("x-expires", 30000);
                //队列上消息过期时间，应小于队列过期时间
                //过期消息转向路由
                //过期消息转向路由相匹配routingkey
                var dic = new Dictionary<string, object>
                {
                    {"x-expires", queueTimeSpan},
                    {"x-message-ttl", messageTimeSpan},
                    {"x-dead-letter-exchange", TimingConfig.DeadLetterExchange},
                    {"x-dead-letter-routing-key", TimingConfig.DeadLetterRoutingKey}
                };
                var name = channel.QueueDeclare(
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: dic).QueueName;

                var properties = channel.CreateBasicProperties();
                //消息持久性
                properties.Persistent = true;

                channel.BasicPublish(exchange: "",
                    routingKey: $"{name}",
                    basicProperties: properties,
                    body: body);
            }
        }
    }
}