// ------------------------------------------------------------------------------
// Copyright  吴来伟个人 版权所有。
// 项目名：WorkData.TimingConsumer
// 文件名：TimingConsumerService.cs
// 创建标识：吴来伟 2018-04-20 16:59
// 创建描述：
//
// 修改标识：吴来伟2018-04-20 17:30
// 修改描述：
//  ------------------------------------------------------------------------------

#region

using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading.Tasks;
using WorkData.Dependency;
using WorkData.Service.Core.Config;
using WorkData.Service.Core.DomainServices;
using WorkData.Service.Core.Entity;
using WorkData.Service.MessageTransfer.Dependency;
using WorkData.Util.RabbitMQ.RealTime;

#endregion

namespace WorkData.TimingConsumer
{
    public class TimingConsumerService
    {
        /// <summary>
        ///     回调队列配置
        /// </summary>
        public static CallBackQueueConfig CallBackQueueConfig { get; } =
            IocManager.Instance.Resolve<CallBackQueueConfig>();

        /// <summary>
        ///     延迟路由
        /// </summary>
        public static TimingConfig TimingConfig { get; } =
            IocManager.Instance.Resolve<TimingConfig>();

        public IConnection Bus { get; set; }
        public IModel Channel { get; set; }

        public TimingConsumerService()
        {
            Bus = RabbitMqDataSource.CreateInstance();
            Channel = Bus.CreateModel();
        }

        /// <summary>
        ///     Start
        /// </summary>
        public bool Start()
        {
            Task.Run(() =>
            {
                Channel.ExchangeDeclare(
                    exchange: TimingConfig.DeadLetterExchange,
                    type: "direct");

                Channel.QueueDeclare(queue: TimingConfig.ReceiveQueueName,
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                Channel.QueueBind(
                    queue: TimingConfig.ReceiveQueueName,
                    exchange: TimingConfig.DeadLetterExchange,
                    routingKey: TimingConfig.DeadLetterRoutingKey);

                //回调，当consumer收到消息后会执行该函数
                var consumer = new EventingBasicConsumer(Channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var content = Encoding.UTF8.GetString(body);
                    var message = JsonConvert.DeserializeObject<Message>(content);
                    var proxy = new DomainServiceProxy(
                        IocManager.Instance.ResolveName<IDomainService>(message.DomainService.FullName),
                        Bus);
                    proxy.Execute(message);
                };

                Channel.BasicConsume(queue: TimingConfig.ReceiveQueueName,
                    autoAck: true,
                    consumer: consumer);
            });

            return true;
        }

        /// <summary>
        ///     Stop
        /// </summary>
        public bool Stop()
        {
            return true;
        }
    }
}