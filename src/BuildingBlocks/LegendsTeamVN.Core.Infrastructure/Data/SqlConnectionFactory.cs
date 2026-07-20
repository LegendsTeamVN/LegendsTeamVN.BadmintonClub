using System.Data;
using LegendsTeamVN.Core.Application.Data;
using Microsoft.Data.SqlClient;

namespace LegendsTeamVN.Core.Infrastructure.Data;

public sealed class SqlConnectionFactory(string connectionString) : ISqlConnectionFactory
{
    private readonly string _connectionString = connectionString;

    public IDbConnection CreateConnection()
    {
        var connection = new SqlConnection(_connectionString);
        connection.Open();
        return connection;
    }
}
