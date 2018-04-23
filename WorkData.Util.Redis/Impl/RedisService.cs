// ------------------------------------------------------------------------------
// Copyright  吴来伟个人 版权所有。
// 项目名：WorkData.Util.Redis
// 文件名：RedisService.cs
// 创建标识：吴来伟 2018-03-21 14:22
// 创建描述：
//
// 修改标识：吴来伟2018-03-21 14:23
// 修改描述：
//  ------------------------------------------------------------------------------

#region

using Autofac.Extras.DynamicProxy;
using Newtonsoft.Json;
using ServiceStack.Redis;
using System;
using WorkData.Util.Redis.Entity;
using WorkData.Util.Redis.Interface;
using WorkData.Util.Redis.RealTime;

#endregion

namespace WorkData.Util.Redis.Impl
{
    /// <summary>
    ///     Redis服务
    /// </summary>
    [Intercept(typeof(RedisServiceInterceptor))]
    public class RedisService : IRedisService
    {
        /// <summary>
        ///  RedisClientManager
        /// </summary>
        public IRedisClient RedisClientManager { get; set; }

        /// <summary>
        ///     RedisService
        /// </summary>
        public RedisService()
        {
            RedisClientManager = NullRedis.Instance;
        }

        /// <summary>
        /// GetClient
        /// </summary>
        /// <returns></returns>
        public IRedisClient GetClient()
        {
            return RedisDataSource.CreateInstance(); ;
        }

        /// <summary>
        ///     Get
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string Get(string key)
        {
            return RedisClientManager.Get<string>(key);
        }

        /// <summary>
        ///     Add
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Add(string key, string value)
        {
            try
            {
                RedisClientManager.Add(key, value);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        ///     Add
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expireTime"></param>
        /// <returns></returns>
        public bool Add(string key, string value, DateTime expireTime)
        {
            try
            {
                RedisClientManager.Add(key, value, expireTime);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
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
        ///     BlockingPopQueue (TimeSpan 阻塞时间 默认为一直)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="sp"></param>
        /// <returns></returns>
        public T BlockingPopQueue<T>(string key, TimeSpan? sp) where T : class
        {
            var entityData = RedisClientManager.BlockingPopItemFromList(key, sp);
            return JsonConvert.DeserializeObject<T>(entityData);
        }

        /// <summary>
        ///     PopQueue
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T PopQueue<T>(string key) where T : class
        {
            var entityData = RedisClientManager.PopItemFromList(key);
            return entityData == null ? null : JsonConvert.DeserializeObject<T>(entityData);
        }

        /// <summary>
        /// 清空队列
        /// </summary>
        /// <param name="listId"></param>
        public void RemoveList(string listId)
        {
            RedisClientManager.Remove(listId);
        }

        /// <summary>
        ///     AddSet
        /// </summary>
        /// <param name="entity"></param>
        public void AddSet<T>(RedisQueue<T> entity) where T : class
        {
            RedisClientManager
                .AddItemToSet(entity.Key, entity.EntityData);
        }
    }
}