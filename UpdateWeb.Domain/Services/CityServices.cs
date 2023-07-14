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
    public class CityServices : ICityServices
    {
        private readonly ICityRepositories _cityRepositories;
        public CityServices(ICityRepositories cityRepositories)
        {
            _cityRepositories = cityRepositories;
        }

        public List<CityItemViewModel> GetCityById(int areaId)
        {
            var cities = _cityRepositories.GetCityById(areaId);
            var cityItems = cities.Select(c => new CityItemViewModel
            {
                Id = c.Id,
                AreaId = c.AreaId,
                Name = c.Name,
                AreaName = c.AreaName
            }).ToList();

            return cityItems;
        }

        public List<DistrictItemViewModel> GetDistrictByCityId(int cityId)
        {
            var districts = _cityRepositories.GetDistrictByCityId(cityId);
            var districtItems = districts.Select(d => new DistrictItemViewModel
            {
                Id = d.Id,
                CityId = d.CityId,
                Name = d.Name,
                Code = d.Name
            }).ToList();

            return districtItems;
        }

        public List<SchoolItemViewModel> GetSchoolByCityIdSchoolType(int cityId, int? districtId, string schoolType)
        {
            var schools = _cityRepositories.GetSchoolByCityIdSchoolType(cityId, districtId, schoolType);
            var schoolItems = schools.Select(s => new SchoolItemViewModel
            {
                Id = s.Id,
                CityId = s.CityId,
                DistrictId = s.DistrictId,
                Name = s.Name,
                SchoolType = s.SchoolType
            }).ToList();

            return schoolItems;
        }
    }
}
