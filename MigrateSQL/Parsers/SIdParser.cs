using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using MigrateSQL.Helpers;
using MigrateSQL.Settings;

namespace MigrateSQL.Parsers
{
    public class SIdParser : IDisposable
    {
        private StreamReader _dataReader;
        private DataTable _dataTable;

        public SIdParser(string path)
        {
            _dataReader = new StreamReader(path);
            _dataTable = new DataTable();
            _dataTable.Columns.Add("ID", typeof(string));
            _dataTable.Columns.Add("City", typeof(string));
            _dataTable.Columns.Add("Street", typeof(string));
        }

        public void Parse()
        {
            while (!_dataReader.EndOfStream)
            {
                var sIdString = _dataReader.ReadLine();
                if (sIdString == null) break;

                var sIdInfos = sIdString.Split(ParseSettings.Separators);

                var id = sIdInfos[0];
                var city = sIdInfos[1];
                var street = sIdInfos[2];

                _dataTable.Rows.Add(id, city, street);
            }
            SqlHelper.BulkCopy(_dataTable, "dbo.SID");

        }

        public void Dispose()
        {
            _dataReader.Close();
            _dataTable.Clear();
            GC.Collect();
        }
    }
}
