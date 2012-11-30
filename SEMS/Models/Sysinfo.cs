using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SEMS.Models
{
    public class Sysinfo
    {
        [Key]
        [Required]
        public int sysinfo_id { set; get; }

        [Required]
        [MaxLength(15)]
        [Display(Name = "系统表学年(格式:YYYY-YYYY)")]
        [RegularExpression(@"^2\d{3}-2\d{3}$", ErrorMessage = "格式错误！请输入如 2011-2012 的学年格式。")]
        public string sysinfo_school_year { set; get; }

        //enum('秋', '春')
        [Required]
        [Display(Name="系统表学期")]
        public string sysinfo_semester { set; get; }
    }
}