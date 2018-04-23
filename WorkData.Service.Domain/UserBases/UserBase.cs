// ------------------------------------------------------------------------------
// Copyright  吴来伟个人 版权所有。
// 项目名：WorkData.Service.Domain
// 文件名：UserBase.cs
// 创建标识：吴来伟 2018-03-29 11:23
// 创建描述：
//
// 修改标识：吴来伟2018-03-29 11:23
// 修改描述：
//  ------------------------------------------------------------------------------

using Dapper;
using System;

namespace WorkData.Service.Domain.UserBases
{
    [Table("public", "UserBase")]
    public class UserBase
    {
        /// <summary>
        /// Id
        /// </summary>
        [Property("Id", KeyType = KeyType.Assigned)]
        public string Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [Property("UserName")]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Property("Password")]
        public string Password { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Property("CreateTime")]
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 加密盐
        /// </summary>
        [Property("Salt")]
        public string Salt { get; set; }

        /// <summary>
        /// 是否锁定
        /// </summary>
        [Property("IsLock")]
        public bool IsLock { get; set; }
    }
}