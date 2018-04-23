// ------------------------------------------------------------------------------
//    Copyright (C) 成都联宇创新科技有限公司 版权所有。
//
//    文件名：DbContext.cs
//    文件功能描述：
//    创建标识： 2015/07/07
//
//    修改标识：2015/07/15
//    修改描述：
//  ------------------------------------------------------------------------------

#region 导入名称空间

using System;
using System.Collections.Generic;
using System.Data;

#endregion 导入名称空间

namespace Dapper
{
    /// <summary>
    ///     数据库上下文接口
    /// </summary>
    public interface IDbContext : IDisposable
    {
        /// <summary>
        ///     有激活的事务
        /// </summary>
        bool HasActiveTransaction { get; }

        //IDbConnection Connection { get; }

        #region 事务控制

        /// <summary>
        ///     数据库连接
        /// </summary>
        /// <summary>
        ///     开始一个事务
        /// </summary>
        /// <param name="isolationLevel">事务锁定行为</param>
        void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);

        /// <summary>
        ///     提交事务
        /// </summary>
        void Commit();

        /// <summary>
        ///     回滚事务
        /// </summary>
        void Rollback();

        /// <summary>
        ///     在事务中运行
        /// </summary>
        /// <param name="action">需要在事务中运行的 Action 代理</param>
        void RunInTransaction(Action action);

        /// <summary>
        ///     在事务中允许
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="func">需要在事务中运行的 Func&lt;T&gt; 代理</param>
        /// <returns></returns>
        T RunInTransaction<T>(Func<T> func);

        #endregion 事务控制

        /// <summary>
        ///     清空缓存
        /// </summary>
        void ClearCache();

        /// <summary>
        ///     获取新GUID
        /// </summary>
        /// <returns></returns>
        Guid GetNextGuid();

        #region 根据ID查询单条数据

        /// <summary>
        ///     查询
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="id">id</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">SQL执行超时时间</param>
        /// <returns></returns>
        T Get<T>(dynamic id, IDbTransaction transaction, int? commandTimeout = null) where T : class;

        /// <summary>
        ///     查询
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="id">id</param>
        /// <param name="commandTimeout">SQL执行超时时间</param>
        /// <returns></returns>
        T Get<T>(dynamic id, int? commandTimeout = null) where T : class;

        #endregion 根据ID查询单条数据

        #region 插入数据

        /// <summary>
        ///     插入
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entities">实体列表</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">SQL执行超时时间</param>
        void Insert<T>(IEnumerable<T> entities, IDbTransaction transaction, int? commandTimeout = null) where T : class;

        /// <summary>
        ///     插入
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entities">实体列表</param>
        /// <param name="commandTimeout">SQL执行超时时间</param>
        void Insert<T>(IEnumerable<T> entities, int? commandTimeout = null) where T : class;

        /// <summary>
        ///     插入
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">SQL执行超时时间</param>
        /// <returns></returns>
        dynamic Insert<T>(T entity, IDbTransaction transaction, int? commandTimeout = null) where T : class;

        /// <summary>
        ///     插入
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体</param>
        /// <param name="commandTimeout">SQL执行超时时间</param>
        /// <returns></returns>
        dynamic Insert<T>(T entity, int? commandTimeout = null) where T : class;

        #endregion 插入数据

        #region 更新

        /// <summary>
        ///     更新
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">SQL执行超时时间</param>
        /// <returns></returns>
        bool Update<T>(T entity, IDbTransaction transaction, int? commandTimeout = null) where T : class;

        /// <summary>
        ///     更新
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体</param>
        /// <param name="commandTimeout">SQL执行超时时间</param>
        /// <returns></returns>
        bool Update<T>(T entity, int? commandTimeout = null) where T : class;

        #endregion 更新

        #region 删除

        /// <summary>
        ///     删除
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">SQL执行超时时间</param>
        /// <returns></returns>
        bool Delete<T>(T entity, IDbTransaction transaction, int? commandTimeout = null) where T : class;

        /// <summary>
        ///     删除
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体</param>
        /// <param name="commandTimeout">SQL执行超时时间</param>
        /// <returns></returns>
        bool Delete<T>(T entity, int? commandTimeout = null) where T : class;

