#region 导入名称空间

using System;
using System.Collections.Generic;

#endregion 导入名称空间

namespace Dapper
{
    /// <summary>
    ///     复合查询谓语
    /// </summary>
    public class GetMultiplePredicate
    {
        private readonly List<GetMultiplePredicateItem> _items;

        public GetMultiplePredicate()
        {
            _items = new List<GetMultiplePredicateItem>();
        }

        /// <summary>
        ///     复合查询谓语项列表
        /// </summary>
        public IEnumerable<GetMultiplePredicateItem> Items
        {
            get { return _items.AsReadOnly(); }
        }

        public void Add<T>(IPredicate predicate, IList<ISort> sort = null) where T : class
        {
            _items.Add(new GetMultiplePredicateItem
            {
                Value = predicate,
                Type = typeof(T),
                Sort = sort
            });
        }

        /// <summary>
        ///     加入（TODO：此方法有问题直接给ID查询出来的结果有错误）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        public void Add<T>(object id) where T : class
        {
            _items.Add(new GetMultiplePredicateItem
            {
                Value = id,
                Type = typeof(T)
            });
        }

        /// <summary>
        ///     复合查询谓语项
        /// </summary>
        public class GetMultiplePredicateItem
        {
            public object Value { get; set; }
            public Type Type { get; set; }
            public IList<ISort> Sort { get; set; }
        }
    }
}