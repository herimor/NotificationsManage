using System;
using System.ComponentModel.DataAnnotations;

namespace eNotification.Extensions
{
    public class DateRangeValidation : ValidationAttribute
    {
        public int AddMonthCount{ get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if ((DateTime)value >= DateTime.Now && (DateTime)value <= DateTime.Now.AddMonths(AddMonthCount))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Укажите корректную дату");
            }   
        }
    }
}
