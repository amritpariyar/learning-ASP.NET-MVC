using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOIT.Data.ViewModels
{
    public class UserQualificationViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Institution { get; set; }
        public bool IsCertification { get; set; }
        public bool IsEducation { get; set; }
        public string  ReceiveDate { get; set; }
        public int? UserProfileId { get; set; }

    }
}