using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SEMS.Models
{
    public class Module
    {
        [Key]
        [Required]
        [MaxLength(5)]
        public string module_id { set; get; }       //[ForeignKey("PhotoOf")]

        [Required]
        [MaxLength(15)]
        public string module_name { set; get; }

        /// <summary>
        /// 模块的测评项目表
        /// </summary>
        public virtual ICollection<Evaluation_entry> Evaluation_entry { set; get; }

        public virtual Policy Policy { set; get; }
    }
}