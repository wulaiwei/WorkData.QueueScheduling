// ------------------------------------------------------------------------------
// Copyright  吴来伟个人 版权所有。
// 项目名：WorkData.Service
// 文件名：HostConfig.cs
// 创建标识：吴来伟 2018-03-23 13:34
// 创建描述：
//
// 修改标识：吴来伟2018-03-23 13:34
// 修改描述：
//  ------------------------------------------------------------------------------

using System;
using System.ServiceProcess;
using Topshelf;
using Topshelf.HostConfigurators;
using Topshelf.Runtime;

namespace WorkData.Util.Common.TopSelfExtensions
{
    /// <summary>
    /// HostConfig
    /// </summary>
    [Serializable]
    public class HostConfig
    {
        #region 属性   （IOC属性注入）

        /// <summary>
        /// 服务用户名
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// 服务描述
        /// </summary>
        public string ServiceDescription { get; set; }

        /// <summary>
        /// 服务默认描述信息
        /// </summary>
        public string ServiceDisplayName { get; set; }

        /// <summary>
        /// InstanceName
        /// </summary>
        public string InstanceName { get; set; }

        /// <summary>
        /// 服务运行类别
        /// </summary>
        public ServiceAccount RunAs { get; set; }

        /// <summary>
        /// 服务启动模式
        /// </summary>
        public HostStartMode StartMode { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string UserName { get; set; }

        #endregion 属性   （IOC属性注入）

        /// <summary>
        /// CustomerHostConfiguration
        /// </summary>
        public HostConfig()
        {
            ServiceName = "WorkData.Service";
            InstanceName = "HostManagementService";

            ServiceDisplayName = "Windows服务宿主管理服务";
            ServiceDescription = "由Topshelf提供宿主环境的Windows服务，可在其上运行各种用户自定义托管服务";

            RunAs = ServiceAccount.LocalService;
            StartMode = HostStartMode.Automatic;
        }

        /// <summary>
        /// Configure
        /// </summary>
        /// <param name="hostCfg"></param>
        /// <returns></returns>
        public HostConfigurator Configure(ref HostConfigurator hostCfg)
        {
            #region 设置运行模式

            switch (RunAs)
            {
                case ServiceAccount.LocalService:
                    hostCfg.RunAsLocalService();
                    break;

                case ServiceAccount.LocalSystem:
                    hostCfg.RunAsLocalSystem();
                    break;

                case ServiceAccount.NetworkService:
                    hostCfg.RunAsNetworkService();
                    break;

                case ServiceAccount.User:
                    hostCfg.RunAs(UserName, Password);
                    break;

                default:
                    hostCfg.RunAsPrompt();
                    break;
            }

            #endregion 设置运行模式

            #region 设置启动模式

            switch (StartMode)
            {
                case HostStartMode.AutomaticDelayed:
                    hostCfg.StartAutomaticallyDelayed();
                    break;

                case HostStartMode.Disabled:
                    break;

                case HostStartMode.Manual:
                    hostCfg.StartManually();
                    break;

                default:
                    hostCfg.StartAutomatically();
                    break;
            }

            #endregion 设置启动模式

            #region 设置服务基础信息

            hostCfg.SetServiceName(ServiceName);
            hostCfg.SetInstanceName(InstanceName);
            hostCfg.SetDescription(ServiceDescription);
            hostCfg.SetDisplayName(ServiceDisplayName);

            #endregion 设置服务基础信息

            return hostCfg;
        }
    }
}