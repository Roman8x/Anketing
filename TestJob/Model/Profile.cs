using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TestJob.Model.ValidationSttributes;

namespace TestJob.Model
{
    [Serializable]
    public class Profile : Entity
    {
        [DataType(DataType.Text)]
        [Display(Name = "ФИО" , Order =1)]
        [Required]
        [RegularExpression (@"^[а-яА-ЯёЁa-zA-Z0-9\s]+$")]   //  только Русские или английские буквы и цифры 
        public string FIO  { get; set; }

        [DataType(DataType.Date)]
        //[RegularExpression("(0[1-9]|1[0-9]|2[0-9]|3[01]).(0[1-9]|1[012]).[0-9]{4}")]
        [Display(Name = "Дата рождения", Order =2 )] // (Формат ДД.ММ.ГГГГ)   ДД.ММ.ГГГГ     
        [FullAgeAttribute (ErrorMessage = "Возраст должен быть более 14 и менее 100 лет")]
        [Required]           
        public DateTime DateBirth { get; set; }

        [AssertLanguages]// (Можно ввести только указанные варианты, иначе ошибка: PHP, JavaScript, C, C++, Java, C#, Python, Ruby)        
        [UIHint ("Collection")]
        [Display(Name = "Любимый язык программирования", Order = 3 )]  
        public string lang { get; set; }

        [Display(Name = "Опыт программирования на указанном языке", Order = 4)] //  (Полных лет)              
        [Required]
        [Range(0,100)]
        public int Experience { get; set; }

        [RegularExpression(@"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$", ErrorMessage = "Телефон может содержат цифры, пробел и тире и начаниться с +7, 8)")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Мобильный телефон", Order = 5)]
        [Required]
        public string Phone { get; set; }

        /// <summary>
        /// Дата создания 
        /// </summary>
        public DateTime DateCreated { get; set; } = DateTime.Now.Date;

        // Переопределим, что бы корректно работало добавление в коллекции  
        public override int GetHashCode()
        {            
            return Tuple.Create(FIO , lang , DateBirth, Experience, Phone, DateCreated).GetHashCode();
        }

        public override bool Equals(object obj)
        {           
            if (obj == null || GetType() != obj.GetType()) return false;
            Profile p = (Profile)obj;


            return
              Equals(this.Experience,  p.Experience) &&
              Equals(this.DateBirth,   p.DateBirth) &&
              Equals(this.DateCreated, p.DateCreated) &&
              Equals(this.FIO,         p.FIO) &&
              Equals(this.lang,        p.lang) &&
              Equals(this.Phone,       p.Phone);              
        }
    }
}

/*
 В анкете 5 вопросов:
ФИО
Дата рождения (Формат ДД.ММ.ГГГГ)
Любимый язык программирования (Можно ввести только указанные варианты, иначе ошибка: PHP, JavaScript, C, C++, Java, C#, Python, Ruby)
Опыт программирования на указанном языке (Полных лет)
Мобильный телефон

*/
