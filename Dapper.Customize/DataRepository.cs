// ------------------------------------------------------------------------------
//    Copyright (C) 成都联宇创新科技有限公司 版权所有。
//
//    文件名：Repository.cs
//    文件功能描述：
//    创建标识：骆智慧 2015/06/28
//
//    修改标识：2015/06/28
//    修改描述：
//  ------------------------------------------------------------------------------

#region 导入名称空间

using System.Collections.Generic;

#endregion 导入名称空间

namespace Dapper
{
    /// <summary>
    /// 仓储接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDataRepository<T>
    {
        /// <summary>
        ///     查询
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        T GetById(dynamic id);

        /// <summary>
        ///     插入
        /// </summary>
        /// <param name="entities">实体列表</param>
        void Insert(IEnumerable<T> entities);

        /// <summary>
        ///     插入
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        dynamic Insert(T entity);

        bool Update(T entity);

        /// <summary>
        ///     删除
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        bool Delete(T entity);

        /// <summary>
        ///     删除
        /// </summary>
        /// <param name="predicate">谓语（条件）</param>
        /// <returns></returns>
        bool Delete(object predicate);

        /// <summary>
        ///     获取实体列表
        /// </summary>
        /// <param name="predicate">谓语（条件）</param>
        /// <param name="sort">排序列表</param>
        /// <returns></returns>
        IEnumerable<T> GetList(object predicate = null, IList<ISort> sort = null);

        /// <summary>
        ///     获取已分页的实体列表
        /// </summary>
        /// <param name="predicate">谓语（条件）</param>
        /// <param name="sort">排序列表</param>
        /// <param name="page"></param>
        /// <param name="resultsPerPage"></param>
        /// <returns></returns>
        IEnumerable<T> GetPage(IPredicateGroup predicate, IList<ISort> sort, int page, int resultsPerPage);

        /// <summary>
        ///     获取已分页的实体列表
        /// </summary>
        /// <param name="predicate">谓语（条件）</param>
        /// <param name="sort">排序列表</param>
        /// <param name="page"></param>
        /// <param name="resultsPerPage"></param>
        /// <returns></returns>
        IEnumerable<T> GetPage(object predicate, IList<ISort> sort, int page, int resultsPerPage);

        /// <summary>
        ///     获取集合结果
        /// </summary>
        /// <param name="predicate">谓语（条件）</param>
        /// <param name="sort">排序列表</param>
        /// <param name="firstResult">第一条结果</param>
        /// <param name="maxResults"></param>
        /// <returns></returns>
        IEnumerable<T> GetSet(object predicate, IList<ISort> sort, int firstResult, int maxResults);

        /// <summary>
        ///     获取记录条数
        /// </summary>
        /// <param name="predicate">谓语（条件）</param>
        /// <returns></returns>
        int Count(object predicate);

        /// <summary>
        ///     根据条件筛选出数据集合
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="buffered"></param>
        /// <returns></returns>
        IEnumerable<T> Query(string sql, dynamic param = null, bool buffered = true);

        /// <summary>
        ///     复合查询
        /// </summary>
        /// <param name="predicate">谓语</param>
        /// <returns></returns>
        IMultipleResultReader GetMultiple(GetMultiplePredicate predicate);
    }

    /// <summary>
    ///     数据仓储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DataRepository<T> : IDataRepository<T> where T : class
    {
        /// <summary>
        ///     查询
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public virtual T GetById(dynamic id)
        {
            using (var db = DbContext.CreateInstance())
            {
                return db.Get<T>(id);
            }
        }

        /// <summary>
        ///     插入
        /// </summary>
        /// <param name="entities">实体列表</param>
        public virtual void Insert(IEnumerable<T> entities)
        {
            using (var db = DbContext.CreateInstance())
            {
                db.Insert(entities);
            }
        }

        /// <summary>
        ///     插入
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public virtual dynamic Insert(T entity)
        {
            using (var db = DbContext.CreateInstance())
            {
                return db.Insert(entity);
            }
        }

        public virtual bool Update(T entity)
        {
            using (var db = DbContext.CreateInstance())
            {
                return db.Update(entity);
            }
        }

        /// <summary>
        ///     删除
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public virtual bool Delete(T entity)
        {
            using (var db = DbContext.CreateInstance())
            {
                return db.Delete(entity);
            }
        }

        /// <summary>
        ///     删除
        /// </summary>
        /// <param name="predicate">谓语（条件）</param>
        /// <returns></returns>
        public virtual bool Delete(object predicate)
        {
            using (var db = DbContext.CreateInstance())
            {
                return db.Delete(predicate);
            }
        }

        /// <summary>
        ///     获取实体列表
        /// </summary>
        /// <param name="predicate">谓语（条件）</param>
        /// <param name="sort">排序列表</param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetList(object predicate = null, IList<ISort> sort = null)
        {
            using (var db = DbContext.CreateInstance())
            {
                return db.GetList<T>(predicate, sort);
            }
        }

        /// <summary>
        ///     获取已分页的实体列表
        /// </summary>
        /// <param name="predicate">谓语（条件）</param>
        /// <param name="sort">排序列表</param>
        /// <param name="page"></param>
        /// <param name="resultsPerPage"></param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetPage(IPredicateGroup predicate, IList<ISort> sort, int page, int resultsPerPage)
        {
            using (var db = DbContext.CreateInstance())
            {
                return db.GetPage<T>(predicate, sort, page, resultsPerPage);
            }
        }

        /// <summary>
        ///     获取已分页的实体列表
        /// </summary>
        /// <param name="predicate">谓语（条件）</param>
        /// <param name="sort">排序列表</param>
        /// <param name="page"></param>
        /// <param name="resultsPerPage"></param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetPage(object predicate, IList<ISort> sort, int page, int resultsPerPage)
        {
            using (var db = DbContext.CreateInstance())
            {
                return db.GetPage<T>(predicate, sort, page, resultsPerPage);
            }
        }

        /// <summary>
        ///     获取集合结果
        /// </summary>
        /// <param name="predicate">谓语（条件）</param>
        /// <param name="sort">排序列表</param>
        /// <param name="firstResult">第一条结果</param>
        /// <param name="maxResults"></param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetSet(object predicate, IList<ISort> sort, int firstResult, int maxResults)
        {
            using (var db = DbContext.CreateInstance())
            {
                return db.GetSet<T>(predicate, sort, firstResult, maxResults, null, true);
            }
        }

        /// <summary>
        ///     获取记录条数
        /// </summary>
        /// <param name="predicate">谓语（条件）</param>
        /// <returns></returns>
        public virtual int Count(object predicate)
        {
            using (var db = DbContext.CreateInstance())
            {
                return db.Count<T>(predicate);
            }
        }

        /// <summary>
        ///     根据条件筛选出数据集合
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="buffered"></param>
        /// <returns></returns>
        public virtual IEnumerable<T> Query(string sql, dynamic param = null, bool buffered = true)
        {
            using (var db = DbContext.CreateInstance())
            {
                return db.Query<T>(sql, param, buffered);
            }
        }

        /// <summary>
        ///     复合查询
        /// </summary>
        /// <param name="predicate">谓语</param>
        /// <returns></returns>
        public virtual IMultipleResultReader GetMultiple(GetMultiplePredicate predicate)
        {
            using (var db = DbContext.CreateInstance())
            {
                return db.GetMultiple(predicate);
            }
        }
    }
}