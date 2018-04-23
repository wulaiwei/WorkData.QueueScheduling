// ------------------------------------------------------------------------------
// Copyright  吴来伟个人 版权所有。
// 项目名：WorkData.Util.RabbitMQ
// 文件名：WorkDataRabbitMQModule.cs
// 创建标识：吴来伟 2018-04-15 19:33
// 创建描述：
//
// 修改标识：吴来伟2018-04-15 19:33
// 修改描述：
//  ------------------------------------------------------------------------------

using Autofac;
using Autofac.Configuration;
using Microsoft.Extensions.Configuration;
using System;
using WorkData.Extensions.Modules;
using WorkData.Util.RabbitMQ.RealTime;

namespace WorkData.Util.RabbitMQ
{
    // ReSharper disable once InconsistentNaming
    public class WorkDataRabbitMqModule : WorkDataBaseModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            //配置文件注入则不需要再次注入
            builder.RegisterType<RabbitMqDataSource>();

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
            config.AddJsonFile("Config/rabbitMqConfig.json");
            var module = new ConfigurationModule(config.Build());
            builder.RegisterModule(module);
            IocManager.UpdateContainer(builder);
        }
    }
}