using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Upload_ProfilePhoto.Hubs;
using Upload_ProfilePhoto.Models;
using Upload_ProfilePhoto.Repositorys;

namespace Upload_ProfilePhoto.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHostingEnvironment _appEnvironment;
        private readonly IAccountRepository _accountRepository;
        private readonly NotificationHub _notificationHub;

        public HomeController(ILogger<HomeController> logger, IHostingEnvironment appEnvironment, IAccountRepository accountRepository, NotificationHub notificationHub)
        {
            _logger = logger;
            _accountRepository = accountRepository;
            _appEnvironment = appEnvironment;
            _notificationHub = notificationHub;

        }

        public IActionResult Index()
        {
            string id = HttpContext.Session.GetString("UserId");
            if (id == null)
            {
                return RedirectToAction("Login", "User");
            }
            return View();
        }
        public IActionResult Profile()
        {
            string id = HttpContext.Session.GetString("UserId");
            if (id == null)
            {
                return RedirectToAction("Login", "User");
            }
            Comman data = _accountRepository.GetProfile(Convert.ToInt32(id));
            return View(data);
        }
        [HttpPost]
        public IActionResult Profile(string profile)
        {
            User user = new User();
            var files = HttpContext.Request.Form.Files;
            foreach (var Image in files)
            {
                if (Image != null && Image.Length > 0)
                {
                    var file = Image;
                    //There is an error here
                    var uploads = Path.Combine(_appEnvironment.WebRootPath, "ProfilePhoto");
                    if (file.Length > 0)
                    {
                        var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
                        using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                        {
                            file.CopyToAsync(fileStream);
                            user = _accountRepository.UpdateProfile(fileName);
                            if (user != null)
                            {
                                ViewBag.Sucess = "Profile Save Successfull..!";
                            }
                        }
                    }
                }
            }
            Comman data = _accountRepository.GetProfile(Convert.ToInt32(user.Id));
            ModelState.Clear();
            return View(data);
        }
        public JsonResult GetMyPicture()
        {
            var data = _accountRepository.MyAllPrifiles();
            return Json(data);
        }
        public JsonResult GetAllPicture()
        {
            var data = _accountRepository.GetAllPicture();
            return Json(data);
        }
        public JsonResult LoggedUserProfile()
        {
            var data = _accountRepository.CurrentUserPicture();
            return Json(data);
        }
        [HttpPost]
        public JsonResult Like(int pictureid, bool like)
        {
            var data = _accountRepository.Like(pictureid, like);
            var notification = _accountRepository.Notification(pictureid, like);
                if (notification != null)
                {
                        _notificationHub.SendNotification(notification);
                }
            
            return Json(data);
        }
        public JsonResult GetAllLIker()
        {
            var data = _accountRepository.GetLikes();
            return Json(data);
        }
        [HttpPost]
        public JsonResult Notification(int pictureid)
        {
            var data = _accountRepository.Notification(pictureid, true);

            return Json(data);
        }
        public JsonResult GetNotification()
        {
            var data = _accountRepository.GetUserNotification();
            return Json(data);
        }
        public JsonResult UpdateNotification(int NotifyId)
        {
            var data = _accountRepository.UpdateNotification(NotifyId);
            return Json(data);
        }

        public JsonResult SendComment(string comments,int pictureid)
        {
            var data = "Success";
            return Json(data);
        }
        public IActionResult Privacy()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
