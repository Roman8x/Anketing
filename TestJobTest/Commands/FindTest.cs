using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TestJob.Commands;
using TestJob.Helpers;
using TestJob.Model;
using TestJob.Repository;
using TestJobTest.Model;

namespace TestJobTest.Commands
{
    [TestClass]
    public class FindTest
    {
        [TestMethod]
        public void Execute()
        {
            var conf = new Configuration();
            conf.WorkingDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Test");

            var etf = new EntityTextFormatter<Profile>();
            var repo = new ProfileRepository(conf, etf);
            var cmd = new Find (repo);
            var profile = MokProfile.GetSomeProfile().FIO;
            var res = cmd.Execute(null, profile +".txt");
            Assert.IsNotNull(res);
        }
    }
}
