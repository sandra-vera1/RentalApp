using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalApp.Models;
using RentalApp.Services.AccountServices;
using RentalApp.Services.UserServices;
using RentalApp.ViewModels;
using System.Security.Claims;

namespace RentalApp.Controllers
{
    public class UserController : Controller
    {
        private readonly string _connectionString;
        private readonly IUserService _userService;
        private readonly IAccountService _accountService;
        public UserController(IOptions<ConnectionStringOptions> options,
            IUserService userService, IAccountService accountService)
        {
            // Retrieve the connection string from the options
            _connectionString = options.Value.Connection;
            _userService = userService;
            _accountService = accountService;
        }
        // GET: UserController
        [Authorize]
        public ActionResult Index()
        {
            int UserId = Convert.ToInt32(User.Claims.First(c => c.Type == "UserId").Value);//UserId from the session
            IEnumerable<UserViewModel> Model = _userService.ListUsers(_connectionString);
            Model = Model.Where(u => u.user.UserId == UserId);
            return View(Model);
        }

        [AllowAnonymous]
        // GET: UserController/Create
        public ActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        // POST: UserController/CreateUser
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUser(IFormCollection collection)
        {
            if (!User.Identity.IsAuthenticated)
            {
                try
                {
                    string fullName = collection["fullName"][0];
                    string password = collection["password"][0];
                    string userPhoneNumber = collection["userPhoneNumber"][0];
                    string userEmail = collection["userEmail"][0];
                    int userAccountType = Convert.ToInt32(collection["userAccountType"][0]);
                    User user = new User(fullName, password, userPhoneNumber, userEmail, userAccountType);
                    _userService.CreateUser(_connectionString, user);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View("Create");
                }
            }
            else
            {
                return View();
            }
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            IEnumerable<UserViewModel> users = _userService.ListUsers(_connectionString);
            User user = users.First(a => a.user.UserId == id).user;
            return View(user);
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, IFormCollection collection)
        {
            try
            {
                string fullName = collection["fullName"][0];
                string password = collection["password"][0];
                string userPhoneNumber = collection["userPhoneNumber"][0];
                string userEmail = collection["userEmail"][0];
                int userAccountType = Convert.ToInt32(collection["userAccountType"][0]);
                User user = new User(fullName, password, userPhoneNumber, userEmail, userAccountType)
                {
                    UserId = id,
                    UserAccountName = userAccountType == 1 ? "Owner" : "Renter"
                };
                _userService.EditUser(_connectionString, user);
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal claimsPrincipal = _accountService.CreateLogin(user);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
