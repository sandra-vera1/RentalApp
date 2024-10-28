using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalApp.Models;

namespace RentalApp.Controllers
{
    public class UserController : Controller
    {
        private readonly AppManager appManager;
        public UserController(IOptions<ConnectionStringOptions> options)
        {
            // Retrieve the connection string from the options
            string _connectionString = options.Value.Connection;
            appManager = new AppManager(_connectionString);
        }
        // GET: UserController
        public ActionResult Index()
        {
            return View();
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
                int userId = Convert.ToInt32(collection.ToList()[0].Value);
                string userName = collection.ToList()[2].Value;
                string password = collection.ToList()[3].Value;
                string userPhoneNumber = collection.ToList()[4].Value;
                string userEmail = collection.ToList()[5].Value;
                int userAccountType = Convert.ToInt32(collection.ToList()[6].Value);
                User user = new User(userId, userName, password, userPhoneNumber, userEmail, userAccountType);
                appManager.CreateUser(user);
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
            return View();
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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
