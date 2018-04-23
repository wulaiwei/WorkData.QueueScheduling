#region 导入名称空间

using System;
using System.Reflection;

#endregion 导入名称空间

namespace Dapper
{
    /// <summary>
    ///     映射实体属性到数据库中相应的列
    /// </summary>
    internal class PropertyMap : IPropertyMap
    {
        public PropertyMap(PropertyInfo propertyInfo)
        {
            PropertyInfo = propertyInfo;
            ColumnName = PropertyInfo.Name;
        }

        /// <summary>
        ///     从指定的 propertyInfo 中获取属性名称.
        /// </summary>
        public string Name
        {
            get { return PropertyInfo.Name; }
        }

        /// <summary>
        ///     从当前属性获取列名
        /// </summary>
        public string ColumnName { get; private set; }

        /// <summary>
        ///     从当前属性获取 keyType
        /// </summary>
        public KeyType KeyType { get; private set; }

        /// <summary>
        ///     Sequence
        /// </summary>
        public string SequenceName { get; private set; }

        /// <summary>
        ///     Gets the ignore status of the current property. If ignored, the current property will not be included in queries.
        /// </summary>
        public bool Ignored { get; private set; }

        /// <summary>
        ///     是否只读？如果只读不会在 Insert 和 update 语句中包含此属性
        /// </summary>
        public bool IsReadOnly { get; private set; }

        /// <summary>
        ///     当前属性的 PropertyInfo
        /// </summary>
        public PropertyInfo PropertyInfo { get; private set; }

        /// <summary>
        ///     设置列名
        /// </summary>
        public PropertyMap Column(string columnName)
        {
            ColumnName = columnName;
            return this;
        }

        /// <summary>
        ///     设置Sequence Name
        /// </summary>
        /// <param name="sequenceName"></param>
        /// <returns></returns>
        public PropertyMap Sequence(string sequenceName)
        {
            SequenceName = sequenceName;
            return this;
        }

        /// <summary>
        ///     设置KeyType
        /// </summary>
        public PropertyMap Key(KeyType keyType)
        {
            if (Ignored)
            {
                throw new ArgumentException(string.Format("'{0}' 标记为 Ignore 不能作为 key ", Name));
            }

            if (IsReadOnly)
            {
                throw new ArgumentException(string.Format("'{0}' 标记为 readonly 不能作为 key. ", Name));
            }

            KeyType = keyType;
            return this;
        }

        /// <summary>
        ///     设置 当前属性 不包含在 Query 语句中
        /// </summary>
        public PropertyMap Ignore()
        {
            if (KeyType != KeyType.NotAKey)
            {
                throw new ArgumentException(string.Format("'{0}' 是 key 不能标记为 Ignore。", Name));
            }

            Ignored = true;
            return this;
        }

        /// <summary>
        ///     设置当前属性为 只读状态
        /// </summary>
        public PropertyMap ReadOnly()
        {
            if (KeyType != KeyType.NotAKey)
            {
                throw new ArgumentException(string.Format("'{0}' 是 key 不能标记为 Readonly。", Name));
            }

            IsReadOnly = true;
            return this;
        }
    }
}