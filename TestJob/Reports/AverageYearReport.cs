using System;
using System.Collections.Generic;
using System.Linq;
using TestJob.Model;

namespace TestJob.Reports
{
    /// <summary>
    /// Средний возраст всех опрошенных
    /// </summary>
    public class AverageYearReport : IReport
    {
        public IEnumerable<string> Create(IList<Profile> list)
        {
            var result = new List<string>(1);
            if (list == null || !list.Any())
                return result;

            var avgYear = AverageYears(list);
                
            result.Add($"Средний возраст всех опрошенных: {avgYear} {YearToString(avgYear)}");

            return result;
        }


    
        private int AverageYears(IList<Profile> list)
        {
            var f = DateTime.Now - DateTime.Now;
            long averageticks = (long)list.Average(x => ( DateTime.Now- x.DateBirth ).Ticks);
            int year = new DateTime(averageticks).Year;
            return year;
        }

        private string YearToString(int year)
        {
            // 1 год
            // 2 .. 4 года
            // 5 .. 0 лет 
            switch (year % 10)
            {
                case 1: { return "год"; }
                case 2:
                case 3:
                case 4: { return "года"; }
                default: { return "лет"; }
            }
        }
    }
}
