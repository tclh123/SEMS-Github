using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SEMS.Filters;

namespace SEMS.Controllers.Admin
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/
        [VaildateLogin]
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Account/Login/

        public ActionResult Login()
        {
            if(Request.IsAuthenticated) // 如果已存在登陆Cookie
                return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        public ActionResult Login(Models.Administrater model)
        {
            if (ModelState.IsValid)
            {
                if (BLL.AdministraterBS.IsValid(model.admin_id, model.admin_pwd))
                {
                    FormsAuthentication.SetAuthCookie(model.admin_id, false);    //!!
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "用户名或密码错误！");
                }
            }

            // 登陆失败
            return View(model);
        }

        //
        // GET: /Account/ChangePassword/
        [VaildateLogin]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [VaildateLogin]
        public ActionResult ChangePassword(SEMS.ViewModels.ChangePassWord model)
        {
            //if (BLL.AdministraterBS.ChangePassword("tclh123", model.Newpwd,model.Oldpwd))
            if (BLL.AdministraterBS.ChangePassword(User.Identity.Name, model.Newpwd, model.Oldpwd))
            {
                return RedirectToAction("Index", "Home");
            }
            else
                ModelState.AddModelError("", "密码输入错误!");

            //修改失败
            return View(model);
        }

        //
        // GET: /Account/Logout/
        [VaildateLogin]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}
