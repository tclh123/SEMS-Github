using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SEMS.Filters;
using PagedList;

namespace SEMS.Controllers.Admin
{
    [VaildateLogin]
    public class EntryController : Controller
    {
        //
        // GET: /Module/

        public ActionResult Index(string selectedmoduel, int? page)
        {
            //为所属模块创建下拉列表
            var moduellist = BLL.ModuleBS.GetModuleIDList();
            ViewBag.SelectedModuel = new SelectList(moduellist, selectedmoduel);
            //创建完毕

            int pageSize = 20;  //一页显示二十个学生
            int pageNumber = (page ?? 1);       // ( page ?? 1 ) 意味着如果 page 有值得话返回这个值，如果是 null 的话，返回 1

            if (!string.IsNullOrEmpty(selectedmoduel))
            {
                ViewBag.ModuelId = selectedmoduel;
                var entrylist = BLL.Evaluation_entryBS.GetCurEvaluation_entryList(selectedmoduel);
                return View(entrylist.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                var entrylist = BLL.Evaluation_entryBS.GetCurEvaluation_entryList();
                return View(entrylist.ToPagedList(pageNumber, pageSize));
            }
         }

        public ActionResult Create ()
        {
            //创建下拉框数据
            SelectList list = new SelectList(BLL.ModuleBS.GetModuleIDList());
            ViewBag.List = list;
            //创建完毕

            return View();
        }

        [HttpPost]
        public ActionResult Create ( SEMS.Models.Evaluation_entry model )
        {
            if (ModelState.IsValid && BLL.Evaluation_entryBS.AddEvaluation_entry(model))
            {
                return RedirectToAction("Index");
            }
            else
                ModelState.AddModelError("", "创建失败");

            //创建下拉框数据
            SelectList list = new SelectList(BLL.ModuleBS.GetModuleIDList());
            ViewBag.List = list;
            //创建完毕

            //创建失败
            return View(model);
        }

        public ActionResult Delete ( int id )
        {
            BLL.Evaluation_entryBS.DelEvaluation_entry(id);
            return RedirectToAction("Index");
        }

        public ActionResult Edit ( int id )
        {
            //为所属模块创建下拉列表数据
            SelectList list = new SelectList(BLL.ModuleBS.GetModuleIDList());
            ViewBag.List = list;
            //创建完毕

            SEMS.Models.Evaluation_entry model = BLL.Evaluation_entryBS.FindEvaluation_entry(id);
            ViewBag.ID = id;
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit (int id , SEMS.Models.Evaluation_entry model )
        {
            //为所属模块创建下拉列表数据
            SelectList list = new SelectList(BLL.ModuleBS.GetModuleIDList());
            ViewBag.List = list;
            //创建完毕
            ViewBag.ID = id;
            
            if (ModelState.IsValid && BLL.Evaluation_entryBS.ModifyEvaluation_entry(id, model))
            {
                return RedirectToAction("Index");
            }
            else
                ModelState.AddModelError("", "修改失败");
            return View(model);
        }

        public ActionResult Details ( int id )
        {
            var model=BLL.Evaluation_entryBS.FindEvaluation_entry(id);
            return View(model);
        }

        public ActionResult FindOldEntry(int? page)
        {
            int pageSize = 20;  //一页显示20个
            int pageNumber = (page ?? 1);       // ( page ?? 1 ) 意味着如果 page 有值得话返回这个值，如果是 null 的话，返回 1
            return View(BLL.Evaluation_entryBS.GetOldEvaluation_entry().ToPagedList(pageNumber, pageSize));
        }
    }
}
