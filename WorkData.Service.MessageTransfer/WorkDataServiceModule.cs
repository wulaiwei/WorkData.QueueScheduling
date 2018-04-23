// ------------------------------------------------------------------------------
// Copyright  吴来伟个人 版权所有。
// 项目名：WorkData.Service.MessageTransfer
// 文件名：WorkDataServiceModule.cs
// 创建标识：吴来伟 2018-04-19 15:59
// 创建描述：
//
// 修改标识：吴来伟2018-04-20 14:38
// 修改描述：
//  ------------------------------------------------------------------------------

#region

using Autofac;
using Autofac.Configuration;
using Microsoft.Extensions.Configuration;
using System;
using WorkData.Extensions.Modules;
using WorkData.Extensions.Types;
using WorkData.Service.Application;
using WorkData.Service.MessageTransfer.Dependency;
using WorkData.Service.MessageTransfer.Extensions;
using WorkData.Util.RabbitMQ;
using WorkData.Util.Redis;

#endregion

namespace WorkData.Service.MessageTransfer
{
    /// <summary>
    ///     WorkDataServiceModule
    /// </summary>
    /// s
    [DependsOn(
        typeof(WorkDataRedisModule),
        typeof(WorkDataRabbitMqModule),
        typeof(WorkDataServiceApplicationModule))]
    public class WorkDataServiceModule : WorkDataBaseModule
    {
        private readonly ILoadType _loadType;

        public WorkDataServiceModule()
        {
            _loadType = NullLoadType.Instance;
        }

        protected override void Load(ContainerBuilder builder)
        {
            //自动用Services里的类来注册相应实例，无须一个个注册
            builder.RegisterType<QueueSchedulingService>();
            builder.RegisterType(typeof(DomainServiceProxy));
            builder.RegisterType<SubscriptionManage>();
            RegisterMatchConfig();
        }

        /// <summary>
        ///     RegisterMatchConfig
        /// </summary>
        private void RegisterMatchConfig()
        {
            if (IocManager == null)
                return;

            var builder = new ContainerBuilder();
            var config = new ConfigurationBuilder();
            config.SetBasePath(AppDomain.CurrentDomain.BaseDirectory);
            config.AddJsonFile("Config/callBackQueueConfig.json");
            var module = new ConfigurationModule(config.Build());

            builder.RegisterModule(module);
            IocManager.UpdateContainer(builder);
        }
    }
}