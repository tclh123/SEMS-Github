using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SEMS.Models
{
    public class Lk_evaluation_year_classes
    {
        [Key, Column(Order = 0)]
        [Required]
        public int evaluation_year_id { set; get; }

        [Key, Column(Order = 1)]
        [Required]
        [MaxLength(15)]
        public string class_id { set; get; }

        [Key, Column(Order = 2)]
        [Required]
        [MaxLength(5)]
        public string class_small_id { set; get; }

        [Required]
        [MaxLength(15)]
        public string evaluation_school_year { set; get; }

        //enum('秋', '春')
        [Required]
        public string evaluation_semester { set; get; }

        public virtual Evaluation_year Evaluation_year { set; get; }
        public virtual Classes Classes { set; get; }
    }
}