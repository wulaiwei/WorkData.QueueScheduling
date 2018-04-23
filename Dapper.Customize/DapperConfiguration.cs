#region 导入名称空间

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;

#endregion 导入名称空间

namespace Dapper
{
    public interface IDapperConfiguration
    {
        IList<Assembly> MappingAssemblies { get; }
        ISqlDialect Dialect { get; }

        IClassMapper GetMap(Type entityType);

        IClassMapper GetMap<T>() where T : class;

        void ClearCache();

        Guid GetNextGuid();
    }

    internal class DapperConfiguration : IDapperConfiguration
    {
        private readonly ConcurrentDictionary<Type, IClassMapper> _classMaps =
            new ConcurrentDictionary<Type, IClassMapper>();

        public DapperConfiguration(IList<Assembly> mappingAssemblies,
            ISqlDialect sqlDialect)
        {
            MappingAssemblies = mappingAssemblies ?? new List<Assembly>();
            Dialect = sqlDialect;
        }

        public IList<Assembly> MappingAssemblies { get; private set; }
        public ISqlDialect Dialect { get; private set; }

        public IClassMapper GetMap(Type entityType)
        {
            IClassMapper map;
            if (!_classMaps.TryGetValue(entityType, out map))
            {
                map = new ClassMapper(entityType).GetMap();
                if (map != null)
                {
                    _classMaps[entityType] = map;
                }
            }

            return map;
        }

        public IClassMapper GetMap<T>() where T : class
        {
            return GetMap(typeof(T));
        }

        public void ClearCache()
        {
            _classMaps.Clear();
        }

        public Guid GetNextGuid()
        {
            var b = Guid.NewGuid().ToByteArray();
            var dateTime = new DateTime(1900, 1, 1);
            var now = DateTime.Now;
            var timeSpan = new TimeSpan(now.Ticks - dateTime.Ticks);
            var timeOfDay = now.TimeOfDay;
            var bytes1 = BitConverter.GetBytes(timeSpan.Days);
            var bytes2 = BitConverter.GetBytes((long)(timeOfDay.TotalMilliseconds / 3.333333));
            Array.Reverse(bytes1);
            Array.Reverse(bytes2);
            Array.Copy(bytes1, bytes1.Length - 2, b, b.Length - 6, 2);
            Array.Copy(bytes2, bytes2.Length - 4, b, b.Length - 4, 4);
            return new Guid(b);
        }
    }
}