// ------------------------------------------------------------------------------
// Copyright  吴来伟个人 版权所有。
// 项目名：WorkData.NancyHost
// 文件名：RedisCacheManager.cs
// 创建标识：吴来伟 2018-04-20 10:30
// 创建描述：
//
// 修改标识：吴来伟2018-04-20 11:28
// 修改描述：
//  ------------------------------------------------------------------------------

#region

using ServiceStack.Redis;
using System;
using WorkData.Util.Redis;
using WorkData.Util.Redis.Entity;

#endregion

namespace WorkData.NancyHost.Cache
{
    public class RedisCacheManager : ICacheManager
    {
        public IRedisClient RedisClientManager { get; set; }

        public RedisCacheManager()
        {
            RedisClientManager = NullRedis.Instance;
        }

        /// <summary>
        ///     Contains
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Contains(string key)
        {
            return RedisClientManager.ContainsKey(key);
        }

        public TEntity Get<TEntity>(string key)
        {
            return RedisClientManager.Get<TEntity>(key);
        }

        /// <summary>
        ///     Remove
        /// </summary>
        /// <param name="key"></param>
        public void Remove(string key)
        {
            RedisClientManager.Remove(key);
        }

        /// <summary>
        ///     Set
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="cacheTime"></param>
        public void Set(string key, object value, DateTime cacheTime)
        {
            RedisClientManager.Add(key, value, cacheTime);
        }

        /// <summary>
        ///     AddQueue
        /// </summary>
        /// <param name="entity"></param>
        public void AddQueue<T>(RedisQueue<T> entity) where T : class
        {
            RedisClientManager
                .AddItemToList(entity.Key, entity.EntityData);
        }

        /// <summary>
        ///     AddQueue
        /// </summary>
        /// <param name="entity"></param>
        public void AddSet<T>(RedisQueue<T> entity) where T : class
        {
            RedisClientManager
                .AddItemToSet(entity.Key, entity.EntityData);
        }
    }
}