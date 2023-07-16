using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpdateWeb.Infrastructure.Models;

namespace UpdateWeb.Infrastructure.Repositories
{
    public interface IUserRepositories
    {
        public User GetUserByPhone(string userName);
    }
}
