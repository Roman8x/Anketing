using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using TestJob.Helpers;
using TestJob.Interfaces;
using TestJob.Model;

namespace TestJob.Commands
{
    /// <summary>
    /// Сохранить заполненную анкету
    /// </summary>
    [Description("-save - Сохранить заполненную анкету")]
    public class SaveProfile : ICommand
    {
        public string CommandName { get; private set; } = "-save";

        private readonly IProfileRepository profileRepository; 
        public SaveProfile(IProfileRepository profileRepository) {
            this.profileRepository = profileRepository; 
        }
        public IResponse Execute(SessionContext sessionContext, string parameter)
        {
            if (sessionContext?.Anketa == null)
                return new Response(ResponseType.NoSessionContent);

            Profile profile = sessionContext.Anketa;
            profileRepository.SaveOrUpdate(profile);
            Console.WriteLine("Анкета сохранена");
            return new Response(ResponseType.Ok, profile);
        }
    }
}
