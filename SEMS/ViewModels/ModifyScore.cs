using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SEMS.ViewModels
{
    public class ModifyScore
    {
        [Required]
        [StringLength(8,ErrorMessage="{0}长度必需为{2}",MinimumLength=8)]
        [Display(Name="学生学号")]
        public string student_id { set; get; }

        [Required]
        [Display(Name="项目名称")]
        public string entry_id { set; get; }

        [Required]
        [Display(Name="分数")]
        public int score { set; get; }

        public string name
        {
            get;
            set;
        }
    }
}