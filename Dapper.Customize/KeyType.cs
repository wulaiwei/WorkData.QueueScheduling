namespace Dapper
{
    /// <summary>
    ///     键类别
    /// </summary>
    public enum KeyType
    {
        /// <summary>
        ///     属性不是键，Dapper不自动管理
        /// </summary>
        NotAKey,

        /// <summary>
        ///     属性是数据库生成的基于 integer 的自增长标识列
        /// </summary>
        Identity,

        /// <summary>
        ///     属性是一个 GUID 类型 Dapper 自动管理
        /// </summary>
        Guid,

        /// <summary>
        ///     属性是由用户自己管理
        /// </summary>
        Assigned,

        /// <summary>
        ///     属性是 Sequence
        /// </summary>
        Sequence
    }
}