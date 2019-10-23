using SOIT.Data;
using SOIT.Repos.Interface;
using SOIT.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOIT.Repos.Repository
{
    public class UserProfileRepo : IUserProfileRepo,ICommonServiceRepo
    {
        SOITEntities _db;
        UserProfileService _userProfileService;
        public UserProfileRepo()
        {
            _db = new SOITEntities();
            _userProfileService = new UserProfileService(_db);
        }
        public List<UserProfile> _GetAllUseProfile()
        {
            List<UserProfile> userProfiles= this._userProfileService.GetAllUserProfiles();
            var taskList = new { };
            List<object> userTask = new List<object>();
            userTask.Add(new
            {
                userProfiles.First().FullName,
                TaskName = ""
            });
            return userProfiles;

            //SOITEntities db = new SOITEntities();
            //return db.UserProfile.ToList();
        }

        public string _GetSystemTime()
        {
            throw new NotImplementedException();
        }
    }
}
