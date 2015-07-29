using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace atpac.webapi.Helper
{
    public static class DataRowExtensions
    {
        public static T GetValueOrDefault<T>(this DataRow row, string columnName)
        {
            if (!row.IsNull(columnName))
            {
                // Might want to support type conversion using Convert.ChangeType().
                return (T)row[columnName];
            }
            return default(T);
        }
    }
}