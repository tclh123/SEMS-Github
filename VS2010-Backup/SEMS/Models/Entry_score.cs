using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SEMS.Models
{
    /// <summary>
    /// 项目得分表
    /// </summary>
    public class Entry_score
    {
        [Key,Column(Order = 0)]
        [ForeignKey("Student")]
        [Required]
        [MaxLength(15)]
        public string student_id { set; get; }

        [Key, Column(Order = 1)]
        [Required]
        [ForeignKey("Evaluation_entry")]
        public int entry_id { set; get; }

        [Required]
        public int score { set; get; }

        public virtual Student Student { set; get; }
        public virtual Evaluation_entry Evaluation_entry { set; get; }
    }
}