using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalApp.Models;
using RentalApp.Services.AccountServices;
using System.Security.Claims;

namespace RentalApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly string _connectionString;
        private readonly IAccountService _accountService;
        public AccountController(IOptions<ConnectionStringOptions> options, //This parameter is for get the connection string
            IAccountService accountService)
        {
            // Retrieve the connection string from the options
            _connectionString = options.Value.Connection;
            _accountService = accountService;
        }
        // GET: AccountController
        public ActionResult Login()
        {
            return View();
        }

        // POST: AccountController/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(string useremail, string password)
        {
            try
            {
                User autUser = _accountService.CheckLogin(_connectionString, useremail, password);
                if (autUser.UserEmail != null)
                {
                    ClaimsPrincipal claimsPrincipal = _accountService.CreateLogin(autUser);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ErrorViewModel errorViewModel = new ErrorViewModel
                    {
                        ErrorMessage = "Invalid login attempt."
                    };
                    return View(errorViewModel);
                }
            }
            catch
            {
                ErrorViewModel errorViewModel = new ErrorViewModel
                {
                    ErrorMessage = "Invalid login attempt."
                };
                return View(errorViewModel);
            }
        }

        // GET: AccountController/Edit/5
        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        // GET: AccountController/AccessDenied
        public ActionResult AccessDenied()
        {
            return View();
        }



    }
}
