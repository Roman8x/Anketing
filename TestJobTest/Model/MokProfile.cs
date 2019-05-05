using System;
using System.Collections.Generic;
using System.Text;
using TestJob.Model;

namespace TestJobTest.Model
{
    public static class MokProfile
    {
        public static Profile GetSomeProfile  () => new Profile()
        {
            DateBirth = DateTime.Now.AddYears(-32).Date,
            Experience = 2,
            FIO = "Иванов Иван Иванович",
            lang = "PHP",
            Phone = "345890"
        };

        public static IList<Profile> GetListProfiles() {
            var result = new List<Profile>();
            result.Add(GetSomeProfile());
            var mokProfile = GetSomeProfile();
            mokProfile.Experience = 3;
            result.Add(mokProfile);

            mokProfile.Experience = 4;
            result.Add(mokProfile);

            mokProfile.Experience = 5;
            result.Add(mokProfile);

            return result;
        }
    }
}
