
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentalApp.Models;

namespace RentalApp.Controllers
{
    public class PropertyController : Controller
    {

        private readonly Property _property;

        public PropertyController(IOptions<ConnectionStringOptions> options)
        {
            string _connectionString = options.Value.Connection;
            _property = new Property(_connectionString);
        }



        // GET: PropertyController
        public ActionResult Index()
        {
            try
            {
                List<Property> Model = _property.ListProperty();
                return View(Model);
            }
            catch
            {
                // Redirect to home page on error right now
                return View("../Home/Index");
            }
        }

        // GET: PropertyController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // ************************ TEST *****************************

        // POST: PropertyController/Search
        [HttpPost]
        public ActionResult Search(IFormCollection collection)
        {
            try
            {

                string type = collection["Type"][0];
				//double sqFtMin = Convert.ToDouble(collection["SqFtMin"][0]);
                //Remember to check if the value is a double before the convert or use the: double.TryParse(type, out double value);
				double sqFtMin;
                double.TryParse(collection["SqFtMin"][0], out sqFtMin);
				
				double sqFtMax = Convert.ToDouble(collection["SqFtMax"][0]);
                double priceMin = Convert.ToDouble(collection["PriceMin"][0]);
                double priceMax = Convert.ToDouble(collection["PriceMax"][0]);


                List<Property> Model = _property.ListProperty();
                // Model.Contains(prop => prop.Price > minPrice);

                return View("./Index", Model);
            }
            catch
            {
                return View("Index");
            }
            
        }


        // *****************************************************


        // GET: My Properties

        public ActionResult MyProperties()
        {
            try
            {
                List<Property> Model = _property.ListProperty();

                // **********************************************************************************************
                // FILTER MODEL WITH LOGGED IN USER ID?? 
                // **********************************************************************************************

                return View(Model);
            }
            catch
            {
                // Redirect to home page on error right now
                return View();
            }
        }

        // GET: PropertyController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PropertyController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                int addressId = Convert.ToInt32(collection["AddressId"][0]);
                double sqFt = Convert.ToDouble(collection["SquareFootage"][0]);
                int termId = Convert.ToInt32(collection["TermId"][0]);
                string type = collection["Type"][0];
                string facilities = collection["Facilities"][0];
                bool availability = Convert.ToBoolean(collection["Availability"][0]);

                int price = Convert.ToInt32(collection["Price"][0]);
                // probably parse owner ID from log in?
                // int ownerId = Convert.ToInt32(collection.ToList()[0].Value);
                int ownerId = 1; // using a default for now
               
        
                Property property = new Property(addressId, sqFt, facilities, termId, type, availability, price, ownerId);
                _property.CreateProperty(property);

                return RedirectToAction(nameof(Create));
            }
            catch
            {
                return View("Create");
            }
        }

        // GET: PropertyController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PropertyController/Edit/5
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

        // GET: PropertyController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PropertyController/Delete/5
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
