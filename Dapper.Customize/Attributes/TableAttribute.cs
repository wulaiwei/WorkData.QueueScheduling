// ------------------------------------------------------------------------------
//    Copyright (C) 成都联宇创新科技有限公司 版权所有。
//
//    文件名：DapperAttribute.cs
//    文件功能描述：
//    创建标识：骆智慧 2015/05/11
//
//    修改标识：2015/05/11
//    修改描述：
// ------------------------------------------------------------------------------

#region 导入名称空间

using System;

#endregion 导入名称空间

namespace Dapper
{
    /// <summary>
    ///     映射Dapper POCO模型到数据库表
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = true)]
    public class TableAttribute : Attribute
    {
        public TableAttribute()
        {
        }

        /// <summary>
        ///     映射数据库表
        /// </summary>
        /// <param name="tableName">表名</param>
        public TableAttribute(string tableName)
        {
            TableName = tableName;
        }

        /// <summary>
        ///     映射数据库表
        /// </summary>
        /// <param name="schemaName">架构名</param>
        /// <param name="tableName">表名</param>
        public TableAttribute(string schemaName, string tableName)
            : this(tableName)
        {
            SchemaName = schemaName;
        }

        /// <summary>
        ///     数据库架构名称
        /// </summary>
        public string SchemaName { get; set; }

        /// <summary>
        ///     数据库表名
        /// </summary>
        public string TableName { get; set; }
    }
}