// ------------------------------------------------------------------------------
// Copyright  吴来伟个人 版权所有。
// 项目名：WorkData.Util.Redis
// 文件名：RedisDataSource.cs
// 创建标识：吴来伟 2018-03-21 14:22
// 创建描述：
//
// 修改标识：吴来伟2018-03-21 14:24
// 修改描述：
//  ------------------------------------------------------------------------------

using ServiceStack.Redis;
using System.Configuration;

namespace WorkData.Util.Redis.RealTime
{
    public class RedisDataSource
    {
        #region 初始化配置

        private static string _connectionString = "";

        /// <summary>
        ///     SetConnectionString
        /// </summary>
        /// <param name="connectionString"></param>
        private static void SetConnectionString(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                _connectionString = ConfigurationManager
                    .ConnectionStrings["Redis"]
                    .ConnectionString;
        }

        #endregion 初始化配置

        /// <summary>
        ///     实例化
        /// </summary>
        /// <returns></returns>
        public static IRedisClient CreateInstance()
        {
            //设置连接
            SetConnectionString(null);
            var db = new RedisClient(_connectionString, 6379);

            return db;
        }
    }
}