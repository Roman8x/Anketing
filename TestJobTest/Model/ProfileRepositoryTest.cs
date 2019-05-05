using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TestJob.Helpers;
using TestJob.Model;

namespace TestJobTest.Model
{
    [TestClass]
    public class ProfileRepositoryTest 
    {          
        [TestMethod]
        public void SaveOrUpdate()
        {
            var conf = new Configuration();
            conf.WorkingDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Test");

            IProfileRepository testRepository = new ProfileRepository(conf, new TestJob.Repository.EntityTextFormatter<Profile>());
            testRepository.SaveOrUpdate(MokProfile.GetSomeProfile ());
        }

        [TestMethod]
        public bool Delete( )
        {
            var conf = new Configuration();
            conf.WorkingDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Test");

            var fio = MokProfile.GetSomeProfile().FIO + ".txt";
            IProfileRepository testRepository = new ProfileRepository(conf, new TestJob.Repository.EntityTextFormatter<Profile>());
            var res= testRepository.Delete(fio); ;
            Assert.IsTrue(res);
            return res;
        }     
 
 

        [TestMethod]
        public IEnumerable<string> TodayId()
        {
            var conf = new Configuration();
            conf.WorkingDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Test");

            IProfileRepository testRepository = new ProfileRepository(conf, new TestJob.Repository.EntityTextFormatter<Profile>());
            var todayList = testRepository.TodayId();
            Assert.IsNotNull(todayList);
            Assert.IsNotNull(todayList.Count() > 1);
            return todayList;
        }


        [TestMethod]
        public void GetAllId()
        {
            var conf = new Configuration();
            conf.WorkingDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Test");

            IProfileRepository testRepository = new ProfileRepository(conf, new TestJob.Repository.EntityTextFormatter<Profile>());
            var todayList = testRepository.GetAllId();
            Assert.IsTrue(todayList.Count () > 0);        
        }


        [TestMethod]
        public Profile FindByID( )
        {
            var conf = new Configuration();
            conf.WorkingDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Test");

            IProfileRepository testRepository = new ProfileRepository(conf, new TestJob.Repository.EntityTextFormatter<Profile>());
            Profile orign = MokProfile.GetSomeProfile();
            Profile profile = testRepository.FindByID(orign.FIO+".txt");

            Assert.Equals(orign, profile);
            return orign;
        }


        [TestMethod]
        public IList<Profile> GetAll()
        {
            var conf = new Configuration();
            conf.WorkingDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Test");

            IProfileRepository testRepository = new ProfileRepository(conf, new TestJob.Repository.EntityTextFormatter<Profile>());
            var todayList = testRepository.GetAll();
            Assert.IsTrue(todayList.Count( ) > 0);
            return todayList;
        }
    }
}
