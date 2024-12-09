
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
                PropertySearchViewModel propertiesAndSearch = new PropertySearchViewModel();

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
            PropertyOwnerListView propertyDetails = new PropertyOwnerListView();

            propertyDetails.Property = _propertyService.GetPropertyById(_connectionString, propertyId);

            propertyDetails.Owner = _userServices.GetUserById(_connectionString, propertyDetails.Property.OwnerId);
            propertyDetails.Property.Address = _addressService.GetAddressOfProperty(_connectionString, propertyDetails.Property.PropertyId);
            propertyDetails.Term = _propertyService.GetTermNameById(_connectionString, propertyDetails.Property.TermId);


            return View(propertyDetails);
        }

        // ************************ TEST *****************************

        // POST: PropertyController/Search
        [HttpPost]
        public IActionResult Search(PropertySearchViewModel filter)
        {
            try
            {

				PropertySearchViewModel propertiesAndSearch = new PropertySearchViewModel();

				propertiesAndSearch.PropertyList = _propertyService.Get(_connectionString);

                propertiesAndSearch.PropertyList = propertiesAndSearch.PropertyList.Where(prop => prop.Property.Price > filter.PriceMin)
                    .Where(prop => prop.Property.Price < filter.PriceMax)
                    .Where(prop => prop.Property.SquareFootage > filter.SqFtMin)
                    .Where(prop => prop.Property.SquareFootage < filter.SqFtMax)
                    .Where(prop => prop.Property.Type.ToLower() == filter.Type.ToLower() || filter.Type == "All");
                if(filter.SearchAll != null)
                {
                    propertiesAndSearch.PropertyList = propertiesAndSearch.PropertyList
                        .Where(prop => prop.Property.Address.StreetName.ToLower().Contains(filter.SearchAll.ToLower()));
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


                 //Model.Contains(prop => prop.Property.Price > filter.MinPrice);

                // return View("./Index", FilteredModel);

                return View("Index", propertiesAndSearch);
            }
            catch
            {
                return View("../Home/Index");
            }
            
        }


        // *****************************************************


        // GET: My Properties

        public ActionResult MyProperties()
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.First(c => c.Type == "UserId").Value);//UserId from the session
                // **********************************************************************************************
                // FILTER MODEL WITH LOGGED IN USER ID?? 
                // **********************************************************************************************
                
                //PropertySearchViewModel propertiesAndSearch = new PropertySearchViewModel();
                // List<PropertyOwnerListView> propertyDetails = new List<PropertyOwnerListView>();

                var propertyDetails = _propertyService.Get(_connectionString);
                propertyDetails = propertyDetails.Where(p => p.Property.OwnerId == UserId);

                //propertiesAndSearch.PropertyList = _propertyService.Get(_connectionString);
                //propertiesAndSearch.PropertyList = propertiesAndSearch.PropertyList.Where(p => p.Property.OwnerId == UserId);

                //List<Property> properties = new List<Property>();
                //foreach (var property in propertyDetails)
                //{
                //    properties.Add(property.Property);
                //}
                return View(propertyDetails);
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
            try
            {
                
                int UserId = Convert.ToInt32(User.Claims.First(c => c.Type == "UserId").Value);
                

                CreatePropertyViewModel property = new CreatePropertyViewModel();
                property.TermList = _propertyService.GetTerms(_connectionString);
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
				_propertyService.Create(_connectionString, property);

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

        // GET: PropertyController/Edit/5

        public ActionResult Edit(int id)
        {

            try
            {
                int UserId = Convert.ToInt32(User.Claims.First(c => c.Type == "UserId").Value);
                // retrieve property data
                Property property = _propertyService.GetPropertyById(_connectionString, id);
                CreatePropertyViewModel editView = new CreatePropertyViewModel();

                editView.TermList = _propertyService.GetTerms(_connectionString);
                editView.AddressList = _addressService.GetAddressesOfUser(_connectionString, UserId);

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


        //public ActionResult Edit(int id)
        //{
        //    try
        //    {
        //        // retrieve property data
        //        Property property = _propertyService.GetPropertyById(_connectionString, id);

        //        // send property object to view - this allows us to pre-populate the form with current property info
        //        return View(property);
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // POST: PropertyController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        // int id, IFormCollection collection
        public ActionResult Edit(CreatePropertyViewModel model)
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


                _propertyService.Edit(_connectionString, property, model.PropertyId);

                return RedirectToAction(nameof(MyProperties));
            }
            catch
            {
                int UserId = Convert.ToInt32(User.Claims.First(c => c.Type == "UserId").Value);

                model.TermList = _propertyService.GetTerms(_connectionString);
                model.AddressList = _addressService.GetAddressesOfUser(_connectionString, UserId);

                return View(model);


                //return View("Edit");
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



        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}




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


