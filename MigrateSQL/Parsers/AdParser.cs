using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using MigrateSQL.Helpers;
using MigrateSQL.Settings;

namespace MigrateSQL.Parsers
{
    public class AdParser : IDisposable
    {
        private StreamReader _dataReader;
        private DataTable _dataTable;


        public AdParser(string path)
        {
            _dataReader = new StreamReader(path);
            _dataTable = new DataTable();
            _dataTable.Columns.Add("ID", typeof(string));
            _dataTable.Columns.Add("UserType", typeof(byte));
            _dataTable.Columns.Add("Latitude", typeof(double));
            _dataTable.Columns.Add("Longitude", typeof(double));
        }

        public void Parse()
        {


            while (!_dataReader.EndOfStream)
            {
                var adString = _dataReader.ReadLine();
                if (adString == null) break;

                var adInfos = adString.Split(ParseSettings.Separators);

                var id = adInfos[0];
                var userTypeId = byte.Parse(adInfos[1]);
                var latitude = double.Parse(adInfos[2]);
                var longitude = double.Parse(adInfos[3]);

                _dataTable.Rows.Add(id, userTypeId, latitude, longitude);
            }

            SqlHelper.BulkCopy(_dataTable, "dbo.AD");
        }

        public void Dispose()
        {
            _dataReader.Close();
            _dataTable.Clear();
            GC.Collect();
        }
    }
}
