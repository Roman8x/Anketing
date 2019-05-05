using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using TestJob.Helpers;
using TestJob.Interfaces;

namespace TestJob.Commands
{
    [Description("-help  Показать список доступных команд с описанием")]
    public class Help : ICommand
    {
        private readonly IEnumerable<ICommand> allcommands;

        public string CommandName { get; private set; } = "-help";


        /// <summary>
        /// Все доступные команды
        /// </summary>
        /// <param name="allcommands"></param>
        public Help(IEnumerable<ICommand> allcommands ) {
            var allcmds  = new List<ICommand>(allcommands);
            allcmds.Add(this);
            this.allcommands = allcmds;
        }

        public IResponse Execute(SessionContext sessionContext, string parameter)
        {

            foreach (ICommand cmd in allcommands)
            {
                var description = cmd.GetType().GetCustomAttribute(typeof(DescriptionAttribute)) as DescriptionAttribute;
                Console.WriteLine(description.Description);
            }

            return new Response(ResponseType.Ok);
        }
    }
}
