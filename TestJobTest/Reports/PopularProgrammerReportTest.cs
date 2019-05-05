using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestJob.Model;
using TestJob.Reports;

namespace TestJobTest.Reports
{
    [TestClass]
    public class PopularProgrammerReport
    {
        [TestMethod]
        public void Execute()
        {
            var report = new AverageYearReport();
            IEnumerable<string> result;

            var param = new List<Profile>();
            result = report.Create(param);
            Assert.IsNotNull(result);
        }
    }
}
