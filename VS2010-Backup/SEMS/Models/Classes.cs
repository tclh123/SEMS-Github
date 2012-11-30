using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SEMS.Models
{
    public class Classes
    {
        [Key, Column(Order = 0)]    //由于是复合主键，所以必须为他们确定顺序
        //[StringLength(6,ErrorMessage="{0}长度必须为{2}",MinimumLength=6)]
        [Required]
        [Display(Name="班级")]
        [MaxLength(15)]
        public string class_id { set; get; }
        
        [Key, Column(Order = 1)]
        //[StringLength(2,ErrorMessage="{0}长度必须为{2}",MinimumLength=2)]
        [Required]
        [Display(Name="小班号")]
        [MaxLength(5)]
        public string class_small_id { set; get; }

        [NotMapped]
        public string class_id_tot
        {
            get
            {
                return string.Format("{0}{1}", class_id, class_small_id);
            }
        }

        [StringLength(4,ErrorMessage="{0}长度必须为{2}",MinimumLength=4)]
        [Required]
        [Display(Name="年级")]
        public string class_grade { set; get; }

        /// <summary>
        /// 导航属性，一个班级包含一个集合的学生
        /// </summary>
        public virtual ICollection<Student> Students { get; set; }

        public virtual ICollection<Evaluation_year> Evaluation_years { set; get; }

        public virtual ICollection<Lk_evaluation_year_classes> Lk_evaluation_year_classes { set; get; }
    }
}