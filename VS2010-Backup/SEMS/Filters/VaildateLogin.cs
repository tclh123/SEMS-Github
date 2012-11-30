using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SEMS.Filters
{
    public class VaildateLogin : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectResult("/Admin/Account/Login");

                //filterContext.Result = new RedirectToRouteResult("Default", new RouteValueDictionary(new { area = "Admin", controller = "Account", action = "Login", namespaces = new[] { "SEMS.Controllers.Admin" } }));
                //不起作用？
            }
        }
    }
}
