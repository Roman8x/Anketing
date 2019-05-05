using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TestJob.Model.ValidationSttributes
{
/// <summary>
/// Проверяет на разрешенные языки программирования 
/// </summary>
    public    class AssertLanguagesAttribute: ValidationAttribute
    {
        public static readonly ISet<string> AssertLanguages = new HashSet<string>() { "PHP", "JavaScript", "C", "C++", "Java", "C#", "Python", "Ruby" };
        public override bool IsValid(object value)
        {
            return AssertLanguages.Contains(value?.ToString()); 
        }  
    }
}