        /// <summary>
        ///     删除
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="predicate">谓语（条件）</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">SQL执行超时时间</param>
        /// <returns></returns>
        bool Delete<T>(object predicate, IDbTransaction transaction, int? commandTimeout = null) where T : class;

        /// <summary>
        ///     删除
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="predicate">谓语（条件）</param>
        /// <param name="commandTimeout">SQL执行超时时间</param>
        /// <returns></returns>
        bool Delete<T>(object predicate, int? commandTimeout = null) where T : class;

        #endregion 删除

        #region 获取实体列表/集合/分页数据

        /// <summary>
        ///     获取实体列表
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="predicate">谓语（条件）</param>
        /// <param name="sort">排序列表</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">SQL执行超时时间</param>
        /// <param name="buffered">是否缓存结果</param>
        /// <returns></returns>
        IEnumerable<T> GetList<T>(object predicate, IList<ISort> sort, IDbTransaction transaction,
            int? commandTimeout = null, bool buffered = true) where T : class;

        /// <summary>
        ///     获取实体列表
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="predicate">谓语（条件）</param>
        /// <param name="sort">排序列表</param>
        /// <param name="commandTimeout">SQL执行超时时间</param>
        /// <param name="buffered">是否缓存结果</param>
        /// <returns></returns>
        IEnumerable<T> GetList<T>(object predicate = null, IList<ISort> sort = null, int? commandTimeout = null,
            bool buffered = true) where T : class;

        /// <summary>
        ///     获取已分页的实体列表
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="predicate">谓语（条件）</param>
        /// <param name="sort">排序列表</param>
        /// <param name="page"></param>
        /// <param name="resultsPerPage"></param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">SQL执行超时时间</param>
        /// <param name="buffered">是否缓存结果</param>
        /// <returns></returns>
        IEnumerable<T> GetPage<T>(IPredicateGroup predicate, IList<ISort> sort, int page, int resultsPerPage,
            IDbTransaction transaction, int? commandTimeout = null, bool buffered = true) where T : class;

        /// <summary>
        ///     获取已分页的实体列表
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="predicate">谓语（条件）</param>
        /// <param name="sort">排序列表</param>
        /// <param name="page"></param>
        /// <param name="resultsPerPage"></param>
        /// <param name="commandTimeout">SQL执行超时时间</param>
        /// <param name="buffered">是否缓存结果</param>
        /// <returns></returns>
        IEnumerable<T> GetPage<T>(object predicate, IList<ISort> sort, int page, int resultsPerPage,
            int? commandTimeout = null, bool buffered = true) where T : class;

