#region 导入名称空间

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion 导入名称空间

namespace Dapper
{
    /// <summary>
    ///     Sql生成器
    /// </summary>
    public interface ISqlGenerator
    {
        /// <summary>
        ///     Dapper扩展配置
        /// </summary>
        IDapperConfiguration Configuration { get; }

        /// <summary>
        ///     生成查询SQL
        /// </summary>
        /// <param name="classMap">数据库映射实体</param>
        /// <param name="predicate">谓词（条件）</param>
        /// <param name="sort">排序</param>
        /// <param name="parameters">查询参数</param>
        /// <returns></returns>
        string Select(IClassMapper classMap, IPredicate predicate, IList<ISort> sort,
            IDictionary<string, object> parameters);

        /// <summary>
        ///     生成分页查询SQL
        /// </summary>
        /// <param name="classMap"></param>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="resultsPerPage"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        string SelectPaged(IClassMapper classMap, IPredicateGroup predicate, IList<ISort> sort, int page, int resultsPerPage,
            IDictionary<string, object> parameters);

        /// <summary>
        ///     生成分页查询SQL
        /// </summary>
        /// <param name="classMap"></param>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="resultsPerPage"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        string SelectPaged(IClassMapper classMap, IPredicate predicate, IList<ISort> sort, int page, int resultsPerPage,
            IDictionary<string, object> parameters);

        /// <summary>
        ///     生成集合查询SQL
        /// </summary>
        /// <param name="classMap"></param>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="firstResult"></param>
        /// <param name="maxResults"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        string SelectSet(IClassMapper classMap, IPredicate predicate, IList<ISort> sort, int firstResult, int maxResults,
            IDictionary<string, object> parameters);

        /// <summary>
        ///     生成记录条数SQL
        /// </summary>
        /// <param name="classMap"></param>
        /// <param name="predicate"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        string Count(IClassMapper classMap, IPredicate predicate, IDictionary<string, object> parameters);

        /// <summary>
        ///     生成插入SQL
        /// </summary>
        /// <param name="classMap"></param>
        /// <returns></returns>
        string Insert(IClassMapper classMap);

        /// <summary>
        ///     生成更新SQL
        /// </summary>
        /// <param name="classMap"></param>
        /// <param name="predicate"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        string Update(IClassMapper classMap, IPredicate predicate, IDictionary<string, object> parameters);

        /// <summary>
        ///     生成删除SQL
        /// </summary>
        /// <param name="classMap"></param>
        /// <param name="predicate"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        string Delete(IClassMapper classMap, IPredicate predicate, IDictionary<string, object> parameters);

        string IdentitySql(IClassMapper classMap);

        string GetTableName(IClassMapper map);

        string GetColumnName(IClassMapper map, IPropertyMap property, bool includeAlias);

        string GetColumnName(IClassMapper map, string propertyName, bool includeAlias);

        bool SupportsMultipleStatements();
    }

    internal class SqlGeneratorImpl : ISqlGenerator
    {
        public SqlGeneratorImpl(IDapperConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IDapperConfiguration Configuration { get; private set; }

        private void OrderBy(StringBuilder sql, IClassMapper classMap, IList<ISort> sort)
        {
            if (sort != null && sort.Any())
            {
                var orderBy = sort.Select(s => GetColumnName(classMap, s.PropertyName, false) + (s.Ascending ? " ASC" : " DESC")).AppendStrings();
                sql.Append(" ORDER BY " + orderBy);
            }
        }

        public virtual string Select(IClassMapper classMap, IPredicate predicate, IList<ISort> sort,
            IDictionary<string, object> parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException("parameters", "查询参数不能为NULL");
            }

            var sql = new StringBuilder(string.Format("SELECT {0} FROM {1}",
                BuildSelectColumns(classMap),
                GetTableName(classMap)));
            if (predicate != null)
            {
                sql.Append(" WHERE ").Append(predicate.GetSql(this, parameters));
            }

            OrderBy(sql, classMap, sort);
            //
            //            if (sort != null && sort.Any())
            //            {
            //                sql.Append(" ORDER BY ").Append(sort.Select(s => GetColumnName(classMap, s.PropertyName, false) + (s.Ascending ? " ASC" : " DESC")).AppendStrings());
            //            }

            return sql.ToString();
        }

        public virtual string SelectPaged(IClassMapper classMap, IPredicate predicate, IList<ISort> sort, int page,
            int resultsPerPage, IDictionary<string, object> parameters)
        {
            //            if (sort == null || !sort.Any())
            //            {
            //                throw new ArgumentNullException("sort", "排序不能为NULL或空.");
            //            }

            if (parameters == null)
            {
                throw new ArgumentNullException("parameters", "查询参数不能为NULL");
            }

            var innerSql = new StringBuilder(string.Format("SELECT {0} FROM {1}",
                BuildSelectColumns(classMap),
                GetTableName(classMap)));

            if (predicate != null)
            {
                innerSql.Append(" WHERE ")
                    .Append(predicate.GetSql(this, parameters));
            }

            //            if (sort!=null&&sort.Any())
            //            {
            //                var orderBy = sort.Select(s => GetColumnName(classMap, s.PropertyName, false) + (s.Ascending ? " ASC" : " DESC")).AppendStrings();
            //                innerSql.Append(" ORDER BY " + orderBy);
            //            }
            OrderBy(innerSql, classMap, sort);

            var sql = Configuration.Dialect.GetPagingSql(innerSql.ToString(), page, resultsPerPage, parameters);
            return sql;
        }

        public virtual string SelectPaged(IClassMapper classMap, IPredicateGroup predicateGroup, IList<ISort> sort, int page,
            int resultsPerPage, IDictionary<string, object> parameters)
        {
            //            if (sort == null || !sort.Any())
            //            {
            //                throw new ArgumentNullException("sort", "排序不能为NULL或空.");
            //            }

            if (parameters == null)
            {
                throw new ArgumentNullException("parameters", "查询参数不能为NULL");
            }

            var innerSql = new StringBuilder(string.Format("SELECT {0} FROM {1}",
                BuildSelectColumns(classMap),
                GetTableName(classMap)));
            innerSql.Append(" WHERE  1=1");
            if (predicateGroup != null)
            {
                foreach (var predicate in predicateGroup.Predicates)
                {
                    innerSql.Append(" " + GroupOperator.And + " ")
                        .Append(predicate.GetSql(this, parameters));
                }
            }

            OrderBy(innerSql, classMap, sort);

            //            if (sort != null && sort.Any())
            //            {
            //                var orderBy = sort.Select(s => GetColumnName(classMap, s.PropertyName, false) + (s.Ascending ? " ASC" : " DESC")).AppendStrings();
            //                innerSql.Append(" ORDER BY " + orderBy);
            //            }

            var sql = Configuration.Dialect.GetPagingSql(innerSql.ToString(), page, resultsPerPage, parameters);
            return sql;
        }

        public virtual string SelectSet(IClassMapper classMap, IPredicate predicate, IList<ISort> sort, int firstResult,
            int maxResults, IDictionary<string, object> parameters)
        {
            //            if (sort == null || !sort.Any())
            //            {
            //                throw new ArgumentNullException("sort", "排序不能为NULL或空.");
            //            }

            if (parameters == null)
            {
                throw new ArgumentNullException("parameters", "查询参数不能为NULL");
            }

            var innerSql = new StringBuilder(string.Format("SELECT {0} FROM {1}",
                BuildSelectColumns(classMap),
                GetTableName(classMap)));
            if (predicate != null)
            {
                innerSql.Append(" WHERE ").Append(predicate.GetSql(this, parameters));
            }

            //            var orderBy =
            //                sort.Select(s => GetColumnName(classMap, s.PropertyName, false) + (s.Ascending ? " ASC" : " DESC"))
            //                    .AppendStrings();
            //            innerSql.Append(" ORDER BY " + orderBy);

            OrderBy(innerSql, classMap, sort);

            var sql = Configuration.Dialect.GetSetSql(innerSql.ToString(), firstResult, maxResults, parameters);
            return sql;
        }

        public virtual string Count(IClassMapper classMap, IPredicate predicate, IDictionary<string, object> parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException("parameters", "查询参数不能为NULL");
            }

            var sql = new StringBuilder(string.Format("SELECT COUNT(*) AS {0}Total{1} FROM {2}",
                Configuration.Dialect.OpenQuote,
                Configuration.Dialect.CloseQuote,
                GetTableName(classMap)));
            if (predicate != null)
            {
                sql.Append(" WHERE ")
                    .Append(predicate.GetSql(this, parameters));
            }

            return sql.ToString();
        }

        public virtual string Insert(IClassMapper classMap)
        {
            var columns = classMap.Properties.Where(p => !(p.Ignored || p.IsReadOnly || p.KeyType == KeyType.Identity));
            var propertyMaps = columns as IPropertyMap[] ?? columns.ToArray();
            if (!propertyMaps.Any())
            {
                throw new ArgumentException("没有映射任何列");
            }

            var columnNames = propertyMaps.Select(p => GetColumnName(classMap, p, false));
            var parameters = propertyMaps.Select(p => Configuration.Dialect.ParameterPrefix + p.Name);

            var sql = string.Format("INSERT INTO {0} ({1}) VALUES ({2})",
                GetTableName(classMap),
                columnNames.AppendStrings(),
                parameters.AppendStrings());

            return sql;
        }

        public virtual string Update(IClassMapper classMap, IPredicate predicate, IDictionary<string, object> parameters)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate", "谓语不能为NULL");
            }

            if (parameters == null)
            {
                throw new ArgumentNullException("parameters", "查询参数不能为NULL");
            }

            var columns =
                classMap.Properties.Where(p => !(p.Ignored || p.IsReadOnly || p.KeyType == KeyType.Identity));
            if (!columns.Any())
            {
                throw new ArgumentException("没有映射任何列");
            }

            var setSql =
                columns.Select(
                    p =>
                        string.Format(
                            "{0} = {1}{2}", GetColumnName(classMap, p, false), Configuration.Dialect.ParameterPrefix,
                            p.Name));

            return string.Format("UPDATE {0} SET {1} WHERE {2}",
                GetTableName(classMap),
                setSql.AppendStrings(),
                predicate.GetSql(this, parameters));
        }

        public virtual string Delete(IClassMapper classMap, IPredicate predicate, IDictionary<string, object> parameters)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate", "谓语不能为NULL");
            }

            if (parameters == null)
            {
                throw new ArgumentNullException("parameters", "查询参数不能为NULL");
            }

            var sql = new StringBuilder(string.Format("DELETE FROM {0}", GetTableName(classMap)));
            sql.Append(" WHERE ").Append(predicate.GetSql(this, parameters));
            return sql.ToString();
        }

        public virtual string IdentitySql(IClassMapper classMap)
        {
            if (classMap.Properties.Any(x => x.KeyType == KeyType.Sequence))
            {
                var propertyMap = classMap.Properties.SingleOrDefault(x => x.KeyType == KeyType.Sequence);
                return Configuration.Dialect.GetCurrentSequence(propertyMap.SequenceName);
            }
            else
            {
                return Configuration.Dialect.GetIdentitySql(GetTableName(classMap));
            }
        }

        public virtual string GetTableName(IClassMapper map)
        {
            return Configuration.Dialect.GetTableName(map.SchemaName, map.TableName, null);
        }

        public virtual string GetColumnName(IClassMapper map, IPropertyMap property, bool includeAlias)
        {
            string alias = null;
            if (property.ColumnName != property.Name && includeAlias)
            {
                alias = property.Name;
            }

            return Configuration.Dialect.GetColumnName(GetTableName(map), property.ColumnName, alias);
        }

        public virtual string GetColumnName(IClassMapper map, string propertyName, bool includeAlias)
        {
            var propertyMap =
                map.Properties.SingleOrDefault(
                    p => p.Name.Equals(propertyName, StringComparison.InvariantCultureIgnoreCase));
            if (propertyMap == null)
            {
                throw new ArgumentException(string.Format("在映射文件中不能找到 '{0}' 。", propertyName));
            }

            return GetColumnName(map, propertyMap, includeAlias);
        }

        public virtual bool SupportsMultipleStatements()
        {
            return Configuration.Dialect.SupportsMultipleStatements;
        }

        public virtual string BuildSelectColumns(IClassMapper classMap)
        {
            var columns = classMap.Properties
                .Where(p => !p.Ignored)
                .Select(p => GetColumnName(classMap, p, true));
            return columns.AppendStrings();
        }
    }
}