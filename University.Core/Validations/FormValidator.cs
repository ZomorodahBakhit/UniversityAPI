using System.ComponentModel.DataAnnotations;

namespace University.Core.Validations
{
    public class FormValidator
    {


        public static FormValidationResults Validate(object form)
        {

            var context = new ValidationContext(form, serviceProvider:null, items:null);
            var results = new List<ValidationResult>();


            var  isValid = Validator.TryValidateObject(form, context, results, true);

            return new FormValidationResults(isValid, results);
            
        }


    }
}
