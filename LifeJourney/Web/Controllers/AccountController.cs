using BL.DTOs;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using BL.Interfaces;
using Web.Models;
using AspNetCoreHero.ToastNotification.Abstractions;
using Shared.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace Web.Controllers
{
    public class AccountController : Controller
    {
        private IUserRepo _userRepo;

        private StateHelper _stateHelper;
        private readonly INotyfService _notyf;

        public AccountController(IUserRepo userRepo,
             INotyfService notyf,
             StateHelper stateHelper)
        {
            _userRepo = userRepo;
            _notyf = notyf;
            _stateHelper = stateHelper;

        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(AddUserDTO dto)
        {
            _userRepo.Add(dto);
            _notyf.Success("User registered successfully");
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Chat");
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginDTO dto)
        {
            var user = _userRepo.Get(dto.Email);

            if (user == null)
            {
                _notyf.Error("User not found");
                return View();
            }
            else if (!SecurityHelper.ValidateHash(dto.Password, user.Password))
            {
                _notyf.Error("User credentials are wrong");
                return View();
            }
            else
            {
                var identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Email),
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.UserData, JsonConvert.SerializeObject(user))
                }, CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);


                return RedirectToAction("Index", "Chat");
            }

        }
        [HttpGet("Logout"), Authorize]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
