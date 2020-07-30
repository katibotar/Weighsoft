using System.ComponentModel.DataAnnotations;

namespace Weighsoft.Validation
{
    public class CustomLocalAuthority : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            var model = context.ObjectInstance as Models.UserDetailModel;

            if (model.isOnRoad && model.SelectedLocalAuthority == "Please Select")
            {
                return new ValidationResult(ErrorMessage);
            }

            return null;
        }
    }
}