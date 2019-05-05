
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using TestJob.Commands.ChangeStep;
using TestJob.Helpers;
using TestJob.Interfaces;
using TestJob.Model;

namespace TestJob.Commands
{
    /// <summary>
    ///  Заполнить новую анкету
    /// </summary>
    [Description  ("-new_profile - Заполнить новую анкету")]
    public class NewProfile : ICommand
    {
        public string CommandName { get; private set; } = "-new_profile";

        private readonly IDictionary<string, IChangeStepCommand> changeStepCmdDict = new Dictionary<string, IChangeStepCommand>();
        public NewProfile(   IEnumerable < IChangeStepCommand> changeStepCmds)
        {
            foreach (var cmd in changeStepCmds)
                changeStepCmdDict.Add(cmd.Name, cmd);

        }

        public IResponse Execute(SessionContext sessionContext, string parameter)
        {
            Profile profile = new Profile();
            var properties = PropertiesWorker.GetDisplayProperties(typeof(Profile));

            Console.WriteLine("Заполните анкету, отвечая на вопросы");
            for (int step = 0; step <= (properties.Count() - 1);)
            {
                var property = properties[step];
                var displayAttribute = properties[step].GetCustomAttribute<DisplayAttribute>();                
                Console.WriteLine($"{displayAttribute.Order}. {displayAttribute.Prompt?? displayAttribute.Name}");
                var s = Console.ReadLine();
                if (String.IsNullOrWhiteSpace(s))
                    continue;

                string [] cmdCheckStr = s.Split(" ");
                IChangeStepCommand cmd;
                if (changeStepCmdDict.TryGetValue (cmdCheckStr [0], out  cmd) )
                {
                    // Пришла команда на изменение номера шага
                    var param  = s.Substring(cmdCheckStr[0].Length);
                    step = cmd.Execute( step, param  );
                }
                else
                {
                    int nextStep = ValidatePropertyValue(profile, property, s);
                    step += nextStep;
                }

            }
            return new Response(ResponseType.Entity, profile);       
        }

        private int  ValidatePropertyValue(Profile profile , PropertyInfo property, string s ) {
            try
            {
                // 1. проверяем строку регулярным выражением, если всё Ок
                // 2. Конвертим значение в нужный тип
                var value = Convert.ChangeType(s, property.PropertyType);
                typeof(Profile).GetProperty(property.Name).SetValue(profile, value);
                // 3. Валидируем значение 

                //


                ICollection<ValidationResult> results = new List<ValidationResult>();  
                var context = new ValidationContext(profile) { MemberName = property.Name };

                if (!Validator.TryValidateProperty(value, context, results)) {
                    foreach (var res in results)
                    {
                        Console.WriteLine(res.ErrorMessage);
                    }
                    return 0;
                }                          
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Повторите ввод");
                return 0;
            }                    
        }
    }
}
       
 