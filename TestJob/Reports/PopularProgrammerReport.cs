using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestJob.Model;

namespace TestJob.Reports
{
    /// <summary>
    ///Самый опытный программист:   ФИО человека, у которого указан самый большой опыт работы
    /// </summary>
    public class PopularProgrammerReport : IReport
    {
        public IEnumerable<string> Create(IList<Profile> list)
        {
            const string MSG = "Самый опытный программист: ";
            if ((list == null) || !list.Any())
                return new List< string>();

            var maxYearExp = list.Max(p => p.Experience);        
            var result =  list
                .Where(profile => profile.Experience == maxYearExp)
                .Select(profile => MSG +  profile.FIO);

            return result;
        }
    }
}
