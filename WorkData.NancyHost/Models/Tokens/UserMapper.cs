// ------------------------------------------------------------------------------
// Copyright  吴来伟个人 版权所有。
// 项目名：WorkData.NancyHost
// 文件名：UserMapper.cs
// 创建标识：吴来伟 2018-03-28 17:18
// 创建描述：
//
// 修改标识：吴来伟2018-03-28 17:18
// 修改描述：
//  ------------------------------------------------------------------------------

using Nancy.Security;
using System;
using WorkData.NancyHost.Cache;

namespace WorkData.NancyHost.Models.Tokens
{
    public class UserMapper : IUserMapper
    {
        private static readonly ICacheManager CacheManager =
            new RedisCacheManager();

        /// <summary>
        /// 根据token获取用户信息，检测用户是否有效
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public IUserIdentity GetUserFromAccessToken(string token)
        {
            var result = string.IsNullOrEmpty(token) ? null :
                CacheManager.Get<UserIdentity>(token);
            return result;
        }

        /// <summary>
        /// 生成一个新的token,并缓存
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string GenerateToken(string userName)
        {
            var token = Guid.NewGuid().ToString();
            //token有效期
            CacheManager.Set(token, new UserIdentity(userName), DateTime.Now.AddHours(1));
            return token;
        }
    }
}