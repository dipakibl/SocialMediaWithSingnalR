using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Upload_ProfilePhoto.Models;
using Upload_ProfilePhoto.Repositorys;

namespace Upload_ProfilePhoto.Controllers
{
    public class UserController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        public UserController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(User user)
        {
            try
            {
                if (user != null)
                {
                   int success = _accountRepository.InsertUser(user);
                    if (success == 1)
                    {
                        return RedirectToAction("Login");
                    }
                   
                }
               
            }
            catch (Exception)
            {

                throw;
            }
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string Email, string Password)
        {
            try
            {
                if (Email != null && Password != null)
                {
                  User success =   _accountRepository.Login(Email,Password);
                    if (success != null)
                    {
                        HttpContext.Session.SetString("UserId", success.Id.ToString());
                        HttpContext.Session.SetString("UserName", success.First_Name);
                        return RedirectToAction("Index","Home");
                    }
                    else if (success == null)
                    {
                        ViewBag.Invalid = "Invalid Email & Password..!";
                        return View();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return View();
        }
        public ActionResult Logout()
        {
            HttpContext.Session.Remove("UserId");
            return RedirectToAction("Login");
        }

    }
}