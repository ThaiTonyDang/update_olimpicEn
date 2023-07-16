using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using UpdateWeb.Domain.Services;
using UpdateWeb.Domain.ViewModels;

namespace Update.Web.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICityServices _cityServices;
        private static CityItemViewModel _city;
        public ProfileController(ILogger<HomeController> logger, ICityServices cityServices)
        {
            _logger = logger;
            _cityServices = cityServices;
        }

        [HttpGet]
        [Route("profile")]
        public IActionResult Index(UserItemViewModel user)
        {
            return View(user);
        }

        [HttpGet]
        [Route("/update/cities/{areaId}")]
        public JsonResult GetCity(int areaId = 0)
        {
            var cities = _cityServices.GetCityById(areaId);
            return Json(cities);
        }

        [HttpGet]
        [Route("/update/districts/{cityId}")]
        public JsonResult GetDistrict(int cityId = 0)
        {
            var districts = _cityServices.GetDistrictByCityId(cityId);
            return Json(districts);
        }

        [HttpGet]
        [Route("schools")]
        public JsonResult GetSchoolById([FromQuery] string schoolType, int cityId, int districtId)
        {
            var schools = _cityServices.GetSchoolByCityIdSchoolType(cityId, districtId, schoolType);
            return Json(schools);
        }

        [HttpGet]
        [Route("/city/{cityId}")]
        public JsonResult GetCityByCityId(int cityId)
        {
            var city = _cityServices.GetCityByCityId(cityId);
            return Json(city);
        }

        [HttpGet]
        [Route("/district/{districtId}")]
        public JsonResult GetDistrictById(int districtId)
        {
            var district = _cityServices.GetDistrictById(districtId);
            return Json(district);
        }

        [HttpGet]
        [Route("/school/{schoolId}")]
        public JsonResult GetSchoolById(int schoolId)
        {
            var school = _cityServices.GetSchoolById(schoolId);
            return Json(school);
        }
    }
}
