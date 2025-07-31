using System.Data;
using System.Data.SqlClient;

namespace ESInfrastructure.Database
{
    public interface ISqlConnectionFactory
    {
        SqlConnection GetOpenConnection();
    }
}
