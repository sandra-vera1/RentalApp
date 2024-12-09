
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.Extensions.Options;
using RentalApp.Models;
using RentalApp.Services.AddressServices;
using RentalApp.Services.PropertyService;
using RentalApp.Services.QuoteServices;
using RentalApp.Services.UserServices;
using RentalApp.ViewModels;
using System.Reflection;
using System.Text.Json;



namespace RentalApp.Controllers
{
    public class PropertyController : Controller
    {

        private readonly string _connectionString;
		// https://www.youtube.com/watch?v=Wiy54682d1w
		// https://www.c-sharpcorner.com/article/implement-and-register-dependency-injection-in-asp-net-core-net-6/
		// used these to learn about repository pattern and services

		// add all our service classes with methods that are used to connect to the DB
		private readonly IPropertyService _propertyService;
        private readonly IAddressService _addressService;
        private readonly IUserService _userServices;


		public PropertyController(IOptions<ConnectionStringOptions> options,
			IPropertyService propertyService, IAddressService addressService, IUserService userService)
        {
            _connectionString = options.Value.Connection;
			_propertyService = propertyService;
            _addressService = addressService;
            _userServices = userService;
        }



        // GET: PropertyController
        public ActionResult Index()
        {
            try
            {
                // this search view model contains a list of properties to display
                // and property fields used by ASP to collect form input upon user search
                // this simplifies the data transfer from UI to controller
                PropertySearchViewModel propertiesAndSearch = new PropertySearchViewModel();

                // collect all properties in the DB
				propertiesAndSearch.PropertyList = _propertyService.Get(_connectionString);
                return View(propertiesAndSearch);
            }
            catch
            {
                // Redirect to home page on error right now
                return View("../Home/Index");
            }
        }

        // GET: PropertyController/Details/5
        public ActionResult Details(int propertyId)
        {
            // property details class includes an owner object, property object and Term information
            // this is used to display all data relevant to an end user instead of ID's 
            PropertyOwnerListView propertyDetails = new PropertyOwnerListView();

            // get property based on ID passed from the view
            propertyDetails.Property = _propertyService.GetPropertyById(_connectionString, propertyId);
            
            // use OwnerID to retrieve Owner details
            propertyDetails.Owner = _userServices.GetUserById(_connectionString, propertyDetails.Property.OwnerId);
            // use AddressID to retrieve Address details
            propertyDetails.Property.Address = _addressService.GetAddressOfProperty(_connectionString, propertyDetails.Property.AddressId);
            // get Term Name information from TermID
            propertyDetails.Term = _propertyService.GetTermNameById(_connectionString, propertyDetails.Property.TermId);

            // pass all values back to the view for displaying
            return View(propertyDetails);
        }

