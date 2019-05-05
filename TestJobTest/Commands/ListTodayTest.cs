using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using TestJob.Commands;
using TestJob.Helpers;
using TestJob.Model;
using TestJob.Repository;

namespace TestJobTest.Commands
{
    [TestClass]
    public class ListTodayTest
    {
        [TestMethod]
        public void Execute()
        {
            var conf = new Configuration();
            conf.WorkingDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Test");


            var etf = new EntityTextFormatter<Profile>();
            var repo = new ProfileRepository(conf, etf);
            var cmd = new ListToday(repo);
            var res = cmd.Execute(null, null);
            Assert.IsNotNull(res);
        }
    }
}
