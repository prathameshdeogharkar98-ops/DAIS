using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.DataManagementService
{
    public interface IDataManagementService
    {
        public int ExecuteNonQuery(string commandText, CommandType commandType, params SqlParameter[] parameters);
        public object ExecuteScalar(string commandText, CommandType commandType, params SqlParameter[] parameters);
        public DataTable ExecuteDataTable(string commandText, CommandType commandType, params SqlParameter[] parameters);


    }
}
