using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TestJob.Commands;
using TestJob.Helpers;

namespace TestJobTest.Commands
{
    [TestClass]
    public class ZipProfileTest
    {
        [TestMethod]
        public void ZipProfile() {
        
        Configuration conf = new Configuration() { WorkingDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Test") };
        var zp = new ZipProfile(conf);
            var ss = new SessionContext();
            const string FILE_NAME = @"Иванов Иван Иванович";
            var folder = Path.Combine(Config.PATH_TO_FILE, "Zip");
            var param = FILE_NAME + ".txt " + folder;
            zp.Execute(null, param);
            var res = System.IO.File.Exists(Path.Combine( folder , FILE_NAME + ".zip"));
            Assert.IsTrue(res);
        }
    }
}
