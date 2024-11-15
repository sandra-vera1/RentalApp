using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
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

        // [Address List step 2]
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
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("Create"); //this return at the sime view.
            }
        }

        // [Update Address] - step 2 //Here is for pass the information to the Edit View
        // GET: AddressController/Edit/5
        public ActionResult Edit(int id)  
        {
            List<Address> addresses = _address.ListAddress();
            Address address = addresses.First(a => a.AddressId == id);
            address.Provinces = _address.Provinces;
            return View(address);
        }


        // [Update Address] - step 5 - It is for send data to Model and next it would save in database
        // POST: AddressController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                string Neighborhood = collection["Neighborhood"][0];
                int StreetNumber = Convert.ToInt32(collection["StreetNumber"][0]);
                string StreetName = collection["StreetName"][0];
                string City = collection["City"][0];
                int Province = Convert.ToInt32(collection["Province"][0]);
                string Country = collection["Country"][0];
                string PostalCode = collection["PostalCode"][0];
                int SuiteNumber = Convert.ToInt32(collection["SuiteNumber"][0]);
                Address address = new Address(Neighborhood, StreetNumber, StreetName, City, Province, Country, PostalCode, SuiteNumber);
                address.AddressId = id;
                _address.UpdateAddress(address);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        // [DeleteAddress step 2]
        // GET: AddressController/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {  
                _address.DeleteAddress(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index)); //this return to the list.
            }
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
