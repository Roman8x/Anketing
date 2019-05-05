using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace TestJob.Commands.ChangeStep
{
    /// <summary>
    /// Изменение номера заполнения сущности 
    /// </summary>
    public interface IChangeStepCommand
    {
        // имя команды 
        string Name { get;   }
        /// <summary>
        /// Выполнение новго шага
        /// </summary>
        /// <param name="step">текущий шаг</param>
        /// <param name="nextStep">Следующий шаг</param>
        /// <returns></returns>
        int Execute(int step, string param );
    }
}
