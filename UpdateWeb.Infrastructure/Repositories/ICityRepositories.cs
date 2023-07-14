using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpdateWeb.Infrastructure.Models;

namespace UpdateWeb.Infrastructure.Repositories
{
    public interface ICityRepositories
    {
        public List<City> GetCityById(int areaId);
        public List<District> GetDistrictByCityId(int cityId);
        public List<School> GetSchoolByCityIdSchoolType(int cityId, int? districtId, string schoolType);
    }
}
