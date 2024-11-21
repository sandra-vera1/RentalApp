
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentalApp.Models;
using RentalApp.ViewModels;
//using RentalApp.Services.PropertyServices;
using RentalApp.Services.PropertyService;



namespace RentalApp.Controllers
{
    public class PropertyController : Controller
    {



        private readonly string _connectionString;
        private readonly IPropertyService _propertyService;

		public PropertyController(IOptions<ConnectionStringOptions> options,
			IPropertyService propertyService)
        {
            _connectionString = options.Value.Connection;
			_propertyService = propertyService;
        }



        // GET: PropertyController
        public ActionResult Index()
        {
            try
            {
                var Model = _propertyService.Get(_connectionString);
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


                //double sqFtMin = Convert.ToDouble(collection["SqFtMin"][0]);
                //Remember to check if the value is a double before the convert or use the: double.TryParse(type, out double value);
                double priceMin;
                double priceMax;
                double sqFtMin;
                double sqFtMax;

                string searchAll = collection["SearchAll"][0];
                
                double.TryParse(collection["PriceMin"][0], out priceMin);
                priceMin = priceMin > 0 ? priceMin : double.MinValue;
                double.TryParse(collection["PriceMax"][0], out priceMax);
                priceMax = priceMax > 0 ? priceMax : double.MaxValue;


                double.TryParse(collection["sqFtMin"][0], out sqFtMin);
                sqFtMin = sqFtMin > 0 ? sqFtMin : double.MinValue;
                double.TryParse(collection["SqFtMax"][0], out sqFtMax);
                sqFtMax = sqFtMax > 0 ? sqFtMax : double.MaxValue;

                string neighborhood = collection["Neighborhood"][0];
                string type = collection["Type"][0];
                string testAvail = collection["Availability"][0];
                bool? availability = collection["Availability"][0] == "Availability" ? true : false;
                //Convert.ToBoolean(collection["Availability"][0]);
                //bool availability = collection["Availability"][0];





                //double.TryParse(collection["PriceMin"][0], out priceMin);
                //double.TryParse(collection["PriceMax"][0], out priceMax);

                //double.TryParse(collection["sqFtMin"][0], out sqFtMin);
                //double.TryParse(collection["SqFtMax"][0], out sqFtMax);

                //double priceMin = Convert.ToDouble(collection["PriceMin"][0])
                //double priceMax = Convert.ToDouble(collection["PriceMax"][0]);
                //double sqFtMin = Convert.ToDouble(collection["sqFtMin"][0]);
                //double sqFtMax = Convert.ToDouble(collection["SqFtMax"][0]);

                // List < Property> Model = _property.ListProperty();
                //var FilteredModel = Model.Where(prop => prop.Price > priceMin)
                //    .Where(prop => prop.Price < priceMax)
                //    .Where(prop => prop.SquareFootage > sqFtMin)
                //    .Where(prop => prop.SquareFootage < sqFtMax)
                //    .Where(prop => prop.Type.ToLower() == type.ToLower() || type == "All")
                //    .Where(prop => prop.Address.Neighborhood.Contains(neighborhood));
                // Model.Contains(prop => prop.Price > minPrice);

                // return View("./Index", FilteredModel);

                return View("Index");
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
                
                return View();
                // **********************************************************************************************
                // FILTER MODEL WITH LOGGED IN USER ID?? 
                // **********************************************************************************************


                //List<Property> Model = _property.ListProperty();
                //return View(Model);
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
        public IActionResult Create(Property model)
        {
            try
            {

                Property property = new Property(
                    model.AddressId, 
                    model.SquareFootage, 
                    model.Facilities, 
                    model.TermId, 
                    model.Type, 
                    model.Availability, 
                    model.Price, 
                    1 // manually making owner ID 1 right now until we have session management
                    );
				_propertyService.Create(_connectionString, property);

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
