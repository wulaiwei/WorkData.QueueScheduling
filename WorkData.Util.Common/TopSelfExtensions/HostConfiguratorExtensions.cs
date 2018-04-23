// ------------------------------------------------------------------------------
// Copyright  吴来伟个人 版权所有。
// 项目名：WorkData.Service
// 文件名：HostConfiguratorExtensions.cs
// 创建标识：吴来伟 2018-03-23 13:37
// 创建描述：
//
// 修改标识：吴来伟2018-03-23 13:37
// 修改描述：
//  ------------------------------------------------------------------------------

using Autofac;
using Topshelf.Autofac;
using Topshelf.HostConfigurators;

namespace WorkData.Util.Common.TopSelfExtensions
{
    public static class HostConfiguratorExtensions
    {
        #region Public Methods and Operators

        public static HostConfigurator UseCustomerConfigure(this HostConfigurator configurator)
        {
            var container = AutofacHostBuilderConfigurator.LifetimeScope;

            if (!container.IsRegistered<HostConfig>())
                return configurator;

            var customerConfigure = container.Resolve<HostConfig>();
            customerConfigure.Configure(ref configurator);

            return configurator;
        }

        #endregion Public Methods and Operators
    }
}