using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using TestJob.Helpers;
using TestJob.Interfaces;
using TestJob.Model;
using TestJob.Reports;

namespace TestJob.Commands
{
    /// <summary>
    /// Показать статистику всех заполненных анкет
    /// </summary>
    [Description  ("-statistics - Показать статистику всех заполненных анкет")]
    public class Statistics : ICommand 
    {
        public string CommandName { get; private set; } = "-statistics";

        private readonly IProfileRepository profileRepository;
        private readonly IEnumerable< IReport> reports;
        public Statistics(IProfileRepository profileRepository, IEnumerable <IReport> reports  ) {            
            this.profileRepository = profileRepository;
            this.reports = reports;
        }

        public IResponse Execute(SessionContext sessionContext, string parameter)
        {
            IList<Profile> list = profileRepository.GetAll();

            foreach (var report in reports)
                PrintReport(report.Create(list));

            return new Response(ResponseType.Ok);
        }


        /// <summary>
        /// Выводит построчно отчёт
        /// </summary>
        /// <param name="someStrings"></param>
        private void PrintReport(IEnumerable<string> someStrings) {
            foreach (var s in someStrings)
                Console.WriteLine(s);
        }
    }
}
