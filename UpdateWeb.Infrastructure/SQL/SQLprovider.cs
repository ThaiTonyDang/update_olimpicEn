using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdateWeb.Infrastructure.SQL
{
    public class SQLprovider : ISQLprovider
    {
        private readonly string _stringConnection;
        public SQLprovider(IConfiguration configuration) // Sử dụng Ioption
        {
            this._stringConnection = configuration.GetConnectionString("OLYMPICENGLISH.VN");
        }
        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_stringConnection);
        }
    }
}
