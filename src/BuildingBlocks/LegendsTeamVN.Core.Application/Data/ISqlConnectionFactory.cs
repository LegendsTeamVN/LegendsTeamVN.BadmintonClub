using System.Data;

namespace LegendsTeamVN.Core.Application.Data;

public interface ISqlConnectionFactory
{
    IDbConnection CreateConnection();
}
