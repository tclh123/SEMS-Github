using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SEMS.Models;
using SEMS.Filters;
using System.IO;
using PagedList;        //引用分页控件

namespace SEMS.Controllers.Admin
{
    [VaildateLogin]
    public class StudentController : Controller
    {
        //
        // GET: /Student/

        public ActionResult Index(string SelectedClass, int? page)
        {
            //为下拉框填充数据
            var classes = BLL.ClassesBS.GetClassIdList();
            ViewBag.SelectedClass = new SelectList(classes, SelectedClass);
            //ViewBag.SelectedClass = new SelectList(classes, "class_id", "class_id", SelectedClass);

            int pageSize = 20;  //一页显示二十个学生
            int pageNumber = (page ?? 1);       // ( page ?? 1 ) 意味着如果 page 有值得话返回这个值，如果是 null 的话，返回 1

            if (!string.IsNullOrEmpty(SelectedClass))   //按班级筛选学生
            {
                ViewBag.ClassId = SelectedClass;
                var students = BLL.StudentBS.GetStudentList(SelectedClass);
                return View(students.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                var students = BLL.StudentBS.GetStudentList();
                return View(students.ToPagedList(pageNumber, pageSize));    
            }
        }

        public ActionResult Create ( )
        {
            //创建班级下拉框
            var classlist = BLL.ClassesBS.GetClassIdList();
            List<SelectListItem> idlist = new List<SelectListItem>();
            foreach (var item in classlist)
            {
                idlist.Add(new SelectListItem()
                {
                    Text = item,
                    Value = item
                });
            };
            ViewBag.ClassList = idlist;
            //创建班级下拉框完毕

            return View();
        }

        [HttpPost]
        public ActionResult Create ( SEMS.Models.Student student )
        {
            if (BLL.StudentBS.AddStudent(student))
                return RedirectToAction("Index");
            else
                ModelState.AddModelError("", "添加失败!");

            //创建班级下拉框
            var classlist = BLL.ClassesBS.GetClassIdList();
            List<SelectListItem> idlist = new List<SelectListItem>();
            foreach (var item in classlist)
            {
                idlist.Add(new SelectListItem()
                {
                    Text = item,
                    Value = item
                });
            };
            ViewBag.ClassList = idlist;
            //创建班级下拉框完毕
            return View(student);
        }

        public ActionResult Delete ( string id )
        {
            BLL.StudentBS.DelStudent(id);
            return RedirectToAction("Index");
        }


        public ActionResult Edit ( string id )
        {
            ViewBag.ID = id;
            SEMS.Models.Student student = BLL.StudentBS.FindStudent(id);

            //创建班级下拉框
            var classlist = BLL.ClassesBS.GetClassIdList();
            List<SelectListItem> idlist = new List<SelectListItem>();
            foreach (var item in classlist)
            {
                idlist.Add(new SelectListItem()
                {
                    Text = item,
                    Value = item
                });
            };
            ViewBag.ClassList = idlist;
            //创建班级下拉框完毕

            return View(student);
        }

        [HttpPost]
        public ActionResult Edit (string id,SEMS.Models.Student student )
        {
            if (BLL.StudentBS.ModifyStudent(id, student))
                return RedirectToAction("Index");
            else
                ModelState.AddModelError("", "修改失败!");

            //创建班级下拉框
            var classlist = BLL.ClassesBS.GetClassIdList();
            List<SelectListItem> idlist = new List<SelectListItem>();
            foreach (var item in classlist)
            {
                idlist.Add(new SelectListItem()
                {
                    Text = item,
                    Value = item
                });
            };
            ViewBag.ClassList = idlist;
            ViewBag.ID = id;               //要设置。。
            //创建班级下拉框完毕
            return View(student);       //!!
        }

        public ActionResult Details (string id)
        {
            if (string.IsNullOrEmpty(id))return RedirectToAction("Index");
            
            ViewBag.ID = id;

            List<SEMS.Models.Entry_score> list = BLL.Entry_scoreBS.GetStuEntry_scoreList(id);//获取
            if (list == null) return RedirectToAction("Index");

            List<SEMS.Models.Evaluation_entry> list_2 = BLL.Evaluation_entryBS.GetAllEvaluation_entryList();

            ViewBag.entry_scorelist = list;
            ViewBag.Evaluation_entrylist = list_2;
            SEMS.Models.Student student = BLL.StudentBS.FindStudent(id);
            if (student == null)return RedirectToAction("Index");
            
            return View(student);
        }

        public ActionResult MultiCreate ( string selectedclass ,string smallid )
        {
            //为下拉框填充数据
            var idlist = BLL.ClassesBS.GetClassIdList();
            var small_idlist = BLL.ClassesBS.GetSmall_ClassIdList();
            ViewBag.selectedclass = new SelectList(idlist, selectedclass);
            ViewBag.smallid = new SelectList(small_idlist, smallid);
            //填充完毕
            return View();
        }

        [HttpPost]
        public ActionResult MultiCreate(string selectedclass, string smallid, HttpPostedFileBase file)// HttpPostedFileBase file )
        {
            //为下拉框填充数据
            var idlist = BLL.ClassesBS.GetClassIdList();
            var small_idlist = BLL.ClassesBS.GetSmall_ClassIdList();
            ViewBag.selectedclass = new SelectList(idlist, selectedclass);
            ViewBag.smallid = new SelectList(small_idlist, smallid);
            //填充完毕

            if (!string.IsNullOrEmpty(selectedclass)
                && !string.IsNullOrEmpty(smallid)
                && file != null && file.ContentLength > 0)
            {
                var filename = Path.GetFileName(file.FileName);

                string path = BLL.IO.SaveToAdminTemp(file);

                int ret = 0;
                if ((ret = BLL.StudentBS.ImportStudent(selectedclass, smallid, path)) != -1)
                {
                    ViewBag.Ret = ret;
                    return this.View("UploadResult");
                }
            }
            ModelState.AddModelError("", "上传失败!");
            return View();
        }

        public ActionResult MultiDelete ( )
        {
            //填充下拉框
            var classlist = BLL.ClassesBS.GetClassIdList();
            ViewBag.ClassList = new SelectList(classlist);
            var smalllist=BLL.ClassesBS.GetSmall_ClassIdList();
            ViewBag.SmallList=new SelectList(smalllist);
            //填充完毕
            return View();
        }

        [HttpPost]
        public ActionResult MultiDelete ( SEMS.Models.Classes model )
        {
            //填充下拉框
            var classlist = BLL.ClassesBS.GetClassIdList();
            ViewBag.ClassList = new SelectList(classlist);
            var smalllist = BLL.ClassesBS.GetSmall_ClassIdList();
            ViewBag.SmallList = new SelectList(smalllist);
            //填充完毕
            if (BLL.ClassesBS.FindClass(model.class_id, model.class_small_id)!=null)
            {
                if (BLL.StudentBS.AllDelStudent(model.class_id, model.class_small_id))
                {
                    return RedirectToAction("Index");
                }
                else
                    ModelState.AddModelError("", "删除失败!");
                return View(model);
            }
            else
                ModelState.AddModelError("", "不存在此班级!");
            return View(model);
        }

        public ActionResult DownloadTemplate()
        {
            var path = BLL.IO.Download(@"Template\导入学生模板.xls");
            //FileStream fs = new FileStream(path, FileMode.Open);
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            byte[] bytes = new byte[(int)fs.Length];
            fs.Read(bytes, 0, bytes.Length);
            fs.Close();
            Response.Charset = "UTF-8";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(path));
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
            return new EmptyResult();
        }
    }
}
