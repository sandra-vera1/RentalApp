using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalApp.Models;
using System.Diagnostics.Metrics;
using System.Security.Principal;

namespace RentalApp.Controllers
{
    public class AddressController : Controller
    {
        //create connection with Model)//step 3
        private readonly Address _address;
        public AddressController(IOptions<ConnectionStringOptions> options) //This parameter is for get the connection string
        {
            // Retrieve the connection string from the options
            string _connectionString = options.Value.Connection;
            _address = new Address(_connectionString); // This line set the connection string to Model and for use this class
        }


        // GET: AddressController
        public ActionResult Index()
        {
            List <Address> Model = _address.ListAddress();
            return View(Model);
        }

        // GET: AddressController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AddressController/Create
        public ActionResult Create()
        {
            return View(_address);
        }

        // POST: AddressController/Create

        //create connection with Model)//step 4 before this step you need to create the View inside a folder with the same name in this case Address.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {  //This is the bridge between the View and Controller. In other words this cast the object that is in the interface.
               //int AddressId it is not necessary because the ID field (DB) is type of Identity
                string Neighborhood = collection.ToList()[0].Value;
                int StreetNumber = Convert.ToInt32(collection.ToList()[1].Value);
                string StreetName = collection.ToList()[3].Value;
                string City = collection.ToList()[4].Value;
                int Province = Convert.ToInt32(collection.ToList()[5].Value);
                string Country = collection.ToList()[6].Value;
                string PostalCode = collection.ToList()[7].Value;
                int SuiteNumber = Convert.ToInt32(collection.ToList()[8].Value);
                Address address = new Address(Neighborhood, StreetNumber, StreetName, City, Province, Country, PostalCode, SuiteNumber);
                _address.CreateAddress(address); //This call the create method with the bridge that is in line 18
                return RedirectToAction(nameof(Create));
            }
            catch
            {
                return View("Create"); //this return at the sime view.
            }
        }

        // GET: AddressController/Edit/5
        public ActionResult Edit(int id)  //Here is for write Update code
        {
            return View();
        }

        // POST: AddressController/Edit/5
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

        // GET: AddressController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AddressController/Delete/5
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
