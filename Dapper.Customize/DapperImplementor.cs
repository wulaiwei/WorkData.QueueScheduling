#region 导入名称空间

using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;

#endregion 导入名称空间

namespace Dapper
{
    /// <summary>
    ///     Dapper实现者接口
    /// </summary>
    public interface IDapperImplementor
    {
        /// <summary>
        ///     SQL生成器
        /// </summary>
        ISqlGenerator SqlGenerator { get; }

        /// <summary>
        ///     查询
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="connection">数据库连接</param>
        /// <param name="id">id</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">执行超时时间</param>
        /// <returns></returns>
        T Get<T>(IDbConnection connection, dynamic id, IDbTransaction transaction, int? commandTimeout) where T : class;

        /// <summary>
        ///     插入
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="connection">数据库连接</param>
        /// <param name="entities">实体列表</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">执行超时时间</param>
        void Insert<T>(IDbConnection connection, IEnumerable<T> entities, IDbTransaction transaction,
            int? commandTimeout) where T : class;

        /// <summary>
        ///     插入
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="connection">数据库连接</param>
        /// <param name="entity">实体</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">执行超时时间</param>
        /// <returns></returns>
        dynamic Insert<T>(IDbConnection connection, T entity, IDbTransaction transaction, int? commandTimeout)
            where T : class;

        /// <summary>
        ///     更新
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="connection">数据库连接</param>
        /// <param name="entity">实体</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">执行超时时间</param>
        /// <returns></returns>
        bool Update<T>(IDbConnection connection, T entity, IDbTransaction transaction, int? commandTimeout)
            where T : class;

        /// <summary>
        ///     删除
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="connection">数据库连接</param>
        /// <param name="entity">实体</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">执行超时时间</param>
        /// <returns></returns>
        bool Delete<T>(IDbConnection connection, T entity, IDbTransaction transaction, int? commandTimeout)
            where T : class;

        /// <summary>
        ///     删除
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="connection">数据库连接</param>
        /// <param name="predicate">谓语（条件）</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">执行超时</param>
        /// <returns></returns>
        bool Delete<T>(IDbConnection connection, object predicate, IDbTransaction transaction, int? commandTimeout)
            where T : class;

