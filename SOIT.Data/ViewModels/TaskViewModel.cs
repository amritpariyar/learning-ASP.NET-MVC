using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SOIT.Data.ViewModels
{
    public class TaskViewModel
    {
        
        public int Id { get; set; }

        [Required]
        [Display(Name ="task 1")]
        [MaxLength(10,ErrorMessage ="Exceeding Limit")]
        [MinLength(1)]
        public string TaskName { get; set; }

        [Required(ErrorMessage ="Task Duration Cann't left blank"),
            MaxLength(10,ErrorMessage ="exceed"),MinLength(1), Display(Name ="Task Duration"),]
        public double? TaskDuration { get; set; }

        [Required(AllowEmptyStrings =true)]
        public bool IsActive { get; set; }
    }
}
