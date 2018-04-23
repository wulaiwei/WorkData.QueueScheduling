#region 导入名称空间

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;

#endregion 导入名称空间

namespace Dapper
{
    internal sealed class ClassMapper : IClassMapper
    {
        public ClassMapper(Type type)
        {
            PropertyTypeKeyTypeMapping = new Dictionary<Type, KeyType>
            {
                {typeof (byte), KeyType.Identity},
                {typeof (byte?), KeyType.Identity},
                {typeof (sbyte), KeyType.Identity},
                {typeof (sbyte?), KeyType.Identity},
                {typeof (short), KeyType.Identity},
                {typeof (short?), KeyType.Identity},
                {typeof (ushort), KeyType.Identity},
                {typeof (ushort?), KeyType.Identity},
                {typeof (int), KeyType.Identity},
                {typeof (int?), KeyType.Identity},
                {typeof (uint), KeyType.Identity},
                {typeof (uint?), KeyType.Identity},
                {typeof (long), KeyType.Identity},
                {typeof (long?), KeyType.Identity},
                {typeof (ulong), KeyType.Identity},
                {typeof (ulong?), KeyType.Identity},
                {typeof (BigInteger), KeyType.Identity},
                {typeof (BigInteger?), KeyType.Identity},
                {typeof (Guid), KeyType.Guid},
                {typeof (Guid?), KeyType.Guid},
            };
            EntityType = type;
            Properties = new List<IPropertyMap>();
            Table(type.Name);
        }

        protected Dictionary<Type, KeyType> PropertyTypeKeyTypeMapping { get; private set; }

        /// <summary>
        ///     架构名称
        /// </summary>
        public string SchemaName { get; private set; }

        /// <summary>
        ///     表名
        /// </summary>
        public string TableName { get; private set; }

        /// <summary>
        ///     要映射到列的属性集合
        /// </summary>
        public IList<IPropertyMap> Properties { get; private set; }

        public Type EntityType { get; private set; }

        public void Schema(string schemaName)
        {
            SchemaName = schemaName;
        }

        public void Table(string tableName)
        {
            TableName = tableName;
        }

        public IClassMapper GetMap()
        {
            var DapperMaps = EntityType.GetCustomAttributes(typeof(TableAttribute), true);
            var tableMap = (TableAttribute)DapperMaps.SingleOrDefault();
            if (tableMap == null)
            {
                return null;
            }

            Table(tableMap.TableName);
            Schema(tableMap.SchemaName);
            const BindingFlags DefaultBindingFlags = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic;

            var props = EntityType.GetProperties(DefaultBindingFlags);
            foreach (var prop in props)
            {
                foreach (PropertyAttribute attribute in prop.GetCustomAttributes(typeof(PropertyAttribute), false))
                {
                    var propModel = new PropertyMap(prop);

                    propModel.Column(attribute.ColumnName)
                        .Key(attribute.KeyType)
                        .Sequence(attribute.SequenceName);

                    if (attribute.Ignored)
                    {
                        propModel.Ignore();
                    }
                    if (attribute.IsReadOnly)
                    {
                        propModel.ReadOnly();
                    }

                    if (Properties.Any(p => p.Name.Equals(propModel.Name)))
                    {
                        throw new ArgumentException(string.Format("检测到属性 {0} 重复映射。", propModel.Name));
                    }

                    Properties.Add(propModel);
                }
            }

            return this;
        }
    }
}