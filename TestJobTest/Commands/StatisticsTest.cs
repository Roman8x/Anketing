using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TestJob.Commands;
using TestJob.Helpers;
using TestJob.Model;
using TestJob.Reports;
using TestJob.Repository;

namespace TestJobTest.Commands
{
    [TestClass]
    public class StatisticsTest
    {
        [TestMethod]
        public void Execute()
        {
            var conf = new Configuration();
            conf.WorkingDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Test");
            var etf = new EntityTextFormatter<Profile>();
            var repo = new ProfileRepository(conf, etf);
            IList< IReport>  repoDict = new List < IReport>();
            repoDict.Add( new AverageYearReport());
            repoDict.Add( new PopularLanguageReport());
            repoDict.Add( new PopularProgrammerReport());

            var cmd = new Statistics(repo, repoDict);
            var res = cmd.Execute(null, null);
            Assert.IsNotNull(res);
        }
    }
}
