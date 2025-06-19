using Dapper;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DataLayer
{
    public class SqlDBConnectionFactory : IDBConnectionFactory
    {
        public readonly string _connectionString;
        public int noOfConnections = 0;

        public SqlDBConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }
        public SqlConnection OpenConnection(CancellationToken token = default)
        {
            var connection = new SqlConnection(_connectionString);
            if (noOfConnections <= 10)
            {
                while (connection.State != System.Data.ConnectionState.Closed && connection.State != System.Data.ConnectionState.Broken)
                {
                    var reply = connection.OpenAsync(token).ConfigureAwait(true);
                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        noOfConnections++;
                        return connection;
                    }
                }
            }
            return connection;
        }

        public SqlConnection CloseConnection(SqlConnection sqlConx, CancellationToken token = default)
        {
            var connection = new SqlConnection(_connectionString);
            if (noOfConnections > 0)
            {
                connection.CloseAsync().ConfigureAwait(true);
                noOfConnections--;
            }
            return connection;
        }

        public async Task<List<dynamic>> CallStoredProc(string storedProc, Dictionary<string, object>? parameters = default)
        {
            var values = new DynamicParameters(parameters);
            CancellationToken cancelToken = default;
            SqlConnection conx = OpenConnection(cancelToken);
            
            var result = (await conx.QueryAsync(storedProc, values, commandType: CommandType.StoredProcedure).ConfigureAwait(true)).ToList();

            conx = CloseConnection(conx, cancelToken);

            return result;
        }
    }
    internal interface IDBConnectionFactory
    {
        SqlConnection OpenConnection(CancellationToken token = default);
    }
}
