using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SEMS.Models;
using SEMS.Filters;
using PagedList;

namespace SEMS.Controllers.Admin
{
    [VaildateLogin]
    public class ClassesController : Controller
    {
        //
        // GET: /Admin/Classes/

        public ActionResult Index(int? page)
        {
            int pageSize = 20;  //一页显示二十个学生
            int pageNumber = (page ?? 1);       // ( page ?? 1 ) 意味着如果 page 有值得话返回这个值，如果是 null 的话，返回 1
            return View(BLL.ClassesBS.GetClassList().ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Edit(string id, string smallid, string grade)
        {
            ViewBag.ID = id;
            ViewBag.SmallID = smallid;
            Classes model = new Classes()
            {
                class_id = id,
                class_small_id = smallid,
                class_grade = grade
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(string id,string smallid,Classes ChangeModel)
        {
            if (BLL.ClassesBS.ModifyClass(id,smallid,ChangeModel))
            {
                return RedirectToAction("Index", "Classes");
            }
            else
                ModelState.AddModelError("", "修改失败!");

            //修改失败
            return View(ChangeModel);
            
            //return new EmptyResult();
        }

        public ActionResult Details(string id,string smallid)
        {
            /*
            ViewBag.ID = class_id;
            return View(BLL.StudentBS.GetStudentList(class_id));
             */
            //return new EmptyResult();//ToDo
            //ViewBag.ID = class_id;
            //ViewBag.SmallID = class_small_id;
            //Console.WriteLine(class_id+" "+class_small_id);
            //
            ViewBag.Class = BLL.StudentBS.GetStudentCountOfClass(id, smallid);
            ViewBag.Entry_score = BLL.Entry_scoreBS.GetEntry_scoreCountOfClass(id, smallid);
            Classes model = BLL.ClassesBS.FindClass(id, smallid);
            return View(model);
            /*if (model == null)
            {
                return RedirectToAction("Index");
            }*/
            //return View(model);//View(model);
        }

        public ActionResult Add ( )
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add ( SEMS.Models.Classes model )
        {
            if (BLL.ClassesBS.AddClass(model))
            {
                return RedirectToAction("Index", "Classes");
            }
            else
                ModelState.AddModelError("", "添加失败!");

            //添加失败
            return View(model);
        }

        public ActionResult Delete ( string id, string smallid )
        {
            if (!BLL.ClassesBS.DelClass(id, smallid))
            {
                ModelState.AddModelError("","删除失败!");
            }

            return RedirectToAction("Index");
        }
    }
}