#region 导入名称空间

using System.Collections.Generic;

#endregion 导入名称空间

namespace Dapper
{
    internal class Oracle : SqlDialectBase
    {
        public override char OpenQuote
        {
            get { return '"'; }
        }

        public override char CloseQuote
        {
            get { return '"'; }
        }

        public override string GetNextSequence(string sequenceName)
        {
            return string.Format("SELECT {0}.NEXTVAL AS Id FROM DUAL", sequenceName);
        }

        public override string GetCurrentSequence(string sequenceName)
        {
            return string.Format("SELECT {0}.CURRVAL AS Id FROM DUAL", sequenceName);
        }

        public override string GetIdentitySql(string tableName)
        {
            return string.Format("SELECT MAX(ID) as Id FROM {0}", tableName);
        }

        public override string GetPagingSql(string sql, int page, int resultsPerPage,
            IDictionary<string, object> parameters)
        {
            var startValue = (page - 1) * resultsPerPage;
            return GetSetSql(sql, startValue, resultsPerPage, parameters);
        }

        public override string GetSetSql(string sql, int firstResult, int maxResults,
            IDictionary<string, object> parameters)
        {
            var lowerLimit = firstResult;
            var upperLimit = firstResult + maxResults;
            var result = string.Format("SELECT * FROM (SELECT ROWNUM RN, A.* FROM({0}) A WHERE ROWNUM<=:upperLimit ) WHERE RN >:lowerLimit", sql);
            parameters.Add(":upperLimit", upperLimit);
            parameters.Add(":lowerLimit", lowerLimit);
            return result;
        }

        public override char ParameterPrefix
        {
            get
            {
                return ':';
            }
        }

        public override bool SupportsMultipleStatements
        {
            get
            {
                return false;
            }
        }

        public override string GetColumnName(string prefix, string columnName, string alias)
        {
            return base.GetColumnName(null, columnName, alias);
        }
    }
}