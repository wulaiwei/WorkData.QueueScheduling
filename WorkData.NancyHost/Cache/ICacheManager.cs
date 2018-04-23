// ------------------------------------------------------------------------------
// Copyright  吴来伟个人 版权所有。
// 项目名：WorkData.Util.Common
// 文件名：ICacheManager.cs
// 创建标识：吴来伟 2018-03-28 16:50
// 创建描述：
//
// 修改标识：吴来伟2018-03-28 16:53
// 修改描述：
//  ------------------------------------------------------------------------------

#region

using System;
using WorkData.Util.Redis.Entity;

#endregion

namespace WorkData.NancyHost.Cache
{
    public interface ICacheManager
    {
        /// <summary>
        ///     获取
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        TEntity Get<TEntity>(string key);

        /// <summary>
        ///     设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="cacheTime"></param>
        void Set(string key, object value, DateTime cacheTime);

        /// <summary>
        ///     判断是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool Contains(string key);

        /// <summary>
        ///     移除
        /// </summary>
        /// <param name="key"></param>
        void Remove(string key);

        /// <summary>
        /// 添加队列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        void AddQueue<T>(RedisQueue<T> entity) where T : class;

        /// <summary>
        /// 添加至set
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        void AddSet<T>(RedisQueue<T> entity) where T : class;
    }
}