        /// <summary>
        ///     查询实体列表
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="connection">数据库连接</param>
        /// <param name="predicate">谓语（条件）</param>
        /// <param name="sort">排序</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">执行超时</param>
        /// <param name="buffered">是否缓存</param>
        /// <returns></returns>
        IEnumerable<T> GetList<T>(IDbConnection connection, object predicate, IList<ISort> sort,
            IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class;

        /// <summary>
        ///     获取分页数据
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="connection">数据库连接</param>
        /// <param name="predicate">谓语（条件）</param>
        /// <param name="sort">排序</param>
        /// <param name="page"></param>
        /// <param name="resultsPerPage"></param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">超时时间</param>
        /// <param name="buffered">是否缓存</param>
        /// <returns></returns>
        IEnumerable<T> GetPage<T>(IDbConnection connection, IPredicateGroup predicate, IList<ISort> sort, int page,
            int resultsPerPage, IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class;

        /// <summary>
        ///     获取分页数据
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="connection">数据库连接</param>
        /// <param name="predicate">谓语（条件）</param>
        /// <param name="sort">排序</param>
        /// <param name="page"></param>
        /// <param name="resultsPerPage"></param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">超时时间</param>
        /// <param name="buffered">是否缓存</param>
        /// <returns></returns>
        IEnumerable<T> GetPage<T>(IDbConnection connection, object predicate, IList<ISort> sort, int page,
            int resultsPerPage, IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class;

        /// <summary>
        ///     获取实体集合
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="connection">数据库连接</param>
        /// <param name="predicate">谓语（条件）</param>
        /// <param name="sort">排序</param>
        /// <param name="firstResult"></param>
        /// <param name="maxResults"></param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">超时时间</param>
        /// <param name="buffered">是否缓存</param>
        /// <returns></returns>
        IEnumerable<T> GetSet<T>(IDbConnection connection, object predicate, IList<ISort> sort, int firstResult,
            int maxResults, IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class;

        /// <summary>
        ///     获取记录条数
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="connection">数据库连接</param>
        /// <param name="predicate">谓语（条件）</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">执行超时</param>
        /// <returns></returns>
        int Count<T>(IDbConnection connection, object predicate, IDbTransaction transaction, int? commandTimeout)
            where T : class;

        /// <summary>
        ///     复合查询
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="predicate">谓语（条件）</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">执行超时</param>
        /// <returns></returns>
        IMultipleResultReader GetMultiple(IDbConnection connection, GetMultiplePredicate predicate,
            IDbTransaction transaction, int? commandTimeout);

        /// <summary>
        ///     转换 DapperRow 到 EntityType
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataRow"></param>
        /// <returns></returns>
        T Transform<T>(dynamic dataRow) where T : class;
    }

    internal class DapperImplementor : IDapperImplementor
    {
        public DapperImplementor(ISqlGenerator sqlGenerator)
        {
            SqlGenerator = sqlGenerator;
        }

        public ISqlGenerator SqlGenerator { get; private set; }

        public T Get<T>(IDbConnection connection, dynamic id, IDbTransaction transaction, int? commandTimeout)
            where T : class
        {
            var classMap = SqlGenerator.Configuration.GetMap<T>();
            IPredicate predicate = GetIdPredicate(classMap, id);
            var result = GetList<T>(connection, classMap, predicate, null, transaction, commandTimeout, true).SingleOrDefault();
            return result;
        }

        public void Insert<T>(IDbConnection connection, IEnumerable<T> entities, IDbTransaction transaction,
            int? commandTimeout) where T : class
        {
            var classMap = SqlGenerator.Configuration.GetMap<T>();
            var sql = SqlGenerator.Insert(classMap);
            var keyPrperties = classMap.Properties.Where(p => p.KeyType != KeyType.NotAKey);
            foreach (var entity in entities)
            {
                foreach (var column in keyPrperties)
                {
                    switch (column.KeyType)
                    {
                        case KeyType.Guid:
                            var comb = SqlGenerator.Configuration.GetNextGuid();
                            column.PropertyInfo.SetValue(entity, comb, null);
                            break;

                        case KeyType.Sequence:
                            SetValueWithSequence(connection, column, entity);
                            break;

                        default: break;
                    }
                }
            }

            connection.Execute(sql, entities, transaction, commandTimeout, CommandType.Text);
        }

        private void SetValueWithSequence<T>(IDbConnection connection, IPropertyMap column, T entity)
        {
            var seqVal = connection.ExecuteScalar(SqlGenerator.Configuration.Dialect.GetNextSequence(column.SequenceName));

            if (column.PropertyInfo.PropertyType == typeof(Int16))
            {
                column.PropertyInfo.SetValue(entity, Convert.ToInt16(seqVal), null);
            }
            if (column.PropertyInfo.PropertyType == typeof(Int32))
            {
                column.PropertyInfo.SetValue(entity, Convert.ToInt32(seqVal), null);
            }
            if (column.PropertyInfo.PropertyType == typeof(Int64))
            {
                column.PropertyInfo.SetValue(entity, Convert.ToInt64(seqVal), null);
            }
            if (column.PropertyInfo.PropertyType == typeof(UInt16))
            {
                column.PropertyInfo.SetValue(entity, Convert.ToUInt16(seqVal), null);
            }
            if (column.PropertyInfo.PropertyType == typeof(UInt32))
            {
                column.PropertyInfo.SetValue(entity, Convert.ToUInt32(seqVal), null);
            }
            if (column.PropertyInfo.PropertyType == typeof(UInt64))
            {
                column.PropertyInfo.SetValue(entity, Convert.ToUInt64(seqVal), null);
            }
        }

        public dynamic Insert<T>(IDbConnection connection, T entity, IDbTransaction transaction, int? commandTimeout)
            where T : class
        {
            var classMap = SqlGenerator.Configuration.GetMap<T>();
            var nonIdentityKeyProperties =
                classMap.Properties.Where(p => p.KeyType == KeyType.Guid || p.KeyType == KeyType.Assigned).ToList();
            var identityColumn = classMap.Properties.SingleOrDefault(p => p.KeyType == KeyType.Identity || p.KeyType == KeyType.Sequence);
            foreach (var column in nonIdentityKeyProperties)
            {
                if (column.KeyType == KeyType.Guid)
                {
                    var comb = SqlGenerator.Configuration.GetNextGuid();
                    column.PropertyInfo.SetValue(entity, comb, null);
                }
            }

            IDictionary<string, object> keyValues = new ExpandoObject();
            var sql = SqlGenerator.Insert(classMap);
            if (identityColumn != null)
            {
                IEnumerable<long> result;
                if (SqlGenerator.SupportsMultipleStatements())
                {
                    sql += SqlGenerator.Configuration.Dialect.BatchSeperator + SqlGenerator.IdentitySql(classMap);
                    if (identityColumn.KeyType == KeyType.Sequence)
                    {
                        SetValueWithSequence(connection, identityColumn, entity);
                    }

                    result = connection.Query<long>(sql, entity, transaction, false, commandTimeout, CommandType.Text);
                }
                else
                {
                    if (identityColumn.KeyType == KeyType.Sequence)
                    {
                        SetValueWithSequence(connection, identityColumn, entity);
                    }
                    connection.Execute(sql, entity, transaction, commandTimeout, CommandType.Text);
                    sql = SqlGenerator.IdentitySql(classMap);
                    result = connection.Query<long>(sql, entity, transaction, false, commandTimeout, CommandType.Text);
                }

                var identityValue = result.First();
                var identityInt = Convert.ToInt32(identityValue);
                keyValues.Add(identityColumn.Name, identityInt);
                identityColumn.PropertyInfo.SetValue(entity, identityInt, null);
            }
            else
            {
                connection.Execute(sql, entity, transaction, commandTimeout, CommandType.Text);
            }

            foreach (var column in nonIdentityKeyProperties)
            {
                keyValues.Add(column.Name, column.PropertyInfo.GetValue(entity, null));
            }

            if (keyValues.Count == 1)
            {
                return keyValues.First().Value;
            }

            return keyValues;
        }

        public bool Update<T>(IDbConnection connection, T entity, IDbTransaction transaction, int? commandTimeout)
            where T : class
        {
            var classMap = SqlGenerator.Configuration.GetMap<T>();
            var predicate = GetKeyPredicate(classMap, entity);
            var parameters = new Dictionary<string, object>();
            var sql = SqlGenerator.Update(classMap, predicate, parameters);
            var dynamicParameters = new DynamicParameters();

            var columns =
                classMap.Properties.Where(p => !(p.Ignored || p.IsReadOnly || p.KeyType == KeyType.Identity));
            foreach (
                var property in
                    ReflectionHelper.GetObjectValues(entity).Where(property => columns.Any(c => c.Name == property.Key))
                )
            {
                dynamicParameters.Add(property.Key, property.Value);
            }

            foreach (var parameter in parameters)
            {
                dynamicParameters.Add(parameter.Key, parameter.Value);
            }

            return connection.Execute(sql, dynamicParameters, transaction, commandTimeout, CommandType.Text) > 0;
        }

        public bool Delete<T>(IDbConnection connection, T entity, IDbTransaction transaction, int? commandTimeout)
            where T : class
        {
            var classMap = SqlGenerator.Configuration.GetMap<T>();
            var predicate = GetKeyPredicate(classMap, entity);
            return Delete<T>(connection, classMap, predicate, transaction, commandTimeout);
        }

        public bool Delete<T>(IDbConnection connection, object predicate, IDbTransaction transaction,
            int? commandTimeout) where T : class
        {
            var classMap = SqlGenerator.Configuration.GetMap<T>();
            var wherePredicate = GetPredicate(classMap, predicate);
            return Delete<T>(connection, classMap, wherePredicate, transaction, commandTimeout);
        }

        public IEnumerable<T> GetList<T>(IDbConnection connection, object predicate, IList<ISort> sort,
            IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class
        {
            var classMap = SqlGenerator.Configuration.GetMap<T>();
            var wherePredicate = GetPredicate(classMap, predicate);
            return GetList<T>(connection, classMap, wherePredicate, sort, transaction, commandTimeout, true);
        }

        public IEnumerable<T> GetPage<T>(IDbConnection connection, IPredicateGroup predicate, IList<ISort> sort, int page,
            int resultsPerPage, IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class
        {
            var classMap = SqlGenerator.Configuration.GetMap<T>();
            return GetPage<T>(connection, classMap, predicate, sort, page, resultsPerPage, transaction,
                commandTimeout, buffered);
        }

        public IEnumerable<T> GetPage<T>(IDbConnection connection, object predicate, IList<ISort> sort, int page,
            int resultsPerPage, IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class
        {
            var classMap = SqlGenerator.Configuration.GetMap<T>();
            var wherePredicate = GetPredicate(classMap, predicate);
            return GetPage<T>(connection, classMap, wherePredicate, sort, page, resultsPerPage, transaction,
                commandTimeout, buffered);
        }

        public IEnumerable<T> GetSet<T>(IDbConnection connection, object predicate, IList<ISort> sort, int firstResult,
            int maxResults, IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class
        {
            var classMap = SqlGenerator.Configuration.GetMap<T>();
            var wherePredicate = GetPredicate(classMap, predicate);
            return GetSet<T>(connection, classMap, wherePredicate, sort, firstResult, maxResults, transaction,
                commandTimeout, buffered);
        }

        public int Count<T>(IDbConnection connection, object predicate, IDbTransaction transaction, int? commandTimeout)
            where T : class
        {
            var classMap = SqlGenerator.Configuration.GetMap<T>();
            var wherePredicate = GetPredicate(classMap, predicate);
            var parameters = new Dictionary<string, object>();
            var sql = SqlGenerator.Count(classMap, wherePredicate, parameters);
            var dynamicParameters = new DynamicParameters();
            foreach (var parameter in parameters)
            {
                dynamicParameters.Add(parameter.Key, parameter.Value);
            }

            return
                (int)
                    connection.Query(sql, dynamicParameters, transaction, false, commandTimeout, CommandType.Text)
                        .Single()
                        .Total;
        }

        public IMultipleResultReader GetMultiple(IDbConnection connection, GetMultiplePredicate predicate,
            IDbTransaction transaction, int? commandTimeout)
        {
            if (SqlGenerator.SupportsMultipleStatements())
            {
                return GetMultipleByBatch(connection, predicate, transaction, commandTimeout);
            }

            return GetMultipleBySequence(connection, predicate, transaction, commandTimeout);
        }

        /// <summary>
        ///     转换 DapperRow 到 EntityType
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dapperRow"></param>
        /// <returns></returns>
        public T Transform<T>(dynamic dapperRow) where T : class
        {
            var map = SqlGenerator.Configuration.GetMap<T>();

            var entityType = typeof(T);
            var item = Activator.CreateInstance<T>();
            foreach (var propertyMap in map.Properties)
            {
                if (propertyMap.Ignored) continue;

                var property = entityType.GetProperty(propertyMap.Name);
                property.SetValue(item, ((IDictionary<string, object>)dapperRow)[propertyMap.ColumnName], null);
            }
            return item;
        }

        protected IEnumerable<T> GetList<T>(IDbConnection connection, IClassMapper classMap, IPredicate predicate,
            IList<ISort> sort, IDbTransaction transaction, int? commandTimeout, bool buffered) where T : class
        {
            var parameters = new Dictionary<string, object>();
            var sql = SqlGenerator.Select(classMap, predicate, sort, parameters);
            var dynamicParameters = new DynamicParameters();
            foreach (var parameter in parameters)
            {
                dynamicParameters.Add(parameter.Key, parameter.Value);
            }

            return connection.Query<T>(sql, dynamicParameters, transaction, buffered, commandTimeout, CommandType.Text);
        }

        protected IEnumerable<T> GetPage<T>(IDbConnection connection, IClassMapper classMap, IPredicateGroup predicate,
            IList<ISort> sort, int page, int resultsPerPage, IDbTransaction transaction, int? commandTimeout,
            bool buffered) where T : class
        {
            var parameters = new Dictionary<string, object>();
            var sql = SqlGenerator.SelectPaged(classMap, predicate, sort, page, resultsPerPage, parameters);
            var dynamicParameters = new DynamicParameters();
            foreach (var parameter in parameters)
            {
                dynamicParameters.Add(parameter.Key, parameter.Value);
            }

            return connection.Query<T>(sql, dynamicParameters, transaction, buffered, commandTimeout, CommandType.Text);
        }

        protected IEnumerable<T> GetPage<T>(IDbConnection connection, IClassMapper classMap, IPredicate predicate,
            IList<ISort> sort, int page, int resultsPerPage, IDbTransaction transaction, int? commandTimeout,
            bool buffered) where T : class
        {
            var parameters = new Dictionary<string, object>();
            var sql = SqlGenerator.SelectPaged(classMap, predicate, sort, page, resultsPerPage, parameters);
            var dynamicParameters = new DynamicParameters();
            foreach (var parameter in parameters)
            {
                dynamicParameters.Add(parameter.Key, parameter.Value);
            }

            return connection.Query<T>(sql, dynamicParameters, transaction, buffered, commandTimeout, CommandType.Text);
        }

        protected IEnumerable<T> GetSet<T>(IDbConnection connection, IClassMapper classMap, IPredicate predicate,
            IList<ISort> sort, int firstResult, int maxResults, IDbTransaction transaction, int? commandTimeout,
            bool buffered) where T : class
        {
            var parameters = new Dictionary<string, object>();
            var sql = SqlGenerator.SelectSet(classMap, predicate, sort, firstResult, maxResults, parameters);
            var dynamicParameters = new DynamicParameters();
            foreach (var parameter in parameters)
            {
                dynamicParameters.Add(parameter.Key, parameter.Value);
            }

            return connection.Query<T>(sql, dynamicParameters, transaction, buffered, commandTimeout, CommandType.Text);
        }

        protected bool Delete<T>(IDbConnection connection, IClassMapper classMap, IPredicate predicate,
            IDbTransaction transaction, int? commandTimeout) where T : class
        {
            var parameters = new Dictionary<string, object>();
            var sql = SqlGenerator.Delete(classMap, predicate, parameters);
            var dynamicParameters = new DynamicParameters();
            foreach (var parameter in parameters)
            {
                dynamicParameters.Add(parameter.Key, parameter.Value);
            }

            return connection.Execute(sql, dynamicParameters, transaction, commandTimeout, CommandType.Text) > 0;
        }

        protected IPredicate GetPredicate(IClassMapper classMap, object predicate)
        {
            var wherePredicate = predicate as IPredicate;
            if (wherePredicate == null && predicate != null)
            {
                wherePredicate = GetEntityPredicate(classMap, predicate);
            }

            return wherePredicate;
        }

        /// <summary>
        /// 获取 ID 谓语
        /// </summary>
        /// <param name="classMap"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        protected IPredicate GetIdPredicate(IClassMapper classMap, object id)
        {
            var isSimpleType = ReflectionHelper.IsSimpleType(id.GetType());
            var keys = classMap.Properties.Where(p => p.KeyType != KeyType.NotAKey);
            IDictionary<string, object> paramValues = null;
            IList<IPredicate> predicates = new List<IPredicate>();
            if (!isSimpleType)
            {
                paramValues = ReflectionHelper.GetObjectValues(id);
            }

            foreach (var key in keys)
            {
                var value = id;
                if (!isSimpleType)
                {
                    value = paramValues[key.Name];
                }

                var predicateType = typeof(FieldPredicate<>).MakeGenericType(classMap.EntityType);

                var fieldPredicate = Activator.CreateInstance(predicateType) as IFieldPredicate;
                fieldPredicate.Not = false;
                fieldPredicate.Operator = Operator.Eq;
                fieldPredicate.PropertyName = key.Name;
                fieldPredicate.Value = value;
                predicates.Add(fieldPredicate);
            }

            return predicates.Count == 1
                ? predicates[0]
                : new PredicateGroup
                {
                    Operator = GroupOperator.And,
                    Predicates = predicates
                };
        }

        protected IPredicate GetKeyPredicate<T>(IClassMapper classMap, T entity) where T : class
        {
            var whereFields = classMap.Properties.Where(p => p.KeyType != KeyType.NotAKey);
            var enumerable = whereFields as IPropertyMap[] ?? whereFields.ToArray();
            if (!enumerable.Any())
            {
                throw new ArgumentException("必须定义至少一个 Key 。");
            }

            IList<IPredicate> predicates = (enumerable.Select(field => new FieldPredicate<T>
            {
                Not = false,
                Operator = Operator.Eq,
                PropertyName = field.Name,
                Value = field.PropertyInfo.GetValue(entity, null)
            })).Cast<IPredicate>().ToList();

            return predicates.Count == 1
                ? predicates[0]
                : new PredicateGroup
                {
                    Operator = GroupOperator.And,
                    Predicates = predicates
                };
        }

        protected IPredicate GetEntityPredicate(IClassMapper classMap, object entity)
        {
            var predicateType = typeof(FieldPredicate<>).MakeGenericType(classMap.EntityType);
            IList<IPredicate> predicates = new List<IPredicate>();
            foreach (var kvp in ReflectionHelper.GetObjectValues(entity))
            {
                var fieldPredicate = Activator.CreateInstance(predicateType) as IFieldPredicate;
                if (fieldPredicate == null) continue;
                fieldPredicate.Not = false;
                fieldPredicate.Operator = Operator.Eq;
                fieldPredicate.PropertyName = kvp.Key;
                fieldPredicate.Value = kvp.Value;
                predicates.Add(fieldPredicate);
            }

            //if (!predicates.Any())
            //{
            //    predicates.Add(GetIdPredicate(classMap, entity));
            //}

            return predicates.Count == 1
                ? predicates[0]
                : new PredicateGroup
                {
                    Operator = GroupOperator.And,
                    Predicates = predicates
                };
        }

        protected GridReaderResultReader GetMultipleByBatch(IDbConnection connection, GetMultiplePredicate predicate,
            IDbTransaction transaction, int? commandTimeout)
        {
            var parameters = new Dictionary<string, object>();
            var sql = new StringBuilder();
            foreach (var item in predicate.Items)
            {
                var classMap = SqlGenerator.Configuration.GetMap(item.Type);
                var itemPredicate = item.Value as IPredicate;
                if (itemPredicate == null && item.Value != null)
                {
                    itemPredicate = GetPredicate(classMap, item.Value);
                }

                sql.AppendLine(SqlGenerator.Select(classMap, itemPredicate, item.Sort, parameters) +
                               SqlGenerator.Configuration.Dialect.BatchSeperator);
            }

            var dynamicParameters = new DynamicParameters();
            foreach (var parameter in parameters)
            {
                dynamicParameters.Add(parameter.Key, parameter.Value);
            }

            var grid = connection.QueryMultiple(sql.ToString(), dynamicParameters, transaction,
                commandTimeout, CommandType.Text);
            return new GridReaderResultReader(grid);
        }

        protected SequenceReaderResultReader GetMultipleBySequence(IDbConnection connection,
            GetMultiplePredicate predicate, IDbTransaction transaction, int? commandTimeout)
        {
            IList<SqlMapper.GridReader> items = new List<SqlMapper.GridReader>();
            foreach (var item in predicate.Items)
            {
                var parameters = new Dictionary<string, object>();
                var classMap = SqlGenerator.Configuration.GetMap(item.Type);
                var itemPredicate = item.Value as IPredicate;
                if (itemPredicate == null && item.Value != null)
                {
                    itemPredicate = GetPredicate(classMap, item.Value);
                }

                var sql = SqlGenerator.Select(classMap, itemPredicate, item.Sort, parameters);
                var dynamicParameters = new DynamicParameters();
                foreach (var parameter in parameters)
                {
                    dynamicParameters.Add(parameter.Key, parameter.Value);
                }

                var queryResult = connection.QueryMultiple(sql, dynamicParameters, transaction,
                    commandTimeout, CommandType.Text);
                items.Add(queryResult);
            }

            return new SequenceReaderResultReader(items);
        }
    }
}