        /// <summary>
        ///     获取集合结果
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="predicate">谓语（条件）</param>
        /// <param name="sort">排序列表</param>
        /// <param name="firstResult">第一条结果</param>
        /// <param name="maxResults"></param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">SQL执行超时时间</param>
        /// <param name="buffered">是否缓存结果</param>
        /// <returns></returns>
        IEnumerable<T> GetSet<T>(object predicate, IList<ISort> sort, int firstResult, int maxResults,
            IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class;

        /// <summary>
        ///     获取集合结果
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="predicate">谓语（条件）</param>
        /// <param name="sort">排序列表</param>
        /// <param name="firstResult">第一条结果</param>
        /// <param name="maxResults"></param>
        /// <param name="commandTimeout">SQL执行超时时间</param>
        /// <param name="buffered">是否缓存结果</param>
        /// <returns></returns>
        IEnumerable<T> GetSet<T>(object predicate, IList<ISort> sort, int firstResult, int maxResults,
            int? commandTimeout, bool buffered) where T : class;

        #endregion 获取实体列表/集合/分页数据

        #region 查询记录条数

        /// <summary>
        ///     获取记录条数
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="predicate">谓语（条件）</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">SQL执行超时时间</param>
        /// <returns></returns>
        int Count<T>(object predicate, IDbTransaction transaction, int? commandTimeout = null) where T : class;

        /// <summary>
        ///     获取记录条数
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="predicate">谓语（条件）</param>
        /// <param name="commandTimeout">SQL执行超时时间</param>
        /// <returns></returns>
        int Count<T>(object predicate, int? commandTimeout = null) where T : class;

        #endregion 查询记录条数

        #region 查询数据集合

        /// <summary>
        ///     根据条件筛选出数据集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="buffered"></param>
        /// <returns></returns>
        IEnumerable<T> Query<T>(string sql, dynamic param = null, bool buffered = true) where T : class;

        /// <summary>
        ///     根据条件筛选出数据集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="trans"></param>
        /// <param name="buffered"></param>
        /// <returns></returns>
        IEnumerable<T> Query<T>(string sql, dynamic param = null, IDbTransaction trans = null, bool buffered = true) where T : class;

        /// <summary>
        ///     根据条件筛选出数据集合
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="trans"></param>
        /// <param name="buffered"></param>
        /// <returns></returns>
        IEnumerable<dynamic> Query(string sql, dynamic param = null, IDbTransaction trans = null, bool buffered = true);

        /// <summary>
        ///     根据表达式筛选
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="sql"></param>
        /// <param name="map"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="buffered"></param>
        /// <param name="splitOn"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        IEnumerable<TReturn> Query<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, dynamic param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null);

        #endregion 查询数据集合

        #region 复合查询

        /// <summary>
        ///     复合查询
        /// </summary>
        /// <param name="predicate">谓语</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">SQL执行超时时间</param>
        /// <returns></returns>
        IMultipleResultReader GetMultiple(GetMultiplePredicate predicate, IDbTransaction transaction,
            int? commandTimeout = null);

        /// <summary>
        ///     复合查询
        /// </summary>
        /// <param name="predicate">谓语</param>
        /// <param name="commandTimeout">SQL执行超时时间</param>
        /// <returns></returns>
        IMultipleResultReader GetMultiple(GetMultiplePredicate predicate, int? commandTimeout = null);

        #endregion 复合查询

        #region 查询结果转换

        /// <summary>
        ///     转换 DapperRow 到实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Transform<T>(dynamic dapperRow) where T : class;

        #endregion 查询结果转换
    }

    /// <summary>
    ///     数据库上下文
    /// </summary>
    public class DbContext : IDbContext
    {
        private static volatile ObjectPool<IDbContext> ObjectPool = new ObjectPool<IDbContext>(() => new DbContext(true));
        private readonly IDapperImplementor _dapperImplementor;
        private volatile bool _enablePool = true;
        private IDbTransaction _transaction;
        private IDbConnection _connection;

        /// <summary>
        ///     有激活的事务
        /// </summary>
        public bool HasActiveTransaction
        {
            get { return _transaction != null; }
        }

        private DbContext(bool enablePool)
        {
            _enablePool = enablePool;
            var sqlGenerator = DapperConfigurationXml.ConfigurationInstance.GetSqlGenerator();
            _dapperImplementor = new DapperImplementor(sqlGenerator);
            _connection = DapperConfigurationXml.ConfigurationInstance.GetConnection();
        }

        /// <summary>
        ///     释放资源
        /// </summary>
        public void Dispose()
        {
            //阻止GC调用析构函数
            //GC.SuppressFinalize(this);

            #region 关闭数据库连接

            if (_connection.State != ConnectionState.Closed)
            {
                if (_transaction != null)
                {
                    _transaction.Rollback();
                }

                _connection.Close();
            }

            #endregion 关闭数据库连接

            if (_enablePool)
            {
                //将对象放回池中
                ObjectPool.PutObject(this);
            }
        }

        /// <summary>
        ///     创建DbContext实例
        /// </summary>
        /// <returns></returns>
        public static IDbContext CreateInstance(bool enablePool = true)
        {
            var dbContext = enablePool ? ObjectPool.GetObject() as DbContext : new DbContext(false);
            if (dbContext._connection.State != ConnectionState.Open)
            {
                dbContext._connection.Open();
            }

            return dbContext;
        }

        /// <summary>
        ///     转换 DapperRow 到实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Transform<T>(dynamic dapperRow) where T : class
        {
            return _dapperImplementor.Transform<T>(dapperRow);
        }

