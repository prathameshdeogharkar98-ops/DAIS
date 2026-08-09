using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Extensions
{
    public static class ToListOfDictionary
    {
        public static List<Dictionary<string, object>> convtListOfDictionary(this DataTable dt)
        {

            var lstOfDics = new List<Dictionary<string, object>>();
            var columns = dt.Columns
                .Cast<DataColumn>()
                .Select(c => c.ColumnName)
                .ToArray();

            foreach (DataRow x in dt.Rows)
            {
                var dic = new Dictionary<string, object>();

                foreach (string col in columns)
                {
                    dic.Add(col, x[col]);
                }

                lstOfDics.Add(dic);
            }
            return lstOfDics;
        }
    }
}
