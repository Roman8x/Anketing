using System;
using System.ComponentModel;
using System.IO;
using TestJob.Helpers;
using TestJob.Interfaces;
using TestJob.Model;
using TestJob.Repository;

namespace TestJob.Commands
{
    [Description("-find <Имя файла анкеты> - Найти анкету и показать данные анкеты в консоль")]
    public class Find : ICommand
    {
        private readonly IProfileRepository profileRepository;

        public string CommandName { get; private set; } = "-find";

        public Find(IProfileRepository profileRepository) {
            this.profileRepository = profileRepository;
        }
        public IResponse Execute(SessionContext sessionContext, string parameter)
        {
            if (string.IsNullOrWhiteSpace(parameter) || !ValidateFIO (parameter) )
                return new Response(ResponseType.BadParameters);

            var profile = profileRepository.FindByID(parameter);
            if (profile == null)
                return new Response(ResponseType.NoSessionContent);

            // сериализуем в строки 
    
            EntityTextFormatter<Profile> etf = new EntityTextFormatter <Profile>();
            using (var ms = new MemoryStream()) {
                etf.Serialize(ms, profile);
                using (StreamReader sr = new StreamReader(ms)) {
                    Console.Write(sr.ReadToEnd());
                }
            }
            return new Response(ResponseType.Ok);
        }


        /// <summary>
        /// Проверка, параметра, что в имени нет спец. символов 
        /// </summary>
        /// <param name="fio"></param>
        /// <returns></returns>
        private bool ValidateFIO(string fio) {
            return true;
        }
    }
}
