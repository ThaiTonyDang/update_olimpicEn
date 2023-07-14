
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Update.Web.Models;
using UpdateWeb.Domain.Services;

namespace Update.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICityServices _cityServices;
        public HomeController(ILogger<HomeController> logger, ICityServices cityServices)
        {
            _logger = logger;
            _cityServices = cityServices;
        }

        public IActionResult Index(int areaId = 3)
        {
            return View();
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
    }
}
