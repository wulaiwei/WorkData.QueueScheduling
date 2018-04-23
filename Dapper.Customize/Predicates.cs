#region 导入名称空间

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

#endregion 导入名称空间

namespace Dapper
{
    /// <summary>
    ///     SQL谓语
    /// </summary>
    public static class Predicates
    {
        /// <summary>
        ///     工厂方法创建一个新的  IFieldPredicate 谓语: [FieldName] [Operator] [Value].
        ///     谓语转换SQL示例: WHERE FirstName = 'Foo'
        /// </summary>
        /// <typeparam name="T">实例类型</typeparam>
        /// <param name="expression">返回左操作数的表达式  [FieldName].</param>
        /// <param name="op">比较运算符</param>
        /// <param name="value">谓语的值.</param>
        /// <param name="not">反转比较运算符 . 谓语转换SQL示例: WHERE FirstName ！= 'Foo'.</param>
        /// <returns>An instance of IFieldPredicate.</returns>
        public static IFieldPredicate Field<T>(Expression<Func<T, object>> expression, Operator op, object value,
            bool not = false) where T : class
        {
            var propertyInfo = ReflectionHelper.GetProperty(expression) as PropertyInfo;
            return new FieldPredicate<T>
            {
                PropertyName = propertyInfo.Name,
                Operator = op,
                Value = value,
                Not = not
            };
        }

        /// <summary>
        ///     工厂方法创建一个新的 IPropertyPredicate 谓语: [FieldName1] [Operator] [FieldName2]
        ///     谓语转换SQL示例: WHERE FirstName = LastName
        /// </summary>
        /// <typeparam name="T">左操作数实例类型.</typeparam>
        /// <typeparam name="T2">右操作数实例类型.</typeparam>
        /// <param name="expression">返回左操作数的表达式 [FieldName1].</param>
        /// <param name="op">比较运算符.</param>
        /// <param name="expression2">返回右操作数的表达式 [FieldName2].</param>
        /// <param name="not">反转比较运算符. 示例: WHERE FirstName ！= LastName </param>
        /// <returns>An instance of IPropertyPredicate.</returns>
        public static IPropertyPredicate Property<T, T2>(Expression<Func<T, object>> expression, Operator op,
            Expression<Func<T2, object>> expression2, bool not = false)
            where T : class
            where T2 : class
        {
            var propertyInfo = ReflectionHelper.GetProperty(expression) as PropertyInfo;
            var propertyInfo2 = ReflectionHelper.GetProperty(expression2) as PropertyInfo;
            return new PropertyPredicate<T, T2>
            {
                PropertyName = propertyInfo.Name,
                PropertyName2 = propertyInfo2.Name,
                Operator = op,
                Not = not
            };
        }

        /// <summary>
        ///     工厂方法创建一个新的 IPredicateGroup 谓语.
        ///     谓词组与其他谓词可以连接在一起.
        /// </summary>
        /// <param name="op">分组操作时使用的连接谓词 (AND / OR).</param>
        /// <param name="predicate">一组谓词列表.</param>
        /// <returns>An instance of IPredicateGroup.</returns>
        public static IPredicateGroup Group(GroupOperator op, params IPredicate[] predicate)
        {
            return new PredicateGroup
            {
                Operator = op,
                Predicates = predicate
            };
        }

        /// <summary>
        ///     工厂方法创建一个新的 IExistsPredicate 谓语.
        /// </summary>
        public static IExistsPredicate Exists<TSub>(IPredicate predicate, bool not = false)
            where TSub : class
        {
            return new ExistsPredicate<TSub>
            {
                Not = not,
                Predicate = predicate
            };
        }

        /// <summary>
        ///     工厂方法创建一个新的 IBetweenPredicate 谓语.
        /// </summary>
        public static IBetweenPredicate Between<T>(Expression<Func<T, object>> expression, BetweenValues values,
            bool not = false)
            where T : class
        {
            var propertyInfo = ReflectionHelper.GetProperty(expression) as PropertyInfo;
            return new BetweenPredicate<T>
            {
                Not = not,
                PropertyName = propertyInfo.Name,
                Value = values
            };
        }

        /// <summary>
        ///     工厂方法创建一个新的 sort 控制结果将如何排序。.
        /// </summary>
        public static ISort Sort<T>(Expression<Func<T, object>> expression, bool ascending = true)
        {
            var propertyInfo = ReflectionHelper.GetProperty(expression) as PropertyInfo;
            return new Sort
            {
                PropertyName = propertyInfo.Name,
                Ascending = ascending
            };
        }
    }

