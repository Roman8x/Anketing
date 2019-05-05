using System;
using System.Collections.Generic;
using System.Windows.Input;
using TestJob.Helpers;
using TestJob.Interfaces;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;
using TestJob.Commands;
using TestJob.Model;
using ICommand = TestJob.Interfaces.ICommand;
using TestJob.Reports;
using TestJob.Repository;
using TestJob.Commands.ChangeStep;

namespace TestJob
{
    class Program
    {   
        static void Main()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);            
            var  provider = serviceCollection.BuildServiceProvider();


            IList<ICommand> commands = new List<ICommand>(provider.GetServices<ICommand>());
            commands.Add(new Help(commands));

            var commandManager = new CommandManager(commands , provider.GetService<SessionContext>() );

            while (commandManager.IsRunning ) {
                SendWelkome();
                var s = Console.ReadLine();
                commandManager.RunCmd(s.Trim());
            }

            Console.ReadKey();
            
        }
 

        private static void SendWelkome()
        {
            Console.WriteLine("Выберите действие:");
        }


        static private void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<SessionContext, SessionContext>();
            // Вспомогательные классы 
            serviceCollection.AddSingleton<Configuration>();
            serviceCollection.AddSingleton<EntityTextFormatter<Profile>>();
            
            // Репозитори 
            serviceCollection.AddSingleton<IProfileRepository, ProfileRepository>();

            // Команды          
            var lt = new ServiceLifetime();
            
            serviceCollection.Add(new ServiceDescriptor(typeof(ICommand), typeof (Delete), lt));
            serviceCollection.Add(new ServiceDescriptor(typeof(ICommand), typeof(Exit), lt));
            serviceCollection.Add(new ServiceDescriptor(typeof(ICommand), typeof(Find), lt));         
            serviceCollection.Add(new ServiceDescriptor(typeof(ICommand), typeof(ListProfiles), lt));
            serviceCollection.Add(new ServiceDescriptor(typeof(ICommand), typeof(ListToday), lt));
            serviceCollection.Add(new ServiceDescriptor(typeof(ICommand), typeof(NewProfile), lt));
            serviceCollection.Add(new ServiceDescriptor(typeof(ICommand), typeof(SaveProfile), lt));
            serviceCollection.Add(new ServiceDescriptor(typeof(ICommand), typeof(Statistics), lt));
            serviceCollection.Add(new ServiceDescriptor(typeof(ICommand), typeof(ZipProfile), lt));

            //serviceCollection.Add(new ServiceDescriptor(typeof(ICommand), typeof(Help), lt));


            // Команды редактирования 
            serviceCollection.Add(new ServiceDescriptor(typeof(IChangeStepCommand), typeof(GotoPrevQuestion), lt));
            serviceCollection.Add(new ServiceDescriptor(typeof(IChangeStepCommand), typeof(GotoQuestion), lt));
            serviceCollection.Add(new ServiceDescriptor(typeof(IChangeStepCommand), typeof(RestartProfile), lt));

            // Отчеты
            serviceCollection.Add(new ServiceDescriptor(typeof(IReport), typeof(AverageYearReport), lt));
            serviceCollection.Add(new ServiceDescriptor(typeof(IReport), typeof(PopularLanguageReport), lt));
            serviceCollection.Add(new ServiceDescriptor(typeof(IReport), typeof(PopularProgrammerReport), lt));
        }
    }
}
