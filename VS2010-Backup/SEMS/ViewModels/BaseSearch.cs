using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SEMS.ViewModels
{
    public class BaseSearch
    {
        [Required]
        [Display(Name = "学号")]
        [MaxLength(15)]
        public string student_id
        {
            get;
            set;
        }

        [Required]
        [Display(Name = "测评年")]
        public int year_id
        {
            get;
            set;
        }
    }
}