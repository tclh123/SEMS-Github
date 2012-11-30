using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SEMS.ViewModels
{
    public class ChangePassWord
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "旧密码")]
        public string Oldpwd
        {
            get;
            set;
        }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "新密码")]
        [StringLength(12,ErrorMessage="{0}至少要有{2}位",MinimumLength=6)]
        public string Newpwd
        {
            set;
            get;
        }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        [Compare("Newpwd", ErrorMessage = "两次密码不一致,请重新输入!")]
        public string Confirmpwd
        {
            get;
            set;
        }
    }
}