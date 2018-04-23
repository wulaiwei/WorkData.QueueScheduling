// ------------------------------------------------------------------------------
// Copyright  吴来伟个人 版权所有。
// 项目名：WorkData.CallBack
// 文件名：CallBackService.cs
// 创建标识：吴来伟 2018-04-20 9:03
// 创建描述：
//
// 修改标识：吴来伟2018-04-20 9:22
// 修改描述：
//  ------------------------------------------------------------------------------

#region

using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ServiceStack.Text;
using System.Text;
using System.Threading.Tasks;
using WorkData.Dependency;
using WorkData.Service.Core.Config;
using WorkData.Service.Core.Settings;
using WorkData.Util.RabbitMQ.RealTime;

#endregion

namespace WorkData.CallBack
{
    public class CallBackService
    {
        public static CallBackQueueConfig CallBackQueueConfig { get; } =
            IocManager.Instance.Resolve<CallBackQueueConfig>();

        public static ApiHostConfig ApiHostConfig { get; } =
            IocManager.Instance.Resolve<ApiHostConfig>();

        public IConnection Bus { get; set; }
        public IModel Channel { get; set; }

        public CallBackService()
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
                Channel.QueueDeclare(CallBackQueueConfig.QueueName,
                    true,
                    false,
                    false,
                    null);

                Channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

                var consumer = new EventingBasicConsumer(Channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var content = Encoding.UTF8.GetString(body);
                    var result = JsonConvert.DeserializeObject<HandlingResult>(content);
                    var message = new
                    {
                        result.Result.Id,
                        result.Successed,
                        Content = result.Exception.Message
                    };

                    //通知系统业务完成
                    ApiHostConfig.UpdateMessage.PostJsonToUrl(JsonConvert.SerializeObject(message));

                    //通知任务完成
                    Channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                };

                Channel.BasicConsume(CallBackQueueConfig.QueueName,
                    false,
                    consumer);
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