    /// <summary>
    ///     谓词接口
    /// </summary>
    public interface IPredicate
    {
        /// <summary>
        ///     生成SQL
        /// </summary>
        /// <param name="sqlGenerator"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        string GetSql(ISqlGenerator sqlGenerator, IDictionary<string, object> parameters);
    }

    /// <summary>
    ///     基础谓词接口
    /// </summary>
    public interface IBasePredicate : IPredicate
    {
        /// <summary>
        ///     属性名称
        /// </summary>
        string PropertyName { get; set; }
    }

    public abstract class BasePredicate : IBasePredicate
    {
        public abstract string GetSql(ISqlGenerator sqlGenerator, IDictionary<string, object> parameters);

        public string PropertyName { get; set; }

        protected virtual string GetColumnName(Type entityType, ISqlGenerator sqlGenerator, string propertyName)
        {
            var map = sqlGenerator.Configuration.GetMap(entityType);
            if (map == null)
            {
                throw new NullReferenceException(string.Format("Map was not found for {0}", entityType));
            }

            var propertyMap = map.Properties.SingleOrDefault(p => p.Name == propertyName);
            if (propertyMap == null)
            {
                throw new NullReferenceException(string.Format("{0} was not found for {1}", propertyName, entityType));
            }

            return sqlGenerator.GetColumnName(map, propertyMap, false);
        }
    }

    /// <summary>
    ///     比较谓词
    /// </summary>
    public interface IComparePredicate : IBasePredicate
    {
        /// <summary>
        ///     操作符
        /// </summary>
        Operator Operator { get; set; }

        /// <summary>
        ///     操作符反转
        /// </summary>
        bool Not { get; set; }
    }

    public abstract class ComparePredicate : BasePredicate
    {
        public Operator Operator { get; set; }
        public bool Not { get; set; }

        public virtual string GetOperatorString()
        {
            switch (Operator)
            {
                case Operator.Gt:
                    return Not ? "<=" : ">";

                case Operator.Ge:
                    return Not ? "<" : ">=";

                case Operator.Lt:
                    return Not ? ">=" : "<";

                case Operator.Le:
                    return Not ? ">" : "<=";

                case Operator.Like:
                    return Not ? "NOT LIKE" : "LIKE";

                default:
                    return Not ? "<>" : "=";
            }
        }
    }

    /// <summary>
    ///     字段谓词
    /// </summary>
    public interface IFieldPredicate : IComparePredicate
    {
        /// <summary>
        ///     谓词的值
        /// </summary>
        object Value { get; set; }
    }

    public class FieldPredicate<T> : ComparePredicate, IFieldPredicate
        where T : class
    {
        public object Value { get; set; }

        public override string GetSql(ISqlGenerator sqlGenerator, IDictionary<string, object> parameters)
        {
            var columnName = GetColumnName(typeof(T), sqlGenerator, PropertyName);
            if (Value == null)
            {
                return string.Format("({0} IS {1}NULL)", columnName, Not ? "NOT " : string.Empty);
            }

            if (Value is IEnumerable && !(Value is string))
            {
                if (Operator != Operator.Eq)
                {
                    throw new ArgumentException("Operator must be set to Eq for Enumerable types");
                }

                var @params = new List<string>();
                foreach (var value in (IEnumerable)Value)
                {
                    var valueParameterName = parameters.SetParameterName(PropertyName, value,
                        sqlGenerator.Configuration.Dialect.ParameterPrefix);
                    @params.Add(valueParameterName);
                }

                var paramStrings = @params.Aggregate(new StringBuilder(),
                    (sb, s) => sb.Append((sb.Length != 0 ? ", " : string.Empty) + s), sb => sb.ToString());
                return string.Format("({0} {1}IN ({2}))", columnName, Not ? "NOT " : string.Empty, paramStrings);
            }

            var parameterName = parameters.SetParameterName(PropertyName, Value,
                sqlGenerator.Configuration.Dialect.ParameterPrefix);
            return string.Format("({0} {1} {2})", columnName, GetOperatorString(), parameterName);
        }
    }

    /// <summary>
    ///     属性谓词
    /// </summary>
    public interface IPropertyPredicate : IComparePredicate
    {
        /// <summary>
        ///     第二属性名称
        /// </summary>
        string PropertyName2 { get; set; }
    }

    public class PropertyPredicate<T, T2> : ComparePredicate, IPropertyPredicate
        where T : class
        where T2 : class
    {
        public string PropertyName2 { get; set; }

        public override string GetSql(ISqlGenerator sqlGenerator, IDictionary<string, object> parameters)
        {
            var columnName = GetColumnName(typeof(T), sqlGenerator, PropertyName);
            var columnName2 = GetColumnName(typeof(T2), sqlGenerator, PropertyName2);
            return string.Format("({0} {1} {2})", columnName, GetOperatorString(), columnName2);
        }
    }

