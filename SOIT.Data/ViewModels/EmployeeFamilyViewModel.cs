using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOIT.Data.ViewModels
{
    public class EmployeeFamilyViewModel
    {
        public int id { get; set; }
        public string fullName { get; set; }
        public string dob { get; set; }
        public string gender { get; set; }
        public string maritalStatus { get; set; }
        public string bloodGroup { get; set; }
        public string photo { get; set; }
        public string pAddress { get; set; }
        public string cAddress { get; set; }
        public string landline { get; set; }
        public string mobile { get; set; }
        public string citizeship { get; set; }
        public string ctznshipDatePlace { get; set; }
        public string pan { get; set; }
        public string email { get; set; }
        public string emergencyContactName { get; set; }
        public string emergencyContactNo { get; set; }
        public Nullable<bool> status { get; set; }
        public string createdBy { get; set; }
        public Nullable<System.DateTime> createdDate { get; set; }
        public string modifiedBy { get; set; }
        public string modifiedDate { get; set; }
        public string deletedBy { get; set; }
        public Nullable<System.DateTime> deletedDate { get; set; }

        //no of FamilyDetail for one employee so need list type of FamilyDetail.cs
        public virtual List<familyDetail> FamilyDetails { get; set; }
    }
}
