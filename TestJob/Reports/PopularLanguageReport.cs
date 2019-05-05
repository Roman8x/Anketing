using System.Collections.Generic;
using System.Linq;
using TestJob.Model;

namespace TestJob.Reports
{
    /// <summary>
    /// Самый популярный язык программирования: Название языка программирования, который большинство пользователей указали как любимый
    /// </summary>
    public class PopularLanguageReport : IReport 
    {
        private struct PopularLang
        {
            public string Lang;
            public int cnt;
        }

 
        public IEnumerable<string> Create   (IList<Profile> list)
        {
            const string MSG = "Самый популярный язык программирования: ";
            if ((list == null) || (!list.Any()))
                return new List<string>();
            var groupLang = list.GroupBy(profile => profile.lang);

            IEnumerable<PopularLang> populars = from profile in groupLang
                                                select new PopularLang()
                                                { Lang = profile.Key, cnt = profile.Count() };

            int max = populars.Max(x => x.cnt);            
            var result =  populars
                .Where(x => x.cnt == max)
                .Select ( x => MSG + x.Lang);
            return result;
        }
    }            
}
