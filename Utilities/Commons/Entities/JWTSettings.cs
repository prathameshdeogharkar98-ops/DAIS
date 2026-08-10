using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Commons.Entities
{
    public class JWTSettings
    {
        public string Key { get; set; } = string.Empty;

        public string Issuer { get; set; } = string.Empty;

        public string Audience { get; set; } = string.Empty;

        public int ExpirationMinutes { get; set; }
    }
}

