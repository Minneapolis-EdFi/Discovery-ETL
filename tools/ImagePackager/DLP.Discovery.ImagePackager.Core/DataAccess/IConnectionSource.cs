using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLP.Discovery.ImagePackager.Core.DataAccess
{
    public interface IConnectionSource
    {
        string GetConnectionString(string connectionName);
    }

    public class ConnectionSource : IConnectionSource
    {
        public string GetConnectionString(string connectionName)
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
        }
    }
}
