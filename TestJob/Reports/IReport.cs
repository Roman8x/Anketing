using System.Collections.Generic;
using TestJob.Model;

namespace TestJob.Reports
{
    public  interface IReport
    {
        IEnumerable<string> Create(IList<Profile> list);
    }
}