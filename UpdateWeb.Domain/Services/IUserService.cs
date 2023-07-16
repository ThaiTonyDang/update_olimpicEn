using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpdateWeb.Domain.ViewModels;

namespace UpdateWeb.Domain.Services
{
    public interface IUserService
    {
        public UserItemViewModel GetUserByPhone(string userName);
    }
}
