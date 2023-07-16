using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpdateWeb.Domain.Services;
using UpdateWeb.Infrastructure.Repositories;

namespace UpdateWeb.Domain.ViewModels
{
    public class UserItemViewModel
    {
        private readonly ICityServices _cityServices;
        public UserItemViewModel() { }
        public UserItemViewModel(ICityServices cityServices)
        {
            _cityServices = cityServices;
        }
        public string Id { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Class { get; set; }
        public int City { get; set; }
        public int District { get; set; }
        public int School { get; set; }
        public string Khoa { get; set; }
        public string Lop { get; set; }
        public PasswordItemViewModel PasswordItem { get; set; }
        public string CityName { get; set; }
        public int Area { get; set; }
    }
}
