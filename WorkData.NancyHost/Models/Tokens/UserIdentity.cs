// ------------------------------------------------------------------------------
// Copyright  吴来伟个人 版权所有。
// 项目名：WorkData.NancyHost
// 文件名：UserIdentity.cs
// 创建标识：吴来伟 2018-03-28 16:20
// 创建描述：
//
// 修改标识：吴来伟2018-03-28 16:20
// 修改描述：
//  ------------------------------------------------------------------------------

using Nancy.Security;
using System.Collections.Generic;

namespace WorkData.NancyHost.Models.Tokens
{
    /// <summary>
    /// UserIdentity
    /// </summary>
    public class UserIdentity : IUserIdentity
    {
        /// <summary>
        /// UserIdentity
        /// </summary>
        public UserIdentity()
        {
        }

        /// <summary>
        /// UserIdentity
        /// </summary>
        /// <param name="userName"></param>
        public UserIdentity(string userName) :
            this(userName, new List<string>())
        {
        }

        /// <summary>
        /// UserIdentity
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="claims"></param>
        public UserIdentity(string userName, IEnumerable<string> claims)
        {
            UserName = userName;
            Claims = claims;
        }

        /// <summary>
        /// UserName
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Claims
        /// </summary>
        public IEnumerable<string> Claims { get; set; }
    }
}