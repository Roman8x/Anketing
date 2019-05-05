using System.ComponentModel;
using TestJob.Helpers;
using TestJob.Interfaces;
using static System.Net.Mime.MediaTypeNames;

namespace TestJob.Commands
{
    [Description("-exit - Выйти из приложения")]
    public class Exit : ICommand
    {
        public string CommandName { get; private set; } = "-exit";
        public IResponse Execute(SessionContext sessionContext, string parameter)
        {
            System.Environment.Exit(0);
            return new Response(ResponseType.Ok);
        }
    }
}
