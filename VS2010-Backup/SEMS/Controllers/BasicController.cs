using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SEMS.Controllers
{
    public class BasicController : Controller
    {
        //
        // GET: /Basic/

        public ActionResult Index ( )
        {
            //下拉框填充
            var yearlist = BLL.Evaluation_yearBS.GetEvaluation_yearList();
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var item in yearlist)
            {
                list.Add(new SelectListItem()
                {
                    Text = item.evaluation_year_name,
                    Value = item.evaluation_year_id.ToString()
                });
            }
            ViewBag.List = list;
            //填充完毕
            return View();
        }

        [HttpPost]
        public ActionResult Index ( SEMS.ViewModels.BaseSearch model )
        {
            //下拉框填充
            var yearlist = BLL.Evaluation_yearBS.GetEvaluation_yearList();
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var item in yearlist)
            {
                list.Add(new SelectListItem()
                {
                    Text = item.evaluation_year_name,
                    Value = item.evaluation_year_id.ToString()
                });
            }
            ViewBag.List = list;
            //填充完毕

            if (BLL.StudentBS.FindStudent(model.student_id) != null)
            {
                return RedirectToAction("Show", new
                {
                    studentid = model.student_id,
                    year_id = model.year_id
                });
            }
            else
                ModelState.AddModelError("", "无此学号");
            return View(model);
        }

        public ActionResult Show ( string studentid, int? year_id )
        {
            if (string.IsNullOrEmpty(studentid) || !year_id.HasValue)       //要判断
            {
                return RedirectToAction("Index");
            }
            SEMS.ViewModels.BaseSearch model = new ViewModels.BaseSearch();
            model.student_id = studentid;
            model.year_id = year_id.Value;
            return View(model);
        }
    }
}
