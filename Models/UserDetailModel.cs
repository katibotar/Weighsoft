using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Weighsoft.Validation;

namespace Weighsoft.Models
{
    public class UserDetailModel
    {
        [Required(ErrorMessage = "Please enter a name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter an email address")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "The email address is not valid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter a mobile number")]
        public string Mobile { get; set; }
        
        public string Postcode { get; set; }

        public string Address { get; set; }

        [CustomDeliveryDate(ErrorMessage = "The delivery date must be at least 2 days in advance")]       
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Please select a date")]
        public DateTime? DeliveryDate { get; set; }
        public List<ProductModel> Products { get; set; }

        [Required(ErrorMessage = "Please select a product")]
        public string SelectedProduct { get; set; }
       
        public List<WasteTypeModel> WasteTypes { get; set; }

        [Required(ErrorMessage = "Please select a waste type")]
        public string SelectedWasteType { get; set; }

        public UserDetailModel()
        {
            Products = new List<ProductModel>();
            WasteTypes = new List<WasteTypeModel>();
            LocalAuthorities = new List<LocalAuthorityModel>();
        }

        [Display(Name = "Will skip be on the road?")]
        public bool isOnRoad { get; set; }

        public List<LocalAuthorityModel> LocalAuthorities { get; set; }

        [CustomLocalAuthority(ErrorMessage = "Please select a local authority")]
        public string SelectedLocalAuthority { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public double ExVat { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public double Vat { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public double Total { get; set; }
    }
}