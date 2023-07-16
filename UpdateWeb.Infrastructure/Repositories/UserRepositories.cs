using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpdateWeb.Infrastructure.Models;
using UpdateWeb.Infrastructure.SQL;

namespace UpdateWeb.Infrastructure.Repositories
{
    public class UserRepositories : IUserRepositories
    {
        private readonly ISQLprovider _sqlProvider;
        public UserRepositories(ISQLprovider sqlProvider)
        {
            _sqlProvider = sqlProvider;
        }
        public User GetUserByPhone(string phoneNumber)
        {
            var proc = "[dbo].[p_OLP_AspNetUsers_FindByUserName]";
            using (var dapper = _sqlProvider.CreateConnection())
            {
                var user = dapper.QueryFirst<User>(proc, new { UserName = phoneNumber }, commandType: CommandType.StoredProcedure);

                if(string.IsNullOrEmpty(user.FullName) && string.IsNullOrEmpty(user.UserName) && string.IsNullOrEmpty(user.Email))
                {
                    return null;
                }    
                return user;
            }
        }
    }
}