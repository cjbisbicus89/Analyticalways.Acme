using Analyticalways.Acme.Tranversal.Common;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Analyticalways.Acme.Infraestructure.Data
{
    public class ConnectionFactory: IConnectionFactory
    {
        private readonly IConfiguration _configuration;

        public ConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection GetConnection
        {
            get
            {
                return null;
            }
            
        }

    }
}
