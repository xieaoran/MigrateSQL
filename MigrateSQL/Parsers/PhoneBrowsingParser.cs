using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MigrateSQL.Helpers;
using MigrateSQL.Settings;

namespace MigrateSQL.Parsers
{
    public class PhoneBrowsingParser : IDisposable
    {
        private StreamReader _dataReader;
        private DataTable _browsingDataTable;
        private DataTable _phoneDataTable;
        private List<string> _localPhones;
        private int _id;

        public PhoneBrowsingParser()
        {
            _id = 1;
            _browsingDataTable = new DataTable();
            _browsingDataTable.Columns.Add("ID", typeof(int));
            _browsingDataTable.Columns.Add("Time", typeof(DateTime));
            _browsingDataTable.Columns.Add("Site", typeof(string));
            _browsingDataTable.Columns.Add("Generation", typeof(short));
            _browsingDataTable.Columns.Add("AD_ID", typeof(string));
            _browsingDataTable.Columns.Add("Phone_ID", typeof(string));
            _browsingDataTable.Columns.Add("SID_ID", typeof(string));

            _phoneDataTable = new DataTable();
            _phoneDataTable.Columns.Add("ID", typeof(int));
            _phoneDataTable.Columns.Add("Mobile", typeof(string));

            _localPhones = new List<string>();
        }

        public void ChangePath(string path)
        {
            _dataReader?.Close();
            _phoneDataTable.Rows.Clear();
            _browsingDataTable.Rows.Clear();
            _dataReader = new StreamReader(path);
        }

        public void Parse()
        {
            while (!_dataReader.EndOfStream)
            {
                var phoneBrowsingString = _dataReader.ReadLine();
                if (phoneBrowsingString == null) break;

                var phoneBrowsingInfos = phoneBrowsingString.Split(ParseSettings.Separators);

                var time = phoneBrowsingInfos[0];
                var mobile = phoneBrowsingInfos[1];
                var site = phoneBrowsingInfos[2];
                var adId = phoneBrowsingInfos[4];
                var sIdId = phoneBrowsingInfos[5];
                var generation = short.Parse(phoneBrowsingInfos[6]);

                var year = int.Parse(time.Substring(0, 4));
                var month = int.Parse(time.Substring(4, 2));
                var day = int.Parse(time.Substring(6, 2));
                var hour = int.Parse(time.Substring(8, 2));
                var dateTime = new DateTime(year, month, day, hour, 0, 0);

                var phoneId = _localPhones.IndexOf(mobile) + 1;
                if (phoneId <= 0)
                {
                    _localPhones.Add(mobile);
                    phoneId = _localPhones.Count;
                    _phoneDataTable.Rows.Add(phoneId, mobile);
                }

                _browsingDataTable.Rows.Add(_id, dateTime, site, generation, adId, phoneId, sIdId);
            }
            SqlHelper.BulkCopy(_phoneDataTable, "dbo.Phone");
            SqlHelper.BulkCopy(_browsingDataTable, "dbo.Browsing");
        }

        public void Dispose()
        {
            _dataReader.Close();
            _localPhones.Clear();
            _phoneDataTable.Clear();
            _browsingDataTable.Clear();
            GC.Collect();
        }
    }
}
