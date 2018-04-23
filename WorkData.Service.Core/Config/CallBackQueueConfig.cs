// ------------------------------------------------------------------------------
// Copyright  吴来伟个人 版权所有。
// 项目名：WorkData.Service.MessageTransfer
// 文件名：CallBackQueueConfig.cs
// 创建标识：吴来伟 2018-04-19 17:09
// 创建描述：
//
// 修改标识：吴来伟2018-04-19 17:13
// 修改描述：
//  ------------------------------------------------------------------------------

using System;

namespace WorkData.Service.Core.Config
{
    [Serializable]
    public class CallBackQueueConfig
    {
        /// <summary>
        ///     队列名称
        /// </summary>
        public string QueueName { get; set; }

        /// <summary>
        ///     RoutingKey
        /// </summary>
        public string RoutingKey { get; set; }
    }
}