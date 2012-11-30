using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SEMS.Models
{
    public class Student
    {
        [Key]
        [StringLength(8,ErrorMessage="{0}必须为{2}位!",MinimumLength=8)]
        [Required]
        [Display(Name="学号")]
        public string student_id { set; get; }

        [MaxLength(15)]
        [Required]
        [Display(Name="姓名")]
        public string student_name { set; get; }
        
        //enum('男', '女')
        [Required]
        [Display(Name="性别")]
        public string student_sex { set; get; }

        [Required]
        [Display(Name="班级")]
        //[StringLength(6,ErrorMessage="{0}长度必须为{2}!",MinimumLength=6)]
        public string class_id { set; get; }

        [Required]
        [Display(Name="班号")]
        [StringLength(2, ErrorMessage = "{0}长度必须为{2}!", MinimumLength = 2)]
        public string class_small_id { set; get; }

        public virtual Classes Classes { set; get; }

        ///// <summary>
        ///// 学生的测评项目集合
        ///// </summary>
        //public virtual ICollection<Evaluation_entry> Evaluation_entrys { set; get; }

        /// <summary>
        /// 学生的项目得分集合
        /// </summary>
        public virtual ICollection<Entry_score> Entry_scores { set; get; }

        /// <summary>
        /// 学生的模块总分集合
        /// </summary>
        public virtual ICollection<Module_score> Module_scores { set; get; }

    }
}