    /// <summary>
    ///     Between 结构
    /// </summary>
    public struct BetweenValues
    {
        public object Value1 { get; set; }
        public object Value2 { get; set; }
    }

    /// <summary>
    ///     Between 谓词
    /// </summary>
    public interface IBetweenPredicate : IPredicate
    {
        /// <summary>
        ///     属性名称
        /// </summary>
        string PropertyName { get; set; }

        /// <summary>
        ///     值
        /// </summary>
        BetweenValues Value { get; set; }

        /// <summary>
        ///     反转操作符
        /// </summary>
        bool Not { get; set; }
    }

    public class BetweenPredicate<T> : BasePredicate, IBetweenPredicate
        where T : class
    {
        public override string GetSql(ISqlGenerator sqlGenerator, IDictionary<string, object> parameters)
        {
            var columnName = GetColumnName(typeof(T), sqlGenerator, PropertyName);
            var propertyName1 = parameters.SetParameterName(PropertyName, Value.Value1,
                sqlGenerator.Configuration.Dialect.ParameterPrefix);
            var propertyName2 = parameters.SetParameterName(PropertyName, Value.Value2,
                sqlGenerator.Configuration.Dialect.ParameterPrefix);
            return string.Format("({0} {1}BETWEEN {2} AND {3})", columnName, Not ? "NOT " : string.Empty, propertyName1,
                propertyName2);
        }

        public BetweenValues Value { get; set; }

        public bool Not { get; set; }
    }

    /// <summary>
    ///     比较操作符
    /// </summary>
    public enum Operator
    {
        /// <summary>
        ///     相等
        /// </summary>
        Eq,

        /// <summary>
        ///     大于
        /// </summary>
        Gt,

        /// <summary>
        ///     大于等于
        /// </summary>
        Ge,

        /// <summary>
        ///     小于
        /// </summary>
        Lt,

        /// <summary>
        ///     小于等于
        /// </summary>
        Le,

        /// <summary>
        ///     模糊查询 (You can use % in the value to do wilcard searching)
        /// </summary>
        Like
    }

    /// <summary>
    ///     分组查询谓词
    /// </summary>
    public interface IPredicateGroup : IPredicate
    {
        /// <summary>
        /// </summary>
        GroupOperator Operator { get; set; }

        IList<IPredicate> Predicates { get; set; }
    }

    /// <summary>
    ///     分组查询谓词
    /// </summary>
    public class PredicateGroup : IPredicateGroup
    {
        public GroupOperator Operator { get; set; }
        public IList<IPredicate> Predicates { get; set; }

        public string GetSql(ISqlGenerator sqlGenerator, IDictionary<string, object> parameters)
        {
            var seperator = Operator == GroupOperator.And ? " AND " : " OR ";
            return "(" + Predicates.Aggregate(new StringBuilder(),
                (sb, p) => (sb.Length == 0 ? sb : sb.Append(seperator)).Append(p.GetSql(sqlGenerator, parameters)),
                sb =>
                {
                    var s = sb.ToString();
                    if (s.Length == 0) return sqlGenerator.Configuration.Dialect.EmptyExpression;
                    return s;
                }
                ) + ")";
        }
    }

    public interface IExistsPredicate : IPredicate
    {
        IPredicate Predicate { get; set; }
        bool Not { get; set; }
    }

    public class ExistsPredicate<TSub> : IExistsPredicate
        where TSub : class
    {
        public IPredicate Predicate { get; set; }
        public bool Not { get; set; }

        public string GetSql(ISqlGenerator sqlGenerator, IDictionary<string, object> parameters)
        {
            var mapSub = GetClassMapper(typeof(TSub), sqlGenerator.Configuration);
            var sql = string.Format("({0}EXISTS (SELECT 1 FROM {1} WHERE {2}))",
                Not ? "NOT " : string.Empty,
                sqlGenerator.GetTableName(mapSub),
                Predicate.GetSql(sqlGenerator, parameters));
            return sql;
        }

        protected virtual IClassMapper GetClassMapper(Type type, IDapperConfiguration configuration)
        {
            var map = configuration.GetMap(type);
            if (map == null)
            {
                throw new NullReferenceException(string.Format("Map was not found for {0}", type));
            }

            return map;
        }
    }

    public interface ISort
    {
        string PropertyName { get; set; }
        bool Ascending { get; set; }
    }

    public class Sort : ISort
    {
        public string PropertyName { get; set; }
        public bool Ascending { get; set; }
    }

    /// <summary>
    ///     PredicateGroup 加入谓词时使用的操作符
    /// </summary>
    public enum GroupOperator
    {
        And,
        Or
    }
}