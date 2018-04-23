﻿#region 导入名称空间

using System.Collections.Generic;

#endregion 导入名称空间

namespace Dapper
{
    internal class MySql : SqlDialectBase
    {
        public override char OpenQuote
        {
            get { return '`'; }
        }

        public override char CloseQuote
        {
            get { return '`'; }
        }

        public override string GetIdentitySql(string tableName)
        {
            return "SELECT CONVERT(LAST_INSERT_ID(), SIGNED INTEGER) AS Id";
        }

        public override string GetPagingSql(string sql, int page, int resultsPerPage,
            IDictionary<string, object> parameters)
        {
            var startValue = page * resultsPerPage;
            return GetSetSql(sql, startValue, resultsPerPage, parameters);
        }

        public override string GetSetSql(string sql, int firstResult, int maxResults,
            IDictionary<string, object> parameters)
        {
            var result = string.Format("{0} LIMIT @firstResult, @maxResults", sql);
            parameters.Add("@firstResult", firstResult);
            parameters.Add("@maxResults", maxResults);
            return result;
        }
    }
}