using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SEMS.Filters;

namespace SEMS.Controllers.Admin
{
    [VaildateLogin]
    public class SystemController : Controller
    {
        //
        // GET: /System/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ModifySys ( )
        {
            return View();
        }

        [HttpPost]
        public ActionResult ModifySys (SEMS.Models.Sysinfo model )
        {
            if (ModelState.IsValid && BLL.SysinfoBS.ModifySysinfo(model))
            {
                return RedirectToAction("Index");
            }
            else
                ModelState.AddModelError("", "修改失败!");
            return View(model);
        }

        public ActionResult ClearTemp()
        {
            if (BLL.IO.ClearAdminTemp())
            {
                return this.View("ClearTempSuccessed"); //RedirectToAction("ClearTempSuccessed");
            }
            else
                ModelState.AddModelError("", "清空失败!");
            return View();
        }

        public ActionResult ModifyPolicy ( string selectedmodule )
        {
            var policy=BLL.PolicyBS.GetPolicy(selectedmodule);
            //policy.module_id = selectedmodule;
            return View(policy);
        }

        [HttpPost]
        public ActionResult ModifyPolicy ( string selectedmodule, SEMS.Models.Policy model )
        {
            if (BLL.PolicyBS.ModifyPolicy(selectedmodule, model))
            {
                return RedirectToAction("Index");
            }
            else
                ModelState.AddModelError("", "修改失败!");
            model.module_id = selectedmodule;
            return View(model);
        }

        public ActionResult SelectPolicy ( string selectedmodule )
        {
            //下拉框填充
            var modulelist = BLL.ModuleBS.GetModuleIDList();
            ViewBag.selectedmodule = new SelectList(modulelist, selectedmodule);
            //填充完毕
            if (!string.IsNullOrEmpty(selectedmodule))
            {
                return RedirectToAction("ModifyPolicy", new
                {
                    selectedmodule = selectedmodule
                });
            }
            else
                return View();
        }
    }
}
