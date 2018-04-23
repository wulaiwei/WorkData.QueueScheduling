using System;
using System.Collections.Generic;

namespace Dapper
{
    public interface IClassMapper
    {
        string SchemaName { get; }
        string TableName { get; }
        IList<IPropertyMap> Properties { get; }
        Type EntityType { get; }

        IClassMapper GetMap();
    }
}