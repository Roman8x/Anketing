using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TestJob.Helpers;

namespace TestJob.Interfaces
{
    public interface ICommand 
    {
        /// <summary>
        /// Имя команды
        /// </summary>
        string CommandName { get; }
        IResponse Execute(SessionContext sessionContext , string paramater);     
    }
}
