
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
        public HomeController(ILogger<HomeController> logger, ICityServices cityServices)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
