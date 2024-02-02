using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vinaio
{
    public static class DatabaseConfig
    {
        public static string DatabaseFilePath
        {
            get { return ConfigurationManager.AppSettings["DatabaseFilePath"]; }
        }
    }
}
