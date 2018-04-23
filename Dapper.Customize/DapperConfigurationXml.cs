// ------------------------------------------------------------------------------
//    Copyright (C) 成都联宇创新科技有限公司 版权所有。
//
//    文件名：DapperConfigurationXml.cs
//    文件功能描述：
//    创建标识： 2015/07/07
//
//    修改标识：2015/07/15
//    修改描述：
//  ------------------------------------------------------------------------------

#region 导入名称空间

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Reflection;

#endregion 导入名称空间

namespace Dapper
{
    /// <summary>
    ///     XML格式配置解析
    /// </summary>
    internal class DapperConfigurationXml
    {
        private static readonly object LockObj = new Object();
        private static volatile IList<Assembly> _mappingAssemblies = new List<Assembly>();
        private static volatile string _connectionString;
        private static volatile ISqlDialect _dialect;
        private static volatile Type _connectionImpl;
        private static volatile DapperConfigurationXml _instance;
        private static volatile IDapperConfiguration _config;

        /// <summary>
        /// XML配置源解析构造函数
        /// </summary>
        /// <param name="connectionImpl">IDbConnection实现类，类型声明</param>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="assemblies">需要映射的程序集列表</param>
        public DapperConfigurationXml(Type connectionImpl, string connectionString, IList<Assembly> assemblies)
        {
            _connectionImpl = connectionImpl;
            _connectionString = connectionString;
            _dialect = DialectInference(connectionImpl);
            _mappingAssemblies = assemblies;
        }

        /// <summary>
        /// 获取Sql生成器
        /// </summary>
        /// <returns></returns>
        public ISqlGenerator GetSqlGenerator()
        {
            if (_config == null)
            {
                _config = new DapperConfiguration(_mappingAssemblies, _dialect);
            }

            var sqlGeneratorImpl = new SqlGeneratorImpl(_config);

            return sqlGeneratorImpl;
        }

        /// <summary>
        /// 获取链接
        /// </summary>
        /// <returns></returns>
        public IDbConnection GetConnection()
        {
            var connection = (IDbConnection)Activator.CreateInstance(_connectionImpl, _connectionString);
            return connection;
        }

        /// <summary>
        /// XML配置实例
        /// </summary>
        public static DapperConfigurationXml ConfigurationInstance
        {
            get
            {
                if (_instance != null) return _instance;
                lock (LockObj)
                {
                    return _instance ?? (_instance = (DapperConfigurationXml)ConfigurationManager.GetSection("dapper"));
                }
            }
        }

        /// <summary>
        ///     根据数据库连接类名推断当前要使用的数据库方言
        /// </summary>
        /// <param name="dbConnImplType"></param>
        /// <returns></returns>
        private static ISqlDialect DialectInference(Type dbConnImplType)
        {
            if (dbConnImplType.FullName.ToUpper().Contains("SQLCLIENT"))
            {
                return new SqlServer();
            }

            if (dbConnImplType.FullName.ToUpper().Contains("ORACLE"))
            {
                return new Oracle();
            }

            if (dbConnImplType.FullName.ToUpper().Contains("MYSQL"))
            {
                return new MySql();
            }

            if (dbConnImplType.FullName.ToUpper().Contains("NPGSQL"))
            {
                return new PostgreSql();
            }

            if (dbConnImplType.FullName.ToUpper().Contains("SQLCE"))
            {
                return new SqlCe();
            }

            if (dbConnImplType.FullName.ToUpper().Contains("SQLITE"))
            {
                return new Sqlite();
            }

            throw new NotSupportedException("目前还不支持:" + dbConnImplType.Name + "操作数据库");
        }
    }
}