        /// <summary>
        ///     清空缓存
        /// </summary>
        public void ClearCache()
        {
            _dapperImplementor.SqlGenerator.Configuration.ClearCache();
        }

        /// <summary>
        ///     获取新GUID
        /// </summary>
        /// <returns></returns>
        public Guid GetNextGuid()
        {
            return _dapperImplementor.SqlGenerator.Configuration.GetNextGuid();
        }

        #region 事务控制

        /// <summary>
        ///     开始事务
        /// </summary>
        /// <param name="isolationLevel"></param>
        public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            _transaction = _connection.BeginTransaction(isolationLevel);
        }

        /// <summary>
        ///     提交事务
        /// </summary>
        public void Commit()
        {
            _transaction.Commit();
            _transaction = null;
        }

        /// <summary>
        ///     回滚事务
        /// </summary>
        public void Rollback()
        {
            _transaction.Rollback();
            _transaction = null;
        }

        /// <summary>
        ///     在事务中运行
        /// </summary>
        /// <param name="action">需要在事务中运行的 Action 代理</param>
        public void RunInTransaction(Action action)
        {
            BeginTransaction();
            try
            {
                action();
                Commit();
            }
            catch (Exception)
            {
                if (HasActiveTransaction)
                {
                    Rollback();
                }

                throw;
            }
        }

        /// <summary>
        ///     在事务中运行
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="func">需要在事务中运行的 Func&lt;T&gt; 代理</param>
        /// <returns></returns>
        public T RunInTransaction<T>(Func<T> func)
        {
            BeginTransaction();
            try
            {
                var result = func();
                Commit();
                return result;
            }
            catch (Exception)
            {
                if (HasActiveTransaction)
                {
                    Rollback();
                }

                throw;
            }
        }

        #endregion 事务控制

        #region 根据ID查询单条数据

        /// <summary>
        ///     查询
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="id">id</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">SQL执行超时时间</param>
        /// <returns></returns>
        public T Get<T>(dynamic id, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            return (T)_dapperImplementor.Get<T>(_connection, id, transaction, commandTimeout);
        }

        /// <summary>
        ///     查询
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="commandTimeout">SQL执行超时时间</param>
        /// <returns></returns>
        public T Get<T>(dynamic id, int? commandTimeout) where T : class
        {
            return (T)_dapperImplementor.Get<T>(_connection, id, _transaction, commandTimeout);
        }

        #endregion 根据ID查询单条数据

        #region 插入

        /// <summary>
        ///     插入
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entities">实体列表</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">SQL执行超时时间</param>
        public void Insert<T>(IEnumerable<T> entities, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            _dapperImplementor.Insert(_connection, entities, transaction, commandTimeout);
        }

        /// <summary>
        ///     插入
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entities">实体列表</param>
        /// <param name="commandTimeout">SQL执行超时时间</param>
        public void Insert<T>(IEnumerable<T> entities, int? commandTimeout) where T : class
        {
            _dapperImplementor.Insert(_connection, entities, _transaction, commandTimeout);
        }

        /// <summary>
        ///     插入
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">SQL执行超时时间</param>
        /// <returns></returns>
        public dynamic Insert<T>(T entity, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            return _dapperImplementor.Insert(_connection, entity, transaction, commandTimeout);
        }

        /// <summary>
        ///     插入
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体</param>
        /// <param name="commandTimeout">SQL执行超时时间</param>
        /// <returns></returns>
        public dynamic Insert<T>(T entity, int? commandTimeout) where T : class
        {
            return _dapperImplementor.Insert(_connection, entity, _transaction, commandTimeout);
        }

        #endregion 插入

        #region 更新

        /// <summary>
        ///     更新
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">SQL执行超时时间</param>
        /// <returns></returns>
        public bool Update<T>(T entity, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            return _dapperImplementor.Update(_connection, entity, transaction, commandTimeout);
        }

        /// <summary>
        ///     更新
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体</param>
        /// <param name="commandTimeout">SQL执行超时时间</param>
        /// <returns></returns>
        public bool Update<T>(T entity, int? commandTimeout) where T : class
        {
            return _dapperImplementor.Update(_connection, entity, _transaction, commandTimeout);
        }

        #endregion 更新

        #region 删除

