using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;
using TestJob.Commands.ChangeStep;
using TestJob.Interfaces;

namespace TestJob.Commands
{
    [Description("-goto_question <Номер вопроса> - Вернуться к указанному вопросу ")]
    public class GotoQuestion : IChangeStepCommand
    {
        public int Execute(int step, string param )
        {

            int nextStep;

            if (!int.TryParse(param, out nextStep)) {
                Console.WriteLine("Ошибка передачи параметров");
                return 0;
            }
            

            if (nextStep < 0 || nextStep > step) {
                Console.WriteLine($"Перейти к вопросу №{step} невозможно");
                return 0;
            }
                
            return nextStep-1;
        }

        public string Name { get; } = "-goto_question";
    }
}
