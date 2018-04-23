// ------------------------------------------------------------------------------
// Copyright  吴来伟个人 版权所有。
// 项目名：WorkData.Util.Redis
// 文件名：BaseRedisServiceManager.cs
// 创建标识：吴来伟 2018-03-21 14:22
// 创建描述：
//
// 修改标识：吴来伟2018-03-21 14:23
// 修改描述：
//  ------------------------------------------------------------------------------

#region

using ServiceStack.Redis;
using System;
using WorkData.Util.Redis.Entity;
using WorkData.Util.Redis.Interface;

#endregion

namespace WorkData.Util.Redis.Impl
{
    /// <summary>
    ///     BaseRedisService
    /// </summary>
    public class BaseRedisServiceManager
    {
        #region IOC

        private readonly IRedisService _redisService;

        public BaseRedisServiceManager(IRedisService redisService)
        {
            _redisService = redisService;
        }

        #endregion

        /// <summary>
        /// GetClient
        /// </summary>
        /// <returns></returns>
        public IRedisClient GetClient()
        {
            return _redisService.GetClient();
        }

        /// <summary>
        ///     key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string Get(string key)
        {
            return _redisService.Get(key);
        }

        /// <summary>
        ///     Add
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public bool Add(string key, string value)
        {
            return _redisService.Add(key, value);
        }

        /// <summary>
        ///     过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expireTime"></param>
        /// <returns></returns>
        public bool Add(string key, string value, DateTime expireTime)
        {
            return _redisService.Add(key, value, expireTime);
        }

        /// <summary>
        ///     AddQueue
        /// </summary>
        /// <param name="entity"></param>
        public void AddQueue<T>(RedisQueue<T> entity) where T : class
        {
            _redisService.AddQueue(entity);
        }

        /// <summary>
        ///     PopQueue
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public T PopQueue<T>(string key) where T : class
        {
            return _redisService.PopQueue<T>(key);
        }

        /// <summary>
        ///     AddSet
        /// </summary>
        /// <param name="entity"></param>
        public void AddSet<T>(RedisQueue<T> entity) where T : class
        {
            _redisService
                .AddSet(entity);
        }

        /// <summary>
        ///     BlockingPopQueue
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="sp"></param>
        /// <returns></returns>
        public T BlockingPopQueue<T>(string key, TimeSpan? sp) where T : class
        {
            return _redisService.BlockingPopQueue<T>(key, sp);
        }

        /// <summary>
        /// 清空队列
        /// </summary>
        /// <param name="listId"></param>
        public void RemoveList(string listId)
        {
            _redisService.RemoveList(listId);
        }
    }
}