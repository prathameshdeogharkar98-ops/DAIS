using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.DataManagementService
{
    public class DataManagementService : IDataManagementService
    {
        private readonly string _connectionString;
        private readonly ConnectionStringBuilder _connectionStringBuilder;

        public DataManagementService(ConnectionStringBuilder connectionStringBuilder)
        {
            _connectionStringBuilder = connectionStringBuilder;
            _connectionString = _connectionStringBuilder.getConnectionString();
        }

        public int ExecuteNonQuery(
        string commandText,
        CommandType commandType,
        params SqlParameter[] parameters)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand(commandText, connection);

            command.CommandType = commandType;

            if (parameters != null && parameters.Length > 0)
                command.Parameters.AddRange(parameters);

            connection.Open();

            return command.ExecuteNonQuery();
        }

        /// <summary>
        /// Executes Scalar queries.
        /// </summary>
        public object ExecuteScalar(
            string commandText,
            CommandType commandType,
            params SqlParameter[] parameters)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand(commandText, connection);

            command.CommandType = commandType;

            if (parameters != null && parameters.Length > 0)
                command.Parameters.AddRange(parameters);

            connection.Open();

            return command.ExecuteScalar();
        }

        /// <summary>
        /// Returns DataTable.
        /// </summary>
        public DataTable ExecuteDataTable(
            string commandText,
            CommandType commandType,
            params SqlParameter[] parameters)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand(commandText, connection);

            command.CommandType = commandType;

            if (parameters != null && parameters.Length > 0)
                command.Parameters.AddRange(parameters);

            using SqlDataAdapter adapter = new SqlDataAdapter(command);

            DataTable dt = new DataTable();

            adapter.Fill(dt);

            return dt;
        }
    }
}
