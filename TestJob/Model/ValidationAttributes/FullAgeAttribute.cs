using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TestJob.Model.ValidationSttributes
{
    /// <summary>
    /// Проверка на возраст: больше 14, но меньше 100 лет
    /// </summary>
    public class FullAgeAttribute :ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            value = (DateTime)value;
 
            if (DateTime.Now.AddYears(-14).CompareTo(value) >= 0 && DateTime.Now.AddYears(-100).CompareTo(value) <= 0)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Возраст программиста может быть  больше 14, но меньше 100 лет");
            }
        }
    }
}