        // POST: PropertyController/Search
        [HttpPost]
        public IActionResult Search(PropertySearchViewModel filter)
        {
            try
            {

				PropertySearchViewModel propertiesAndSearch = new PropertySearchViewModel();

                // retrieve all properties in DB into the display list
				propertiesAndSearch.PropertyList = _propertyService.Get(_connectionString);

                // perform necessary filtering based on search values passed from view
                // if no value was provided for min or max prices / sq ft, the class is constructed with default infinite max / min values
                // this allows the search to have no limitation if non was provided
                propertiesAndSearch.PropertyList = propertiesAndSearch.PropertyList.Where(prop => prop.Property.Price >= filter.PriceMin)
                    .Where(prop => prop.Property.Price <= filter.PriceMax)
                    .Where(prop => prop.Property.SquareFootage >= filter.SqFtMin)
                    .Where(prop => prop.Property.SquareFootage <= filter.SqFtMax)
                    .Where(prop => prop.Property.Type.ToLower() == filter.Type.ToLower() || filter.Type == "All");
                if(filter.SearchAll != null)
                {
                    propertiesAndSearch.PropertyList = propertiesAndSearch.PropertyList
                        .Where(prop => prop.Property.Address.StreetNumber.ToString().ToLower().Contains(filter.SearchAll.ToLower()) || 
                            prop.Property.Address.StreetName.ToLower().Contains(filter.SearchAll.ToLower()) ||
						    prop.Property.Address.City.ToLower().Contains(filter.SearchAll.ToLower()) ||
							prop.Property.Address.ProvinceName.ToLower().Contains(filter.SearchAll.ToLower()) ||
							prop.Property.SquareFootage.ToString().ToLower().Contains(filter.SearchAll.ToLower()) ||
							prop.Property.Facilities.ToLower().Contains(filter.SearchAll.ToLower()) ||
							prop.Term.ToLower().Contains(filter.SearchAll.ToLower()) ||
							prop.Property.Type.ToLower().Contains(filter.SearchAll.ToLower()) ||
							prop.Property.Price.ToString().ToLower().Contains(filter.SearchAll.ToLower()) ||
							prop.Property.Address.Neighborhood.ToLower().Contains(filter.SearchAll.ToLower())
						);
				}
                if (filter.Neighborhood != null)
                {
					propertiesAndSearch.PropertyList = propertiesAndSearch.PropertyList
						.Where(prop => prop.Property.Address.Neighborhood.ToLower().Contains(filter.Neighborhood.ToLower()));
				}
                if(filter.Availability)
                {
                    propertiesAndSearch.PropertyList = propertiesAndSearch.PropertyList
                        .Where(prop => prop.Property.Availability == true);
                }


                return View("Index", propertiesAndSearch);
            }
            catch
            {
                return View("../Home/Index");
            }
            
        }

        // GET: My Properties

        public ActionResult MyProperties()
        {
            try
            {

                // get loggedin userID
                int UserId = Convert.ToInt32(User.Claims.First(c => c.Type == "UserId").Value);//UserId from the session

                var propertyDetails = _propertyService.Get(_connectionString);
                // filter properties based on current user
                propertyDetails = propertyDetails.Where(p => p.Property.OwnerId == UserId);

                return View(propertyDetails);
            }
            catch
            {
                return View();
            }
        }

        // GET: PropertyController/Create
        public ActionResult Create()
        {
            try
            {
                // retrieve userID of logged in user
                int UserId = Convert.ToInt32(User.Claims.First(c => c.Type == "UserId").Value);
                

                CreatePropertyViewModel property = new CreatePropertyViewModel();
                property.TermList = _propertyService.GetTerms(_connectionString);
                // use ID to retrieve users addresses list
                property.AddressList = _addressService.GetAddressesOfUser(_connectionString, UserId);



                return View(property);
            }
            catch
            {
                return View();
            }
            
        }

        // POST: PropertyController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreatePropertyViewModel model)
        {
            try
            {
				int UserId = Convert.ToInt32(User.Claims.First(c => c.Type == "UserId").Value);
                // create property based on data sent from the view
				Property property = new Property(
                    model.AddressId, 
                    model.SquareFootage, 
                    model.Facilities, 
                    model.TermId, 
                    model.Type, 
                    model.Availability, 
                    model.Price,
					UserId 
					);
                // send property object to DB function to insert new property
				_propertyService.Create(_connectionString, property);

                return RedirectToAction(nameof(MyProperties));
            }
            catch
            {
                // on error redisplay page with necessary details
                int UserId = Convert.ToInt32(User.Claims.First(c => c.Type == "UserId").Value);

                model.TermList = _propertyService.GetTerms(_connectionString);
                model.AddressList = _addressService.GetAddressesOfUser(_connectionString, UserId);

                return View(model);
            }
        }

        // GET: PropertyController/Edit/5

