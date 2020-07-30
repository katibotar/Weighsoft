using System;
using System.ComponentModel.DataAnnotations;

namespace Weighsoft.Validation
{
    public class CustomDeliveryDate : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dateTime = Convert.ToDateTime(value);
            return dateTime > DateTime.Now.AddDays(+2);           
        }
    }
}