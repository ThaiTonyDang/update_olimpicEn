using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpdateWeb.Infrastructure.Models;
using UpdateWeb.Infrastructure.SQL;
using Dapper;

namespace UpdateWeb.Infrastructure.Repositories
{
    public class CityRepositories : ICityRepositories
    {
        private readonly ISQLprovider _sqlProvider;
        public CityRepositories(ISQLprovider sqlProvider)
        {
            _sqlProvider = sqlProvider;
        }

        public List<City> GetCityById(int areaId)
        {
            var proc = "[dbo].[p_OLYMPICHSSV_EditAcount_City_Get]";
            using (var dapper = _sqlProvider.CreateConnection())
            {
                var listCitys = dapper.Query<City>(proc, new { AreaId = areaId }, commandType: CommandType.StoredProcedure).ToList();
                return listCitys;
            }
        }

        public List<District> GetDistrictByCityId(int cityId)
        {
            var proc = "[dbo].[p_OLYMPICHSSV_GetDistricts]";
            using (var dapper = _sqlProvider.CreateConnection())
            {
                var listDitricts = dapper.Query<District>(proc, new { CityId = cityId }, commandType: CommandType.StoredProcedure).ToList();
                return listDitricts;
            }
        }

        public List<School> GetSchoolByCityIdSchoolType(int cityId, int? districtId, string schoolType)
        {
            var district = districtId == null ? 0 : districtId;
            var proc = "[dbo].[p_OLYMPICHSSV_EditAcount_School_Get_By_SchoolType]";
            using (var dapper = _sqlProvider.CreateConnection())
            {
                var listSchools = dapper.Query<School>(proc,
                    new
                    {
                        CityId = cityId,
                        DistrictId = districtId,
                        SchoolType = schoolType
                    },
                    commandType: CommandType.StoredProcedure).ToList();
                return listSchools;
            }
        }
    }
}