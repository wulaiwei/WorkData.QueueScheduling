// ------------------------------------------------------------------------------
// Copyright  吴来伟个人 版权所有。
// 项目名：WorkData.Util.Redis
// 文件名：IRedisService.cs
// 创建标识：吴来伟 2018-03-21 14:22
// 创建描述：
//
// 修改标识：吴来伟2018-03-21 14:24
// 修改描述：
//  ------------------------------------------------------------------------------

#region

using ServiceStack.Redis;
using System;
using WorkData.Util.Redis.Entity;

#endregion

namespace WorkData.Util.Redis.Interface
{
    /// <summary>
    ///     IRedisService
    /// </summary>
    public interface IRedisService
    {
        /// <summary>
        /// GetClient
        /// </summary>
        /// <returns></returns>
        IRedisClient GetClient();

        /// <summary>
        ///     key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string Get(string key);

        /// <summary>
        ///     Add
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        bool Add(string key, string value);

        /// <summary>
        ///     Add
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expireTime"></param>
        bool Add(string key, string value, DateTime expireTime);

        /// <summary>
        ///     AddQueue
        /// </summary>
        /// <param name="entity"></param>
        void AddQueue<T>(RedisQueue<T> entity) where T : class;

        /// <summary>
        ///     PopQueue
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        T PopQueue<T>(string key) where T : class;

        /// <summary>
        /// 删除list
        /// </summary>
        /// <param name="listId"></param>
        void RemoveList(string listId);

        /// <summary>
        /// AddSet
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        void AddSet<T>(RedisQueue<T> entity) where T : class;

        /// <summary>
        ///     BlockingPopQueue
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="sp"></param>
        /// <returns></returns>
        T BlockingPopQueue<T>(string key, TimeSpan? sp) where T : class;
    }
}