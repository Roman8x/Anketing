using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestJob.Model;
using TestJob.Reports;
using TestJobTest.Model;

namespace TestJobTest.Reports
{
    [TestClass]
    public class AverageYearReportTest
    {
        [TestMethod]
        public void Execute()
        {
            var report = new AverageYearReport();
            IEnumerable<string> result;

            IList<Profile> param = new List<Profile>();
            result = report.Create(param);
            Assert.IsNotNull(result);

            param = MokProfile.GetListProfiles();
            result = report.Create(param);
            Assert.IsNotNull(result);

            
           // int average = (2 + 3 + 4 + 5) / 4; // 14/4 =3 года


        }
    }
}
