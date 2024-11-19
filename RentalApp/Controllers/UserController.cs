using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalApp.Models;
using System.Net;

namespace RentalApp.Controllers
{
    public class UserController : Controller
    {
        private readonly User _user;
        public UserController(IOptions<ConnectionStringOptions> options)
        {
            // Retrieve the connection string from the options
            string _connectionString = options.Value.Connection;
			_user = new User(_connectionString);
        }
        // GET: UserController
        public ActionResult Index()
        {
            List<User> Model = _user.ListUsers();
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
				_user.CreateUser(user);
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
            List<User> users = _user.ListUsers();
            User user = users.First(a => a.UserId == id);
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
                _user.EditUser(user);
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
