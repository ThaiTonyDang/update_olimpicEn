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
        public List<City> GetCityByAreaId(int areaId);
        public List<District> GetDistrictByCityId(int cityId);
        public List<School> GetSchoolByCityIdSchoolType(int cityId, int? districtId, string schoolType);
        public City GetCityByCityId(int cityId);
        public District GetDistrictById(int districtId);
        public School GetSchoolById(int schoolId);
    }
}
