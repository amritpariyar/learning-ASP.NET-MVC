using SOIT.Data;
using SOIT.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOIT.Services
{
    public class UserProfileService
    {
        //declare dbContext veriable
        private SOITEntities db;
        public UserProfileService()
        {
            db = new SOITEntities();
        }

        public List<UserProfile> GetAllUserProfiles()
        {
            var userProfiles = db.UserProfile.ToList();
            userProfiles = userProfiles
                .Where(a => !a.IsDeleted.HasValue || (a.IsDeleted.HasValue && !a.IsDeleted.Value))
                .ToList();
            return userProfiles;
        }

        public bool CreateUserProfile(UserProfile userProfile)
        {
            try
            {
                db.UserProfile.Add(userProfile);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public UserProfile GetUserProfileById(int id)
        {
            UserProfile previousRecord = db.UserProfile.Where(a => a.Id == id).AsNoTracking().FirstOrDefault();
            return previousRecord;
        }

        public bool UpdateUserProfile(UserProfile userProfile)
        {
            try
            {
                db.Entry<UserProfile>(userProfile).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<UserQualificationViewModel> GetUserQualificationByUserprofileId(int id)
        {
            List<UserQualificationViewModel> userQualification = db.UserQualification.Where(t => t.UserProfileId == id)
                 .Select(t => new UserQualificationViewModel()
                 {
                     Id = t.Id,
                     Title = t.Title,
                     Institution = t.Institution,
                     IsCertification = t.IsCertification.Value,
                     IsEducation = t.IsEducation.Value,
                     ReceiveDate = t.ReceiveDate,
                     UserProfileId = t.UserProfileId
                 }).ToList();
            return userQualification;

        }

        public UserQualification SaveUserQualification(int userProfileId, string title, string institution, string receiveDate, string quali_certi,string userName)
        {
            UserQualification qualification = new UserQualification();
            qualification.UserProfileId = userProfileId;
            qualification.Title = title;
            qualification.Institution = institution;
            qualification.ReceiveDate = receiveDate;
            if (quali_certi == "Q")
            {
                qualification.IsEducation = true;
            }
            else
            {
                qualification.IsEducation = false;
            }
            if (quali_certi == "C")
            {
                qualification.IsCertification = true;
            }
            else
            {
                qualification.IsCertification = false;
            }
            qualification.CreatedBy = userName;// User.Identity.Name;
            qualification.CraetedDate = DateTime.Now;
            db.UserQualification.Add(qualification);
            db.SaveChanges();
            return qualification;
        }
    }
}
