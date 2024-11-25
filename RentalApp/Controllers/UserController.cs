using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalApp.Models;
using RentalApp.Services.UserServices;
using RentalApp.ViewModels;
using System.Net;

namespace RentalApp.Controllers
{
    public class UserController : Controller
    {
        private readonly string _connectionString;
        private readonly IUserService _userService;
        public UserController(IOptions<ConnectionStringOptions> options,
            IUserService userService)
        {
            // Retrieve the connection string from the options
            _connectionString = options.Value.Connection;
            _userService = userService;
        }
        // GET: UserController
        public ActionResult Index()
        {
            IEnumerable<UserViewModel> Model = _userService.ListUsers(_connectionString);
            return View(Model);
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUser(IFormCollection collection)
        {
            try
            {
                string userName = collection["userName"][0];
                string password = collection["password"][0];
				string userPhoneNumber = collection["userPhoneNumber"][0];
				string userEmail = collection["userEmail"][0];
                int userAccountType = Convert.ToInt32(collection["userAccountType"][0]);
				User user = new User(userName, password, userPhoneNumber, userEmail, userAccountType);
                _userService.CreateUser(_connectionString, user);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("Create");
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
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                string userName = collection["userName"][0];
                string password = collection["password"][0];
                string userPhoneNumber = collection["userPhoneNumber"][0];
                string userEmail = collection["userEmail"][0];
                int userAccountType = Convert.ToInt32(collection["userAccountType"][0]);
                User user = new User(userName, password, userPhoneNumber, userEmail, userAccountType);
                user.UserId = id;
                _userService.EditUser(_connectionString, user);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
