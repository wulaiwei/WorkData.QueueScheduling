// ------------------------------------------------------------------------------
// Copyright  吴来伟个人 版权所有。
// 项目名：WorkData.Util.RabbitMQ
// 文件名：RabbitMqDataSource.cs
// 创建标识：吴来伟 2018-04-17 17:17
// 创建描述：
//
// 修改标识：吴来伟2018-04-18 9:31
// 修改描述：
//  ------------------------------------------------------------------------------

#region

using RabbitMQ.Client;
using WorkData.Dependency;

#endregion

namespace WorkData.Util.RabbitMQ.RealTime
{
    public class RabbitMqDataSource
    {
        public static WorkDataRabbitConfig RabbitMqConfig { get; } = IocManager.Instance.Resolve<WorkDataRabbitConfig>();

        //定义一个私有成员变量，用于Lock的锁定标志
        private static readonly object Lockobj = new object();

        /// <summary>
        /// IBus
        /// </summary>
        public static IConnection Bus { get; set; }

        /// <summary>
        ///     实例化
        /// </summary>
        /// <returns></returns>
        public static IConnection CreateInstance()
        {
            lock (Lockobj)
            {
                if (Bus != null)
                    return Bus;
                var factory = new ConnectionFactory()
                {
                    HostName = RabbitMqConfig.HostName,
                    UserName = RabbitMqConfig.UserName,
                    Password = RabbitMqConfig.Password
                };

                Bus = factory.CreateConnection();
                return Bus;
            }
        }
    }
}