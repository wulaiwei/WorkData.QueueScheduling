// ------------------------------------------------------------------------------
// Copyright  吴来伟个人 版权所有。
// 项目名：WorkData.CallBack
// 文件名：Program.cs
// 创建标识：吴来伟 2018-04-20 9:01
// 创建描述：
//
// 修改标识：吴来伟2018-04-20 9:02
// 修改描述：
//  ------------------------------------------------------------------------------

using System;
using Topshelf;
using Topshelf.Autofac;
using WorkData.Util.Common.TopSelfExtensions;

namespace WorkData.CallBack
{
    internal class Program
    {
        /// <summary>
        ///     Gets a reference to the <see cref="Bootstrap" /> instance.
        /// </summary>
        public static Bootstrap BootstrapWarpper { get; } =
            Bootstrap.Instance<WorkDataCallBackModule>();

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

                hostCfg.Service<CallBackService>(s =>
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