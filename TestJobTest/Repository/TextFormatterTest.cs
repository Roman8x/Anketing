using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TestJob.Model;
using TestJob.Repository;
using TestJobTest.Model;

namespace TestJobTest.Repository
{
    [TestClass]
    public class TextFormatterTest
    {
        [TestMethod]
        public void  SerializableProfile() {
            var mokProfile = MokProfile.GetSomeProfile ();
            var fileName = Path.Combine(Config.PATH_TO_FILE, mokProfile.FIO+".txt");
           
            using (var fs = new StreamWriter(fileName, false, Encoding.UTF8))
            {
                EntityTextFormatter<Profile> textFormatter = new EntityTextFormatter<Profile>();
                textFormatter.Serialize(fs.BaseStream, mokProfile);                
            }
            Assert.IsTrue (System.IO.File.Exists(fileName));
        }

        [TestMethod]
        public void DeserializableProfile()
        {
            var mokProfile = MokProfile.GetSomeProfile();
            var fileName = Path.Combine(Config.PATH_TO_FILE, mokProfile.FIO + ".txt");
            Profile result;


            using (var fr = new StreamReader(fileName,   Encoding.UTF8))
            {
                EntityTextFormatter<Profile> textFormatter = new EntityTextFormatter<Profile>();
                result = textFormatter.Deserialize (fr.BaseStream) as Profile;
            }
            Assert.IsNotNull(result);
            Assert.AreEqual <Profile>(mokProfile, result);
        }
    }
}
