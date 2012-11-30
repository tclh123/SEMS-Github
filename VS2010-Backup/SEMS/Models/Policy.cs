using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SEMS.Models
{
    public class Policy
    {
        [Key]
        [MaxLength(5)]
        [Display(Name="模块ID")]
        public string module_id { set; get; }

        [Display(Name = "优秀分")]
        public int? policy_excellent { set; get; }

        [Display(Name = "良好分")]
        public int? policy_good { set; get; }

        [Display(Name = "及格分")]
        public int? policy_pass { set; get; }

        [Display(Name = "起评分")]
        public int? policy_basic { set; get; }

        [Required]
        public virtual Module Module { set; get; }
    }
}