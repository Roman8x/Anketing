using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using TestJob.Helpers;
using TestJob.Interfaces;
using TestJob.Model;

namespace TestJob.Commands
{
    [Description("-list - Показать список названий файлов всех сохранённых анкет")]
    public class ListProfiles : ICommand
    {
        public string CommandName { get; private set; } = "-list";

        private readonly IProfileRepository profileRepository  ;        
        public ListProfiles(IProfileRepository profileRepository)
        {
            this.profileRepository = profileRepository;
        }
        public IResponse Execute(SessionContext sessionContext, string parameter)
        {
            IEnumerable <string> list = profileRepository.GetAllId();
            foreach (var profileId in list)
                Console.WriteLine(profileId);
            return new Response(ResponseType.Ok);
        }
    }
}
