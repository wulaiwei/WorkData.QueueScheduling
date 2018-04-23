// ------------------------------------------------------------------------------
//    Copyright (C) 成都联宇创新科技有限公司 版权所有。
//
//    文件名：PropertyAttribute.cs
//    文件功能描述：
//    创建标识：骆智慧 2015/05/11
//
//    修改标识：2015/05/11
//    修改描述：
//   ------------------------------------------------------------------------------

#region 导入名称空间

using System;

#endregion 导入名称空间

namespace Dapper
{
    /// <summary>
    ///     映射数据库字段
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class PropertyAttribute : Attribute
    {
        /// <summary>
        ///     数据库字段映射
        /// </summary>
        /// <param name="columnName">字段名</param>
        public PropertyAttribute(string columnName)
        {
            ColumnName = columnName;
            KeyType = KeyType.NotAKey;
            SequenceName = null;
        }

        public string SequenceName { get; set; }
        public string ColumnName { get; set; }
        public bool Ignored { get; set; }
        public bool IsReadOnly { get; set; }
        public KeyType KeyType { get; set; }
    }
}