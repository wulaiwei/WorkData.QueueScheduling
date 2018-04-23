// ------------------------------------------------------------------------------
// Copyright  吴来伟个人 版权所有。
// 项目名：WorkData.Service.Core
// 文件名：Message.cs
// 创建标识：吴来伟 2018-04-17 17:20
// 创建描述：
//
// 修改标识：吴来伟2018-04-20 15:00
// 修改描述：
//  ------------------------------------------------------------------------------

#region

using System;

#endregion

namespace WorkData.Service.Core.Entity
{
    /// <summary>
    ///     消息实体
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///     Key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        ///     消息类型
        /// </summary>
        public MessageType MessageType { get; set; }

        /// <summary>
        ///     DomainService
        /// </summary>
        public Type DomainService { get; set; }

        /// <summary>
        ///     ExpireTime
        /// </summary>
        public DateTime ExpireTime { get; set; }

        /// <summary>
        ///     RequestJson
        /// </summary>
        public string RequestJson { get; set; }

        /// <summary>
        ///     Successed
        /// </summary>
        public bool Successed { get; set; }
    }
}