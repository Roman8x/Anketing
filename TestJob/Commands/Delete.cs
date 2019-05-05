using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using TestJob.Helpers;
using TestJob.Interfaces;
using TestJob.Model;

namespace TestJob.Commands
{
    [Description("-delete <Имя файла анкеты> - Удалить указанную анкету")]
    public class Delete : ICommand
    {
        private readonly IProfileRepository profileRepository;

        public string CommandName { get; private set; } = "-delete";
        public Delete (IProfileRepository profileRepository)
        {
            this.profileRepository = profileRepository;
        }

        public IResponse Execute(SessionContext sessionContext, string parameter)
        {
            if (string.IsNullOrWhiteSpace(parameter)) {               
                return new Response (ResponseType.BadParameters);
            }
      
            profileRepository.Delete(parameter);
            return new Response(ResponseType.Ok);
        }
    }
}
