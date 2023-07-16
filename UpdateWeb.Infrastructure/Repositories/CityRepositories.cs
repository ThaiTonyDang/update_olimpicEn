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

        public List<City> GetCityByAreaId(int areaId)
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

        public City GetCityByCityId(int cityId)
        {
            using (var connection = _sqlProvider.CreateConnection())
            {
                if (connection != null)
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        var city = new City();
                        command.CommandText = "SELECT * FROM dbo.t_OLYMPIC_ENGLISH_Cities Where Id = @CityId";
                        var id = new SqlParameter("@CityId", cityId);
                        command.Parameters.Add(id);
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                city.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                                city.Name = reader.GetString(reader.GetOrdinal("Name")).Trim();
                                city.AreaName = reader.GetString(reader.GetOrdinal("AreaName")).Trim();
                                city.AreaId = reader.GetInt32(reader.GetOrdinal("AreaId"));
                            }
                        }

                        return city;
                    }
                }

                return null;
            }
        }

        public District GetDistrictById(int districtId)
        {
            using (var connection = _sqlProvider.CreateConnection())
            {
                if (connection != null)
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        var district = new District();
                        command.CommandText = "SELECT * FROM dbo.t_OLYMPIC_ENGLISH_Districts WHERE Id = @DistrictId";
                        var id = new SqlParameter("@DistrictId", districtId);
                        command.Parameters.Add(id);
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                district.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                                district.Name = reader.GetString(reader.GetOrdinal("Name")).Trim();
                                district.CityId = reader.GetInt32(reader.GetOrdinal("CityId"));
                            }
                        }

                        return district;
                    }
                }

                return null;
            }
        }

        public School GetSchoolById(int schoolId)
        {
            using (var connection = _sqlProvider.CreateConnection())
            {
                if (connection != null)
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        var school = new School();
                        command.CommandText = "SELECT * FROM dbo.t_OLYMPIC_ENGLISH_Schools WHERE Id = @SchoolId";
                        var id = new SqlParameter("@SchoolId", schoolId);
                        command.Parameters.Add(id);
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                school.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                                school.Name = reader.GetString(reader.GetOrdinal("Name")).Trim();
                                school.CityId = Int32.Parse(reader[nameof(school.CityId)].ToString());  
                                
                                if(reader[nameof(school.DistrictId)].GetType() == typeof(int))
                                {
                                    school.DistrictId = Int32.Parse(reader[nameof(school.DistrictId)].ToString());
                                }    
                                                      
                                school.SchoolType = reader.GetString(reader.GetOrdinal("SchoolType")).Trim();
                            }
                        }

                        return school;
                    }
                }

                return null;
            }
        }
    }
}