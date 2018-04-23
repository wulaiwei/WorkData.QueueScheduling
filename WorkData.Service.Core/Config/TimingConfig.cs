// ------------------------------------------------------------------------------
// Copyright  吴来伟个人 版权所有。
// 项目名：WorkData.Service.Application
// 文件名：TimingConfig.cs
// 创建标识：吴来伟 2018-04-19 10:31
// 创建描述：
//
// 修改标识：吴来伟2018-04-19 10:31
// 修改描述：
//  ------------------------------------------------------------------------------

using System;

namespace WorkData.Service.Core.Config
{
    [Serializable]
    public class TimingConfig
    {
        /// <summary>
        /// DeadLetterExchange
        /// </summary>
        public string DeadLetterExchange { get; set; }

        /// <summary>
        /// DeadLetterRoutingKey
        /// </summary>
        public string DeadLetterRoutingKey { get; set; }

        /// <summary>
        /// ReceiveQueueName
        /// </summary>
        public string ReceiveQueueName { get; set; }
    }
}