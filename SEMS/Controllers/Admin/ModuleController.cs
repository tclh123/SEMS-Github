using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SEMS.Filters;

namespace SEMS.Controllers.Admin
{
    [VaildateLogin]
    public class ModuleController : Controller
    {
        //
        // GET: /Moduel/

        public ActionResult Index()
        {
            var moduellist = BLL.ModuleBS.GetModuleList();
            return View(moduellist);
        }

    }
}
