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
            var cities = _cityRepositories.GetCityByAreaId(areaId);
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

        public CityItemViewModel GetCityByCityId(int cityId)
        {
            var city = _cityRepositories.GetCityByCityId(cityId);
            var cityItem = new CityItemViewModel()
            {
                Id = city.Id,
                AreaId = city.AreaId,
                AreaName = city.AreaName,
                Name = city.Name,
            };

            return cityItem;
        }

        public DistrictItemViewModel GetDistrictById(int districtId)
        {
            var district = _cityRepositories.GetDistrictById(districtId);
            var districtItem = new DistrictItemViewModel()
            {
                Id = district.Id,
                CityId = district.CityId,
                Name = district.Name,
            };

            return districtItem;
        }

        public SchoolItemViewModel GetSchoolById(int schoolId)
        {
            var school = _cityRepositories.GetSchoolById(schoolId);
            var schoolItem = new SchoolItemViewModel()
            {
                Id = school.Id,
                CityId = school.CityId,
                DistrictId = school.DistrictId,
                Name = school.Name,
                SchoolType = school.SchoolType
            };

            return schoolItem;
        }
    }
}
