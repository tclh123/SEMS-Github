using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SEMS.Filters;

namespace SEMS.Controllers.Admin
{
    [VaildateLogin]
    public class HomeController : Controller
    {
        //
        // GET: /Admin/Home/
        public ActionResult Index ( )
        {
            int classnum = BLL.ClassesBS.GetClassCount();
            int studentnum = BLL.StudentBS.GetStudentCount();
            int entrynum = BLL.Evaluation_entryBS.GetAllEvaluation_entryCount();
            int entryCurNum = BLL.Evaluation_entryBS.GetCurEvaluation_entryCount();
            ViewBag.Class = classnum;
            ViewBag.Student = studentnum;
            ViewBag.EntryCur = entryCurNum;
            ViewBag.Entry = entrynum;
            SEMS.Models.Sysinfo model = BLL.SysinfoBS.GetSysinfo();
            return View(model);
        }
    }

        
}
