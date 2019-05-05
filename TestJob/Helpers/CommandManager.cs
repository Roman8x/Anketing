using System;
using System.Collections.Generic;
using TestJob.Interfaces;
using TestJob.Model;

namespace TestJob.Helpers
{
    class CommandManager
    {
        private static bool isRunning = true;
        private readonly IDictionary<string, ICommand> cmdDictonary = new Dictionary<string, ICommand>();
        private readonly SessionContext sessionContext = new SessionContext();

        public CommandManager(IEnumerable <ICommand> commands, SessionContext sessionContext) {;
            foreach (var cmd in commands) {
                cmdDictonary.Add(cmd.CommandName, cmd);
            }
            this.sessionContext = sessionContext;
        }

        public bool IsRunning { get { return isRunning; } internal set { isRunning = value; } }

        public void RunCmd(string cmdString)
        {
            ICommand command = null;

            // Команда не указана
            if (string.IsNullOrWhiteSpace(cmdString))
                return;

            cmdString = cmdString.TrimStart();
            var cmd = cmdString.Split(" ")[0];
            if (cmdDictonary.TryGetValue(cmd, out command))
            {
                var param = cmdString.Substring(cmd.Length ).Trim ();
                var response = command.Execute(sessionContext, param); ;
                ResponseWorker(response);
            }               
            else
                Console.WriteLine($"Неизвестная команда: {cmd}");
        }


        /// <summary>
        /// Парсит ответ команды и в зависимости от вида ответа выполняет дейсвие  
        /// </summary>
        /// <param name="response"></param>
        private void ResponseWorker(IResponse response) {
            if (response == null || response.ResponseType == ResponseType.Ok)
                return;
            switch (response.ResponseType) {
                case ResponseType.Entity : {
                        sessionContext.Anketa = response.Bag;
                        break;
                    }
                case ResponseType.BadParameters:
                    {
                        Console.WriteLine("Команда не выполнена, не верные параметры");
                        break;
                    }

                default:
                    {
                        throw new Exception("Неизвестный тип команды");
                    }
            }
        }
    }
}
