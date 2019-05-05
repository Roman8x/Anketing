using System.Collections.Generic;

namespace TestJob.Model
{
    public interface IProfileRepository
    {
        void  SaveOrUpdate(Profile profile);
        bool Delete(string id);
        IEnumerable<string> TodayId ();
        IEnumerable<string> GetAllId();
        Profile FindByID(string id);
        IList<Profile> GetAll();
    }
}