// ------------------------------------------------------------------------------
// Copyright  吴来伟个人 版权所有。
// 项目名：WorkData.NancyHost
// 文件名：InsertMessageRequest.cs
// 创建标识：吴来伟 2018-04-20 11:04
// 创建描述：
//
// 修改标识：吴来伟2018-04-20 15:23
// 修改描述：
//  ------------------------------------------------------------------------------

#region

using System;

#endregion

namespace WorkData.NancyHost.Models.Messages
{
    public class InsertMessageRequest
    {
        /// <summary>
        ///     key
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///     key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        ///     消息类型
        /// </summary>
        public int MessageType { get; set; }

        /// <summary>
        ///     服务
        /// </summary>
        public string DomainService { get; set; }

        /// <summary>
        ///     到期时间
        /// </summary>
        public DateTime ExpireTime { get; set; }

        /// <summary>
        ///     服务请求体
        /// </summary>
        public string RequestJson { get; set; }

        /// <summary>
        ///     获取或设置处理结果，默认为false。
        /// </summary>
        public bool Successed { get; set; }
    }
}