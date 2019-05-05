using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestJob.Model;

namespace TestJobTest.Model
{
    [TestClass]
    public class ProfileTest
    {
        // Модель должна иметь хотя бы одно публичное свойство с атрибутом Display
        // Можно добавить проверки: что 1. Последовательный порядок полей 2. Порядок полей !=0
        [TestMethod]
        public void CheckProperties()
        {
            var props = typeof(Profile).GetProperties();
            var result = props.Where(pi => pi.GetCustomAttributes(typeof(DisplayAttribute), true).Any()).Any();
            Assert.IsTrue(result);
        }



        [TestMethod]
        public void HashCodeAndEqual()
        {
            var obj1 = MokProfile.GetSomeProfile();
            var obj2 = MokProfile.GetSomeProfile();

            // Хеш код и Equal  
            Assert.IsTrue(obj1.GetHashCode() == obj2.GetHashCode());
            Assert.IsTrue(obj1.Equals(obj2));
        }


        /// <summary>
        /// Проверка на вилидацию модели
        /// </summary>
        /// <param name="property"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        private bool ValidatePropertyValue(string propertyName, object sValue)
        {
            var mokProfile = MokProfile.GetSomeProfile();

            var vResults = new List<ValidationResult>();
            var context = new ValidationContext(mokProfile) { MemberName = propertyName };

            var result = Validator.TryValidateProperty(  sValue, context, vResults); ;
            return result;
        }


        [TestMethod]
        public void ValidateFIO()
        {
            var property = typeof(Profile).GetProperty(nameof(Profile.FIO));
            var s = "Василий Пупкин"; 
            Assert.IsTrue(ValidatePropertyValue(property.Name, s));
              s = "Иван 4 Грозный  ";
            Assert.IsTrue(ValidatePropertyValue(property.Name, s));
            s = "Тут вообще какие-то спец. символы !";
            Assert.IsFalse(ValidatePropertyValue(property.Name, s));
        }

        [TestMethod]
        public void ValidateExperience()
        {
            var propertyName = nameof(Profile.Experience);
            int  s;
            s = 1 ; 
            Assert.IsTrue(ValidatePropertyValue(propertyName, s));

            s = -1 ;
            Assert.IsFalse(ValidatePropertyValue(propertyName, s));

            s = 101 ;
            Assert.IsFalse(ValidatePropertyValue(propertyName, s));
        }

        [TestMethod]
        public void ValidateLanguage()
        {
            var propertyName = nameof(Profile.lang);
            string s;
            s = "PHP";
            Assert.IsTrue(ValidatePropertyValue(propertyName, s));

            s = "Русский";
            Assert.IsFalse(ValidatePropertyValue(propertyName, s));

            s = "1C";
            Assert.IsFalse(ValidatePropertyValue(propertyName, s));
        }

        [TestMethod]
        public void ValidatePhone()
        {
            var propertyName = nameof(Profile.Phone);
            string s;
            s = "+7 912 890 12 34";
            Assert.IsTrue(ValidatePropertyValue(propertyName, s));

            s = " 7834 55";
            Assert.IsTrue(ValidatePropertyValue(propertyName, s));

            s = "8 (495) 123 456 7";
            Assert.IsTrue(ValidatePropertyValue(propertyName, s));

            s = "8 *(495) % 123 456 7";
            Assert.IsFalse(ValidatePropertyValue(propertyName, s));
        }
    }
}
