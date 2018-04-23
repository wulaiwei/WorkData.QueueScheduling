// ------------------------------------------------------------------------------
// Copyright  吴来伟个人 版权所有。
// 项目名：WorkData.Service.Domain
// 文件名：Message.cs
// 创建标识：吴来伟 2018-04-20 10:46
// 创建描述：
//
// 修改标识：吴来伟2018-04-20 11:02
// 修改描述：
//  ------------------------------------------------------------------------------

#region

using Dapper;
using System;

#endregion

namespace WorkData.Service.Domain.Messges
{
    [Table("public", "message")]
    public class Message
    {
        /// <summary>
        ///     ID
        /// </summary>
        [Property("id", KeyType = KeyType.Assigned)]
        public string Id { get; set; }

        /// <summary>
        ///     Key
        /// </summary>
        [Property("key")]
        public string Key { get; set; }

        /// <summary>
        ///     MessageType
        /// </summary>
        [Property("messageType")]
        public int MessageType { get; set; }

        /// <summary>
        ///     DomainService
        /// </summary>
        [Property("domainService")]
        public string DomainService { get; set; }

        /// <summary>
        ///     ExpireTime
        /// </summary>
        [Property("expireTime")]
        public DateTime? ExpireTime { get; set; }

        /// <summary>
        ///     RequestJson
        /// </summary>
        [Property("requestJson")]
        public string RequestJson { get; set; }

        /// <summary>
        ///     Successed
        /// </summary>
        [Property("successed")]
        public bool Successed { get; set; }

        /// <summary>
        ///     Extend
        /// </summary>
        [Property("extend")]
        public string Extend { get; set; }

        /// <summary>
        /// 是否执行
        /// </summary>
        [Property("isexecute")]
        public bool IsExecute { get; set; }

        /// <summary>
        /// 消费成功消息返回内容
        /// </summary>
        [Property("successmessage")]
        public string SuccessMeseage { get; set; }

        /// <summary>
        /// 消费失败消息返回内容
        /// </summary>
        [Property("errormessage")]
        public string ErrorMessage { get; set; }
    }
}