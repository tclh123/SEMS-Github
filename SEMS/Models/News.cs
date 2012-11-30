using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SEMS.Models
{
    public class News
    {
        [Key]
        [Required]
        public int news_id { set; get; }

        [MaxLength(40)]
        [Required]
        [Display(Name="标题")]
        public string news_title { set; get; }

        [MaxLength(2000)]
        [Required]
        [Display(Name="内容")]
        public string news_content { set; get; }

        [Required]
        public DateTime new_date { set; get; }

        [MaxLength(15)]
        [Required]
        public string admin_id { set; get; }

        public virtual Administrater Administrater { set; get; }
    }
}