        /// <summary>
        ///     删除
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">SQL执行超时时间</param>
        /// <returns></returns>
        public bool Delete<T>(T entity, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            return _dapperImplementor.Delete(_connection, entity, transaction, commandTimeout);
        }

        /// <summary>
        ///     删除
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体</param>
        /// <param name="commandTimeout">SQL执行超时时间</param>
        /// <returns></returns>
        public bool Delete<T>(T entity, int? commandTimeout) where T : class
        {
            return _dapperImplementor.Delete(_connection, entity, _transaction, commandTimeout);
        }

        /// <summary>
        ///     删除
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="predicate">谓语（条件）</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">SQL执行超时时间</param>
        /// <returns></returns>
        public bool Delete<T>(object predicate, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            return _dapperImplementor.Delete<T>(_connection, predicate, transaction, commandTimeout);
        }

        /// <summary>
        ///     删除
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="predicate">谓语（条件）</param>
        /// <param name="commandTimeout">SQL执行超时时间</param>
        /// <returns></returns>
        public bool Delete<T>(object predicate, int? commandTimeout) where T : class
        {
            return _dapperImplementor.Delete<T>(_connection, predicate, _transaction, commandTimeout);
        }

        #endregion 删除

        #region 获取实体列表/集合/分页数据

        /// <summary>
        ///     获取实体列表
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="predicate">谓语（条件）</param>
        /// <param name="sort">排序列表</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">SQL执行超时时间</param>
        /// <param name="buffered">是否缓存结果</param>
        /// <returns></returns>
        public IEnumerable<T> GetList<T>(object predicate, IList<ISort> sort, IDbTransaction transaction,
            int? commandTimeout, bool buffered) where T : class
        {
            return _dapperImplementor.GetList<T>(_connection, predicate, sort, transaction, commandTimeout, buffered);
        }

        /// <summary>
        ///     获取实体列表
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="predicate">谓语（条件）</param>
        /// <param name="sort">排序列表</param>
        /// <param name="commandTimeout">SQL执行超时时间</param>
        /// <param name="buffered">是否缓存结果</param>
        /// <returns></returns>
        public IEnumerable<T> GetList<T>(object predicate, IList<ISort> sort, int? commandTimeout, bool buffered)
            where T : class
        {
            return _dapperImplementor.GetList<T>(_connection, predicate, sort, _transaction, commandTimeout, buffered);
        }

