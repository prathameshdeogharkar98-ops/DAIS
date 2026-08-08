using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class ConnectionStringBuilder 
    {
        private readonly IConfiguration _configuration;

        public ConnectionStringBuilder(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string getConnectionString()
        {
            return _configuration.GetConnectionString("DefaultConnection")??"";
        }
    }
}
