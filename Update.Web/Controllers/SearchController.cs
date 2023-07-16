using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using UpdateWeb.Domain.Services;
using UpdateWeb.Domain.ViewModels;
using UpdateWeb.Infrastructure.Const;

namespace Update.Web.Controllers
{
    public class SearchController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogger<HomeController> _logger;
        private readonly ICityServices _cityServices;
        private static UserItemViewModel _userItem;
        public SearchController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Search()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SearchUserInformation(string phoneNumber)
        {
            if(string.IsNullOrEmpty(phoneNumber))
            {
                return BadRequest(new
                {
                    Message = "Phone Number Không Được Để Trống !"
                });
            }    
            var userName = phoneNumber;
            var user = _userService.GetUserByPhone(userName);
            _userItem = user;
            if (user == null)
            {
                return NotFound(new
                {
                    Message = "Không Tìm Thấy User !"
                });
            }

            return Ok(user);
        }

        [HttpGet]
        [Route("detail")]
        public IActionResult ViewDetail()       
        {
            TempData[Mode.MODE] = Mode.USING_LABEL_CONFIRM;
            var user = _userItem;
            if (user == null)
            {
                TempData[Mode.LABEL_CONFIRM_CHECK] = "Nhập Số Điện Thoại Cần Tìm";
                return RedirectToAction("Search", "Search");
            }
            return RedirectToAction("Index", "Profile", new UserItemViewModel{
                UserName = user.UserName,
                Class = user.Class,
                FullName = user.FullName,
                Email = user.Email,
                Khoa = user.Khoa,
                Lop  = user.Lop,
                Area = user.Area,
                City = user.City,
                District = user.District,
                School = user.School,
                Id = user.Id 
            });
        }
    }
}
