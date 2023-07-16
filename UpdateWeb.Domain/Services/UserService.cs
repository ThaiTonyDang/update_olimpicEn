using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpdateWeb.Domain.ViewModels;
using UpdateWeb.Infrastructure.Repositories;

namespace UpdateWeb.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepositories _userRepositories;
        public UserService(IUserRepositories userRepositories)
        {
            _userRepositories = userRepositories;
        }

        public UserItemViewModel GetUserByPhone(string userName)
        {
            var user = _userRepositories.GetUserByPhone(userName);
            if(user != null)
            {
                var userItems = new UserItemViewModel()
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Class = user.Class,
                    Area = user.Area,
                    City = user.City,
                    District = user.District,
                    Email = user.Email,
                    School = user.School,
                    PhoneNumber = user.PhoneNumber,
                    UserName = user.UserName,
                    Khoa = user.Khoa,
                    Lop = user.Lop
                };

                return userItems;
            }

            return null;           
        }
    }
}
