using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using TestJob.Helpers;
using TestJob.Interfaces;
using TestJob.Model;

namespace TestJob.Commands
{
    [Description("-list_today - Показать список названий файлов всех сохранённых анкет, созданных сегодня")]
    public class ListToday : ICommand
    {
        public string CommandName { get; private set; } = "-list_today";


        private readonly IProfileRepository profileRepository;
        public ListToday(IProfileRepository profileRepository) {
            this.profileRepository = profileRepository;
        }

        public IResponse Execute(SessionContext sessionContext, string parameter)
        {
           var list = profileRepository.TodayId();
            foreach (var profileId in list) {
                Console.WriteLine(profileId);
            }
            return new Response(ResponseType.Ok);
        }

    }
}
