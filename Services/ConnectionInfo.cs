using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardWebAPIServer.Services
{
    public static class ConnectionInfo
    {
        public static string GetDbConnectionString()
        {
            return "mongodb://localhost:27017";
        }
    }
}
