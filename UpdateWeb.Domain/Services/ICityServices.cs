using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpdateWeb.Domain.ViewModels;
using UpdateWeb.Infrastructure.Models;

namespace UpdateWeb.Domain.Services
{
    public interface ICityServices
    {
        public List<CityItemViewModel> GetCityById(int areaId);
        public List<DistrictItemViewModel> GetDistrictByCityId(int cityId);
        public List<SchoolItemViewModel> GetSchoolByCityIdSchoolType(int cityId, int? districtId, string schoolType);
    }
}
