// ------------------------------------------------------------------------------
// Copyright  吴来伟个人 版权所有。
// 项目名：WorkData.Service.Application
// 文件名：ImmediateSubscription.cs
// 创建标识：吴来伟 2018-03-26 17:42
// 创建描述：
//
// 修改标识：吴来伟2018-03-26 17:42
// 修改描述：
//  ------------------------------------------------------------------------------

using RabbitMQ.Client;
using WorkData.Dependency;
using WorkData.Service.Core.DomainServices;
using WorkData.Service.Core.Entity;

namespace WorkData.Service.MessageTransfer.Dependency
{
    /// <summary>
    /// ImmediateSubscription
    /// </summary>
    public class ImmediateTask : IWorkDataTask
    {
        private readonly IIocManager _iocManager;
        private readonly IConnection _bus;

        public ImmediateTask(IIocManager iocManager, IConnection bus)
        {
            _iocManager = iocManager;
            _bus = bus;
        }

        /// <summary>
        ///     订阅
        /// </summary>
        public void CreateTask(Message message)
        {
            var proxy = new DomainServiceProxy(
                _iocManager.ResolveName<IDomainService>(message.DomainService.FullName),
                _bus);
            proxy.Execute(message);
        }
    }
}