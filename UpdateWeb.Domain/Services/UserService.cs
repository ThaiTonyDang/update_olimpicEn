using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpdateWeb.Domain.ViewModels;
using UpdateWeb.Infrastructure.Models;
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
                    FullName = user.FullName.Trim(),
                    Class = user.Class.Trim(),
                    Area = user.Area,
                    City = user.City,
                    District = user.District,
                    Email = user.Email,
                    School = user.School,
                    PhoneNumber = user.PhoneNumber,
                    UserName = user.UserName,
                    Khoa = user.Khoa,
                    Lop = user.Lop,
                    EmailConfirmed = user.EmailConfirmed
                };

                return userItems;
            }

            return null;           
        }

        public int UpdateUser(UserItemViewModel userItem)
        {
            var user = new User
            {
                FullName = userItem.FullName,
                Email = userItem.Email,
                Area = userItem.Area,
                PhoneNumber = userItem.UserName,
                Class = userItem.Class,
                UserName = userItem.UserName,
                City = userItem.City,
                District = userItem.District,
                Id = userItem.Id,
                School = userItem.School,
                Khoa = userItem.Khoa,
                Lop = userItem.Lop,
                EmailConfirmed = userItem.EmailConfirmed
            };

            var isActive = _userRepositories.Update(user);
            return isActive;
        }
    }
}
