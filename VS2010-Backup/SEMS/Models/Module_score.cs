using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SEMS.Models
{
    /// <summary>
    /// 模块总分
    /// </summary>
    public class Module_score
    {
        /// <summary>
        /// 总分ID，int自增
        /// </summary>
        [Key]
        [Required]
        public int score_id { set; get; }

        /// <summary>
        /// 学生学号
        /// </summary>
        [Required]
        [StringLength(8,ErrorMessage="{0}必须为{2}位!",MinimumLength=8)]
        public string student_id { set; get; }

        /// <summary>
        /// 模块ID
        /// </summary>
        [Required]
        [MaxLength(5)]
        public string module_id { set; get; }

        /// <summary>
        /// 测评学年, e.g.2011-2012
        /// </summary>
        [Required]
        [MaxLength(15)]
        public string evaluation_school_year { set; get; }

        /// <summary>
        /// 测评学期，enum('秋', '春')
        /// </summary>
        [Required]
        public string evaluation_semester { set; get; }

        /// <summary>
        /// 得分
        /// </summary>
        [Required]
        public int score { set; get; }

        public virtual Student Student { set; get; }
        public virtual Module Module { set; get; }
    }
}