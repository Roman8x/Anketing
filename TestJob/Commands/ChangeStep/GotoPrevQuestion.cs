using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using TestJob.Commands.ChangeStep;
using TestJob.Interfaces;

namespace TestJob.Commands
{
    [Description("-goto_prev_question - Вернуться к предыдущему вопросу (Команда доступна только при заполнении анкеты, вводится вместо ответа на любой вопрос)")]
    public class GotoPrevQuestion : IChangeStepCommand
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"> номер текущего шага, набор свойств, параметр </param>
        /// <returns></returns>
     
        public int Execute(int step, string param)
        {            
            if (step <= 0)
            {
                Console.WriteLine("Вернуться к предыдущему вопросу невозможно ");
                return 0;
            }
            else
                return --step;
        }

        public string Name { get;   } = "-goto_prev_question";
    }
}
