using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SEMS.Models
{
    public class Administrater
    {
        [Key]
        [MaxLength(15)]
        [Display(Name = "用户名")]
        [Required(ErrorMessage = "请输入用户名")]
        public string admin_id { set; get; }

        [MaxLength(40)]
        [Display(Name = "密码")]
        [Required(ErrorMessage = "请输入密码")]
        public string admin_pwd { set; get; }

        [MaxLength(40)]
        [Display(Name = "描述")]
        public string admin_descr { set; get; }

        [NotMapped]
        public string Description
        {
            get 
            {
                return admin_id + " - " + admin_descr;
            }
        }
    }
}