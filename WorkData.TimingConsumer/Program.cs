// ------------------------------------------------------------------------------
// Copyright  吴来伟个人 版权所有。
// 项目名：WorkData.TimingConsumer
// 文件名：Program.cs
// 创建标识：吴来伟 2018-04-20 16:57
// 创建描述：
//
// 修改标识：吴来伟2018-04-20 17:27
// 修改描述：
//  ------------------------------------------------------------------------------

#region

using System;
using Topshelf;
using Topshelf.Autofac;
using WorkData.Util.Common.TopSelfExtensions;

#endregion

namespace WorkData.TimingConsumer
{
    internal class Program
    {
        /// <summary>
        ///     Gets a reference to the <see cref="Bootstrap" /> instance.
        /// </summary>
        public static Bootstrap BootstrapWarpper { get; } =
            Bootstrap.Instance<TimingConsumerModule>();

        /// <summary>
        ///     入口程序
        /// </summary>
        /// <param name="args"></param>
        [STAThread]
        private static void Main(string[] args)
        {
            //Autofac 注入
            BootstrapWarpper.Initiate();

            HostFactory.Run(hostCfg =>
            {
                // Pass it to Topshelf
                hostCfg.UseAutofacContainer(
                    BootstrapWarpper.IocManager.IocContainer);
                //加载配置
                hostCfg.UseCustomerConfigure();

                hostCfg.Service<TimingConsumerService>(s =>
                {
                    // Let Topshelf use it
                    s.ConstructUsingAutofacContainer();
                    s.WhenStarted((service, control) => service.Start());
                    s.WhenStopped((service, control) => service.Stop());
                });
            });
        }
    }
}