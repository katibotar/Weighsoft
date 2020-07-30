using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Weighsoft.Models;
using Weighsoft.Services;

namespace Weighsoft.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAPIService _apiService;

        #region constructor

        public HomeController()
        {
        }

        public HomeController(IAPIService apiService)
        {
            this._apiService = apiService;
        }

        #endregion
        [HttpGet]
        public ActionResult Index()          
        {
            UserDetailModel userDetailModel; 

            if ((UserDetailModel)TempData["datacontainer"] == null)
            {
                userDetailModel = new UserDetailModel();
                userDetailModel.Address = "1 Acacia Avenue, Horsham, West Sussex, RH12 1AS";
            }
            else
            {
                userDetailModel = (UserDetailModel)TempData["datacontainer"]; 
            }

            TempData.Keep("datacontainer");
            return View(userDetailModel);
        }        

        [HttpGet]
        public ActionResult Product()
        {
            UserDetailModel userDetailModel = (UserDetailModel)TempData["datacontainer"];

            TempData.Keep("datacontainer");

            if (userDetailModel.Products.Count == 0)
            {
                List<ProductModel> products = new List<ProductModel>();
                products = this._apiService.GetProducts().ToList();
                userDetailModel.Products.AddRange(products);
            }

            if (userDetailModel.WasteTypes.Count == 0)
            {
                List<WasteTypeModel> wasteTypes = new List<WasteTypeModel>();
                wasteTypes = this._apiService.GetWasteTypes().ToList();
                userDetailModel.WasteTypes.AddRange(wasteTypes);
            }

            if (userDetailModel.LocalAuthorities.Count == 0)
            {
                //hardcode some values for the Local Authority dropdownlist   
                List<LocalAuthorityModel> localAuthorities = new List<LocalAuthorityModel> {
                    new LocalAuthorityModel { Value = "Please Select", Description = "Please Select"},
                    new LocalAuthorityModel { Value = "Local Authority A", Description = "Local Authority A, 3 days £9.50"},
                    new LocalAuthorityModel { Value = "Local Authority B", Description = "Local Authority B, 7 days £15.50" },
                    new LocalAuthorityModel { Value = "Local Authority C", Description = "Local Authority C, 14 days £28.59" }
                };
                userDetailModel.LocalAuthorities.AddRange(localAuthorities);
            }
            
            return View(userDetailModel);
        }

        [HttpPost]
        public ActionResult Product(UserDetailModel userDetailModel)
        {
            if (ModelState.IsValidField("Name") && ModelState.IsValidField("Email") &&
                ModelState.IsValidField("Mobile") && ModelState.IsValidField("DeliveryDate"))
            {
                userDetailModel.Address = "1 Acacia Avenue, Horsham, West Sussex, RH12 1AS";

                TempData["datacontainer"] = userDetailModel;

                List<ProductModel> products = new List<ProductModel>();

                products = this._apiService.GetProducts().ToList();
                userDetailModel.Products.AddRange(products);                          

                List<WasteTypeModel> wasteTypes = new List<WasteTypeModel>();

                wasteTypes = this._apiService.GetWasteTypes().ToList();
                userDetailModel.WasteTypes.AddRange(wasteTypes);

                //hardcode some values for the Local Authority dropdownlist   
                List<LocalAuthorityModel> localAuthorities = new List<LocalAuthorityModel> {
                    new LocalAuthorityModel { Value = "Please Select", Description = "Please Select"},
                    new LocalAuthorityModel { Value = "Local Authority A", Description = "Local Authority A, 3 days £9.50"},
                    new LocalAuthorityModel { Value = "Local Authority B", Description = "Local Authority B, 7 days £15.50" },
                    new LocalAuthorityModel { Value = "Local Authority C", Description = "Local Authority C, 14 days £28.59" }
                };
                userDetailModel.LocalAuthorities.AddRange(localAuthorities);

                ModelState.Clear();

                return View(userDetailModel);
            }         

            return View("Index", userDetailModel);
        }

        [HttpGet]
        public ActionResult Quotation()
        {
            UserDetailModel userDetailModel = (UserDetailModel)TempData["datacontainer"];
            TempData.Keep("datacontainer");
            return View(userDetailModel);
        }

        [HttpPost]
        public ActionResult Quotation(UserDetailModel userDetailModel)
        {   
            if (ModelState.IsValidField("SelectedProduct") && ModelState.IsValidField("SelectedWasteType"))
            {
                userDetailModel.Address = "1 Acacia Avenue, Horsham, West Sussex, RH12 1AS";
                TempData["datacontainer"] = userDetailModel;

                double exVat = this._apiService.GetPrice();
                double vat = exVat * 0.2;
                double total = exVat + vat;

                userDetailModel.ExVat = exVat;
                userDetailModel.Vat = vat;
                userDetailModel.Total = total;

                ModelState.Clear();

                return View(userDetailModel);              
            }

            return View("Product", userDetailModel);
        }

        [HttpGet]
        public ActionResult Order()
        {
            UserDetailModel userDetailModel = (UserDetailModel)TempData["datacontainer"];
            TempData.Keep("datacontainer");
            return View(userDetailModel);
        }

        [HttpPost]
        public ActionResult Order(UserDetailModel userDetailModel)
        {
            userDetailModel.Address = "1 Acacia Avenue, Horsham, West Sussex, RH12 1AS";
            TempData["datacontainer"] = userDetailModel;

            return View(userDetailModel);          
        }

        public ActionResult Privacy()
        {           
            return View();
        }

        public ActionResult Terms()
        {
            return View();
        }
    }
}