using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.Sqlite;

namespace Services.DapperContext
{
    public class AppDapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public AppDapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DbConnection");
        }
        public IDbConnection CreateConnection()
            => new SqliteConnection(_connectionString);
    }
}
