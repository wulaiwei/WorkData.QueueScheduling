// ------------------------------------------------------------------------------
// Copyright  吴来伟个人 版权所有。
// 项目名：WorkData.Service.Core
// 文件名：DataSource.cs
// 创建标识：吴来伟 2018-03-22 15:04
// 创建描述：
//
// 修改标识：吴来伟2018-03-22 15:06
// 修改描述：
//  ------------------------------------------------------------------------------

#region

using FluentData;
using System.Configuration;

#endregion

namespace WorkData.Service.Core.FluentDataInfrastructure
{
    public class DataSource
    {
        private static string _connectionString = "";

        public static void SetConnectionString(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                _connectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
        }

        public static IDbContext CreateInstance()
        {
            //设置数据库连接
            SetConnectionString(null);

            IDbContext db = new DbContext();
            db.ConnectionString(_connectionString, new OracleProvider());
            db.CommandTimeout(1000 * 60);
            db.IgnoreIfAutoMapFails(true);
            db.IsolationLevel(IsolationLevel.ReadCommitted);

            return db;
        }
    }
}