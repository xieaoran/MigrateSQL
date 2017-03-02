using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MigrateSQL.Parsers;

namespace MigrateSQL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var adParser = new AdParser(@"C:\WorkSpace\Documents\AdData\ad_samples.dat"))
            {
                adParser.Parse();
            }
            using (var sIdParser = new SIdParser(@"C:\WorkSpace\Documents\AdData\sid_street.dat"))
            {
                sIdParser.Parse();
            }
            using (var phoneBrowsingParser = new PhoneBrowsingParser())
            {
                foreach (var file in Directory.GetFiles(@"C:\WorkSpace\Documents\AdData\phone_browsing"))
                {
                    phoneBrowsingParser.ChangePath(file);
                    phoneBrowsingParser.Parse();
                }
            }

        }
    }
}
