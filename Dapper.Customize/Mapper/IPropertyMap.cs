using System.Reflection;

namespace Dapper
{
    /// <summary>
    ///     映射实体属性到数据库中相应的列
    /// </summary>
    public interface IPropertyMap
    {
        /// <summary>
        ///     Property 名称
        /// </summary>
        string Name { get; }

        /// <summary>
        ///     列名
        /// </summary>
        string ColumnName { get; }

        /// <summary>
        ///     Sequence
        /// </summary>
        string SequenceName { get; }

        /// <summary>
        ///     是否忽略
        /// </summary>
        bool Ignored { get; }

        /// <summary>
        ///     是否只读
        /// </summary>
        bool IsReadOnly { get; }

        /// <summary>
        ///     Key类型
        /// </summary>
        KeyType KeyType { get; }

        /// <summary>
        ///     属性对象
        /// </summary>
        PropertyInfo PropertyInfo { get; }
    }
}