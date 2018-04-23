// ------------------------------------------------------------------------------
// Copyright  吴来伟个人 版权所有。
// 项目名：WorkData.Util.Redis
// 文件名：NullRedis.cs
// 创建标识：吴来伟 2018-03-21 14:22
// 创建描述：
//
// 修改标识：吴来伟2018-03-21 14:24
// 修改描述：
//  ------------------------------------------------------------------------------

#region

using ServiceStack.Redis;
using WorkData.Util.Redis.RealTime;

#endregion

namespace WorkData.Util.Redis
{
    /// <summary>
    ///     NullRedis
    /// </summary>
    public class NullRedis
    {
        /// <summary>
        ///     Singleton Instance.
        /// </summary>
        public static IRedisClient Instance { get; } =
            RedisDataSource.CreateInstance();
    }
}