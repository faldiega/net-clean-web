using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NetApp.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetApp.Infrastructure.Data
{
    public class DapperContext
    {
        private readonly string _connectionString;

        public DapperContext(IDbConnectionFactory factory)
        {
            _connectionString = factory.GetConnectionStringDefault("DefaultDB");
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);

    }
}
