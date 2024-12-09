using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalApp.Models;
using RentalApp.Services.AddressServices;
using RentalApp.ViewModels;

namespace RentalApp.Controllers
{
    [Authorize(Policy = "OwnerOnly")]
    public class AddressController : Controller
    {
        //create connection with Model)//step 3
        private readonly string _connectionString;
        private readonly IAddressService _addressService;
        public AddressController(IOptions<ConnectionStringOptions> options, //This parameter is for get the connection string
            IAddressService addressService) 
        {
            // Retrieve the connection string from the options
            _connectionString = options.Value.Connection;
            _addressService = addressService;
		}

        // [Address List step 2]
        // GET: AddressController
        public ActionResult Index()
		{
			int UserId = Convert.ToInt32(User.Claims.First(c => c.Type == "UserId").Value);//UserId from the session
			IEnumerable<AddressViewModel> Model = _addressService.ListAddress(_connectionString);
			Model = Model.Where(a => a.address.UserId == UserId);
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
            Address address = new Address(_addressService.ListProvinces(_connectionString));
            return View(address);
        }

        // POST: AddressController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {  //This is the bridge between the View and Controller. In other words this cast the object that is in the interface.
			   //int AddressId it is not necessary because the ID field (DB) is type of Identity

				int UserId = Convert.ToInt32(User.Claims.First(c => c.Type == "UserId").Value);//UserId from the session
				string Neighborhood = collection.ToList()[0].Value;
                int StreetNumber = Convert.ToInt32(collection.ToList()[1].Value);
                string StreetName = collection.ToList()[3].Value;
                string City = collection.ToList()[4].Value;
                int Province = Convert.ToInt32(collection.ToList()[5].Value);
                string Country = collection.ToList()[6].Value;
                string PostalCode = collection.ToList()[7].Value;
                int SuiteNumber = Convert.ToInt32(collection.ToList()[8].Value);
				Address address = new Address(Neighborhood, StreetNumber, StreetName, City, Province, Country, PostalCode, SuiteNumber)
				{
					UserId = UserId
				};
				_addressService.CreateAddress(_connectionString, address); //This call the create method with the bridge that is in line 18
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
            // retrieve addresses from DB
            IEnumerable<AddressViewModel> addresses = _addressService.ListAddress(_connectionString);
			AddressViewModel address = addresses.First(a => a.address.AddressId == id);
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
                string Neighborhood = collection["address.Neighborhood"][0];
                int StreetNumber = Convert.ToInt32(collection["address.StreetNumber"][0]);
                string StreetName = collection["address.StreetName"][0];
                string City = collection["address.City"][0];
                int Province = Convert.ToInt32(collection["address.Province"][0]);
                string Country = collection["address.Country"][0];
                string PostalCode = collection["address.PostalCode"][0];
                int SuiteNumber = Convert.ToInt32(collection["address.SuiteNumber"][0]);
                Address address = new Address(Neighborhood, StreetNumber, StreetName, City, Province, Country, PostalCode, SuiteNumber);
                address.AddressId = id;
                // send new information to the DB method to update details
                _addressService.UpdateAddress(_connectionString, address);
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
                _addressService.DeleteAddress(_connectionString, id);
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
