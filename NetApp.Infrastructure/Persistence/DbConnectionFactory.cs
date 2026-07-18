using Microsoft.Extensions.Configuration;
using NetApp.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetApp.Infrastructure.Persistence
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly IConfiguration _configuration;

        public DbConnectionFactory(IConfiguration conf)
        {
            _configuration = conf;
        }

        public string GetConnectionStringDefault(string name)
        {
            var connectionString = _configuration.GetConnectionString(name);

            if(string.IsNullOrEmpty(connectionString))
                throw new InvalidOperationException($"Connection string '{name}' not found.");
            
            return connectionString;
        }
    }
}