        /// <summary>
        ///     获取已分页的实体列表
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="predicate">谓语（条件）</param>
        /// <param name="sort">排序列表</param>
        /// <param name="page"></param>
        /// <param name="resultsPerPage"></param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">SQL执行超时时间</param>
        /// <param name="buffered">是否缓存结果</param>
        /// <returns></returns>
        public IEnumerable<T> GetPage<T>(IPredicateGroup predicate, IList<ISort> sort, int page, int resultsPerPage,
            IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class
        {
            return _dapperImplementor.GetPage<T>(_connection, predicate, sort, page, resultsPerPage, transaction, commandTimeout,
                buffered);
        }

        /// <summary>
        ///     获取已分页的实体列表
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="predicate">谓语（条件）</param>
        /// <param name="sort">排序列表</param>
        /// <param name="page"></param>
        /// <param name="resultsPerPage"></param>
        /// <param name="commandTimeout">SQL执行超时时间</param>
        /// <param name="buffered">是否缓存结果</param>
        /// <returns></returns>
        public IEnumerable<T> GetPage<T>(object predicate, IList<ISort> sort, int page, int resultsPerPage,
            int? commandTimeout, bool buffered) where T : class
        {
            return _dapperImplementor.GetPage<T>(_connection, predicate, sort, page, resultsPerPage, _transaction, commandTimeout,
                buffered);
        }

        /// <summary>
        ///     获取集合结果
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="predicate">谓语（条件）</param>
        /// <param name="sort">排序列表</param>
        /// <param name="firstResult">第一条结果</param>
        /// <param name="maxResults"></param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">SQL执行超时时间</param>
        /// <param name="buffered">是否缓存结果</param>
        /// <returns></returns>
        public IEnumerable<T> GetSet<T>(object predicate, IList<ISort> sort, int firstResult, int maxResults,
            IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class
        {
            return _dapperImplementor.GetSet<T>(_connection, predicate, sort, firstResult, maxResults, transaction, commandTimeout,
                buffered);
        }

        /// <summary>
        ///     获取集合结果
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="predicate">谓语（条件）</param>
        /// <param name="sort">排序列表</param>
        /// <param name="firstResult">第一条结果</param>
        /// <param name="maxResults"></param>
        /// <param name="commandTimeout">SQL执行超时时间</param>
        /// <param name="buffered">是否缓存结果</param>
        /// <returns></returns>
        public IEnumerable<T> GetSet<T>(object predicate, IList<ISort> sort, int firstResult, int maxResults,
            int? commandTimeout, bool buffered) where T : class
        {
            return _dapperImplementor.GetSet<T>(_connection, predicate, sort, firstResult, maxResults, _transaction, commandTimeout,
                buffered);
        }

        #endregion 获取实体列表/集合/分页数据

        #region 获取记录条数

        /// <summary>
        ///     获取记录条数
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="predicate">谓语（条件）</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">SQL执行超时时间</param>
        /// <returns></returns>
        public int Count<T>(object predicate, IDbTransaction transaction, int? commandTimeout) where T : class
        {
            return _dapperImplementor.Count<T>(_connection, predicate, transaction, commandTimeout);
        }

        /// <summary>
        ///     获取记录条数
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="predicate">谓语（条件）</param>
        /// <param name="commandTimeout">SQL执行超时时间</param>
        /// <returns></returns>
        public int Count<T>(object predicate, int? commandTimeout) where T : class
        {
            return _dapperImplementor.Count<T>(_connection, predicate, _transaction, commandTimeout);
        }

        #endregion 获取记录条数

        #region 复合查询|多语句查询

        /// <summary>
        ///     复合查询
        /// </summary>
        /// <param name="predicate">谓语</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">SQL执行超时时间</param>
        /// <returns></returns>
        public IMultipleResultReader GetMultiple(GetMultiplePredicate predicate, IDbTransaction transaction,
            int? commandTimeout)
        {
            return _dapperImplementor.GetMultiple(_connection, predicate, transaction, commandTimeout);
        }

        /// <summary>
        ///     复合查询
        /// </summary>
        /// <param name="predicate">谓语</param>
        /// <param name="commandTimeout">SQL执行超时时间</param>
        /// <returns></returns>
        public IMultipleResultReader GetMultiple(GetMultiplePredicate predicate, int? commandTimeout)
        {
            return _dapperImplementor.GetMultiple(_connection, predicate, _transaction, commandTimeout);
        }

        #endregion 复合查询|多语句查询

        #region 查询数据集合

        /// <summary>
        ///     根据条件筛选出数据集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="buffered"></param>
        /// <returns></returns>
        public IEnumerable<T> Query<T>(string sql, dynamic param = null, bool buffered = true) where T : class
        {
            return _connection.Query<T>(sql, param as object, null, buffered);
        }

        /// <summary>
        ///     根据条件筛选出数据集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="trans"></param>
        /// <param name="buffered"></param>
        /// <returns></returns>
        public IEnumerable<T> Query<T>(string sql, dynamic param = null, IDbTransaction trans = null, bool buffered = true) where T : class
        {
            return _connection.Query<T>(sql, param as object, trans, buffered);
        }

        /// <summary>
        ///     根据条件筛选出数据集合
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="trans"></param>
        /// <param name="buffered"></param>
        /// <returns></returns>
        public IEnumerable<dynamic> Query(string sql, dynamic param = null, IDbTransaction trans = null, bool buffered = true)
        {
            return _connection.Query(sql, param as object, trans, buffered);
        }

        /// <summary>
        ///     根据表达式筛选
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="sql"></param>
        /// <param name="map"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="buffered"></param>
        /// <param name="splitOn"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public IEnumerable<TReturn> Query<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, dynamic param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id",
            int? commandTimeout = null)
        {
            return _connection.Query(sql, map, param as object, transaction, buffered, splitOn);
        }

        #endregion 查询数据集合
    }
}