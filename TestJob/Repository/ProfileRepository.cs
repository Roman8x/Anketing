using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using TestJob.Helpers;
using TestJob.Repository;

namespace TestJob.Model
{
    public class ProfileRepository : IProfileRepository
    {
        /// <summary>
        /// Расширение по умолчанию
        /// </summary>
        public string DefaultExtension { get; set; } = ".txt";
        private readonly string PATH_TO_SAVE;

        private readonly EntityTextFormatter<Profile> entityTextFormatter;
        public ProfileRepository(Configuration path, EntityTextFormatter<Profile> entityTextFormatter)
        {
            this.PATH_TO_SAVE = path.WorkingDirectory;
            this.entityTextFormatter = entityTextFormatter;
        }

        /// <summary>
        /// Удалить файл 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(string id)
        {
            var fileName = Path.Combine(PATH_TO_SAVE, id);
            if (!(File.Exists(fileName)))
            {
                Console.WriteLine($"Файл не найден {fileName}");
                return false;
            }
            
            File.Delete(fileName);
            Console.WriteLine($"Файл удален {fileName}");
            return true;
        }

 
        public void SaveOrUpdate(Profile profile)
        {
            var fileName = Path.Combine(PATH_TO_SAVE, profile.FIO + DefaultExtension);
            using (var fs = new StreamWriter(fileName, false, Encoding.UTF8))
            {
                EntityTextFormatter<Profile> textFormatter = new EntityTextFormatter<Profile>();
                textFormatter.Serialize(fs.BaseStream, profile);
            }
        }
 

        public IEnumerable <string> TodayId()
        {
            var result = GetAll().Where(profile => profile.DateCreated.Date == DateTime.Now.Date).Select(p => p.FIO + DefaultExtension);
            return result;
        }

        public IEnumerable<string> GetAllId()
        {            
            var files = Directory.GetFiles(PATH_TO_SAVE, "*" + DefaultExtension );
            foreach (var file in files)
                yield return  file.Substring(file.LastIndexOf(@"\")+1);         
        }

        public Profile FindByID(string id)
        {
            var fileName = Path.Combine(PATH_TO_SAVE, id);
            if (!(File.Exists(fileName)))
                return null;
        
            return ReadFileProfile(fileName);
        }

        private Profile ReadFileProfile(string fileName) {
            using (var fr = new StreamReader(fileName, Encoding.UTF8))
            {
                EntityTextFormatter<Profile> textFormatter = new EntityTextFormatter<Profile>();
                return textFormatter.Deserialize(fr.BaseStream) as Profile;
            }
        }

        public IList<Profile> GetAll()
        {
            var files = Directory.GetFiles(PATH_TO_SAVE, "*"+DefaultExtension);
            var result = new List<Profile>();

            foreach (var fileName in  files){
                result.Add(ReadFileProfile(fileName));
            }
            return result;
        }
    }
}


/*
1. ФИО
2. Дата рождения (Формат ДД.ММ.ГГГГ)
3. Любимый язык программирования (Можно ввести только указанные варианты, иначе ошибка: PHP, JavaScript, C, C++, Java, C#, Python, Ruby)
4. Опыт программирования на указанном языке (Полных лет)
5. Мобильный телефон

     */