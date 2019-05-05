using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using TestJob.Commands.ChangeStep;
using TestJob.Interfaces;

namespace TestJob.Commands
{
    [Description("-restart_profile - Заполнить анкету заново (Команда доступна только при заполнении анкеты, вводится вместо ответа на любой вопрос)")]
    public class RestartProfile : IChangeStepCommand
    {
        public int Execute(int step, string param)
        {
            return 0;
        }

        public string Name { get; } = "-restart_profile";
    }
}
 
