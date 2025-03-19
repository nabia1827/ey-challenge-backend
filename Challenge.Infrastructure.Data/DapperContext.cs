using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Challenge.Infrastructure.Data
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            /* _connectionString = configuration.GetConnectionString("KrcpConnection");*/ //legacy: para conexiones con el valor en appsettings            
            _connectionString = configuration["Config:kvDataBase"];
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);

    }
}
