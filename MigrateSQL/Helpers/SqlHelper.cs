using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MigrateSQL.Settings;

namespace MigrateSQL.Helpers
{
    public static class SqlHelper
    {
        public static void BulkCopy(DataTable dataTable, string destinationTableName)
        {
            using (var sqlbulkcopy = new SqlBulkCopy(SqlSettings.ConnectionString, SqlBulkCopyOptions.UseInternalTransaction))
            {
                sqlbulkcopy.BulkCopyTimeout = 65535;
                sqlbulkcopy.DestinationTableName = destinationTableName;
                for (var i = 0; i < dataTable.Columns.Count; i++)
                {
                    sqlbulkcopy.ColumnMappings.Add(dataTable.Columns[i].ColumnName, dataTable.Columns[i].ColumnName);
                }
                sqlbulkcopy.WriteToServer(dataTable);
            }
        }
    }
}
