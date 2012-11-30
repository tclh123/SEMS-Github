using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SEMS.Models
{
    public class Evaluation_year
    {
        [Key]
        [Required]
        public int evaluation_year_id { set; get; }

        [Required]
        [MaxLength(15)]
        public string evaluation_year_name { set; get; }

        public virtual ICollection<Classes> Classes { set; get; }

        public virtual ICollection<Lk_evaluation_year_classes> Lk_evaluation_year_classes { set; get; }
    }
}