        public ActionResult Edit(int id)
        {

            try
            {
                int UserId = Convert.ToInt32(User.Claims.First(c => c.Type == "UserId").Value);
                // retrieve property data via id
                Property property = _propertyService.GetPropertyById(_connectionString, id);
                // here we can reused the createPropertyViewModel for editing since the values will be the same
                CreatePropertyViewModel editView = new CreatePropertyViewModel();

                editView.TermList = _propertyService.GetTerms(_connectionString);
                editView.AddressList = _addressService.GetAddressesOfUser(_connectionString, UserId);

                // however in this case we prepopulate the fields after retrieving property data 
                editView.PropertyId = property.PropertyId;
                editView.SquareFootage = property.SquareFootage;
                editView.Facilities = property.Facilities;
                editView.Type = property.Type;
                editView.Price = property.Price;
                editView.AddressId = property.AddressId;
                editView.TermId = property.TermId;
                editView.Availability = property.Availability;

                // send property object to view - this allows us to pre-populate the form with current property info
                return View(editView);
            }
            catch
            {
                return View();
            }

        }


        // POST: PropertyController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CreatePropertyViewModel model)
        {

            try
            {
				int UserId = Convert.ToInt32(User.Claims.First(c => c.Type == "UserId").Value);

				Property property = new Property(
                model.AddressId,
                model.SquareFootage,
                model.Facilities,
                model.TermId,
                model.Type,
                model.Availability,
                model.Price,
				UserId
				);


                _propertyService.Edit(_connectionString, property, model.PropertyId);

                return RedirectToAction(nameof(MyProperties));
            }
            catch
            {
                int UserId = Convert.ToInt32(User.Claims.First(c => c.Type == "UserId").Value);

                model.TermList = _propertyService.GetTerms(_connectionString);
                model.AddressList = _addressService.GetAddressesOfUser(_connectionString, UserId);

                return View(model);

            }
        }

    

// GET: PropertyController/Delete/5
public ActionResult Delete(int id)
        {
            try
            {
                _propertyService.Delete(_connectionString, id);
                return RedirectToAction(nameof(MyProperties));
            }
            catch
            {
                return RedirectToAction(nameof(MyProperties));
            }
            // return View();
        }

        // POST: PropertyController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(MyProperties));
            }
            catch
            {
                return View();
            }
        }


        // GET: PropertyController/Favorites
        [Authorize(Policy = "RenterOnly")]
		public ActionResult Favorites()
		{
			try
			{
				int UserId = Convert.ToInt32(User.Claims.First(c => c.Type == "UserId").Value);//UserId from the session
                IEnumerable <PropertyFavoriteListView> FavoriteList = _propertyService.GetFavoriteList(_connectionString, UserId);
				return View(FavoriteList);
			}
			catch
			{
				// Redirect to home page on error right now
				return View("../Home/Index");
			}
		}

        // GET: PropertyController/DeleteFavorite/5
        [Authorize(Policy = "RenterOnly")]
		public ActionResult DeleteFavorite(int id)
        {
            _propertyService.DeleteFavorite(_connectionString, id);
            return RedirectToAction(nameof(Favorites));
		}

        // GET: PropertyController/AddFavorite/5
        [Authorize(Policy = "RenterOnly")]
		public ActionResult AddFavorite(int id)
		{
			int UserId = Convert.ToInt32(User.Claims.First(c => c.Type == "UserId").Value);//UserId from the session
			_propertyService.CreateFavorite(_connectionString, id, UserId);
			return RedirectToAction(nameof(Index));
		}

        // GET: PropertyController/Quote/5
        [Authorize(Policy = "RenterOnly")]
		public ActionResult Quote(int id)
		{
            QuoteViewModel quoteViewModel = _propertyService.GetPropertyQuote(_connectionString, id);
            return View(quoteViewModel);
        }

        // Post: PropertyController/Quote
        [Authorize(Policy = "RenterOnly")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Quote(QuoteViewModel quoteViewModelInput)
        {
            int TermDuration = quoteViewModelInput.TermDuration;
            quoteViewModelInput = JsonSerializer.Deserialize<QuoteViewModel>(quoteViewModelInput.jsonObj);
            quoteViewModelInput.TermDuration = TermDuration;
			QuoteTask.RunTask(ref quoteViewModelInput);
            return View(quoteViewModelInput);
        }
    }
}


