using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SEMS.Models
{
    /// <summary>
    /// 评测项目
    /// </summary>
    public class Evaluation_entry
    {
        [Key]
        [Required]
        [Display(Name="编号")]
        public int entry_id { set; get; }

        [Required]
        [MaxLength(15)]
        [Display(Name = "学年(格式:YYYY-YYYY)")]
        [RegularExpression(@"^2\d{3}-2\d{3}$", ErrorMessage = "格式错误！请输入如 2011-2012 的学年格式。")]
        public string entry_school_year { set; get; }

        //enum('秋', '春')
        [Required]
        [Display(Name="学期")]
        public string entry_semester { set; get; }

        [Required]
        [MaxLength(150)]
        [Display(Name="描述")]
        public string entry_description { set; get; }

        [Required]
        [Display(Name="项目开始时间(格式:YYYY-MM-DD)")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]   //d表示短日期，ApplyFormatInEditMode 表示在 LabelFor() 的时候也使用 Format！
        public DateTime entry_date { set; get; }    //entry_date  date not null comment'项目开始时间，e.g. YYYY-MM-DD）', 

        [Required]
        [MaxLength(5)]
        [Display(Name="所属模块")]
        public string module_id { set; get; }

        [NotMapped]
        public string Description
        {
            get
            {
                return module_id + " - " + entry_description; ;
            }
        }

        public virtual Module Module { set; get; }

        ///// <summary>
        ///// 一个项目包含一个集合的学生
        ///// </summary>
        //public virtual ICollection<Student> Students { set; get; }

        /// <summary>
        /// 项目得分集合                  //别少了!!!
        /// </summary>
        public virtual ICollection<Entry_score> Entry_scores { set; get; }
    }
}