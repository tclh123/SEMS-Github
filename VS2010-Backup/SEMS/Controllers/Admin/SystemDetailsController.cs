using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using SEMS.Filters;

namespace SEMS.Controllers.Admin
{
    [VaildateLogin]
    public class SystemDetailsController : Controller
    {
        //
        // GET: /SystemDetails/

        public ActionResult Index()
        {
            return this.View();
        }

        /// <summary>
        /// 用于DropdownList二级联动，异步返回Json
        /// </summary>
        /// <param name="school_year"></param>
        /// <param name="semester"></param>
        /// <returns></returns>
        public JsonResult ScoreListByEntry_JSON(string school_year, string semester)
        {
            var sys = BLL.SysinfoBS.GetSysinfo();
            school_year = string.IsNullOrEmpty(school_year) ? sys.sysinfo_school_year : school_year;     //处理默认
            semester = string.IsNullOrEmpty(semester) ? sys.sysinfo_semester : semester;
            var entrys = BLL.Evaluation_entryBS.GetEvaluation_entryList(school_year, semester);
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Value = "", Text = "无" });     //默认项目：无
            foreach (var e in entrys)
            {
                items.Add(new SelectListItem { Value = e.entry_id.ToString(), Text = e.Description });
            }
            return Json(items, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ScoreListByEntry(int? selectedEntry, string selectedSchool_year, string selectedSemester, int? page)
        {
            //为下拉框填充数据
            ViewBag.SelectedSemester = new SelectList(new List<string>() { "春", "秋" }, selectedSemester);
            ViewBag.SelectedSchool_year = new SelectList(BLL.Evaluation_entryBS.GetSchool_yearList(), selectedSchool_year);
            var sys = BLL.SysinfoBS.GetSysinfo();
            string school_year = string.IsNullOrEmpty(selectedSchool_year) ? sys.sysinfo_school_year : selectedSchool_year;     //处理默认
            string semester = string.IsNullOrEmpty(selectedSemester) ? sys.sysinfo_semester : selectedSemester;
            ViewBag.SelectedEntry = new SelectList(BLL.Evaluation_entryBS.GetEvaluation_entryList(school_year, semester), "entry_id", "Description", selectedEntry); 
            //填充完毕

            int pageSize = 20;  //一页显示二十个学生
            int pageNumber = (page ?? 1);       // ( page ?? 1 ) 意味着如果 page 有值得话返回这个值，如果是 null 的话，返回 1

            if (selectedEntry.HasValue) //选择项目不为空，（学年学期只是用来筛选项目），即判断为Post
            {
                ViewBag.EntryId = selectedEntry.Value;
                var scoreList = BLL.Entry_scoreBS.GetEntry_scoreList(selectedEntry.Value);
                return this.View(scoreList.ToPagedList(pageNumber, pageSize));
            }
            else    //项目为空，即为Get
            {
                return this.View();
            }
        }

        public ActionResult ScoreListByClasses(string SelectedClassID, int? page)
        {
            //为下拉框填充数据
            var classidlist = BLL.ClassesBS.GetClassIdList();
            ViewBag.SelectedClassID = new SelectList(classidlist, SelectedClassID);
            //填充完毕

            ViewBag.classID = SelectedClassID;

            int pageSize = 5;  //一页显示5个学生
            int pageNumber = (page ?? 1);       // ( page ?? 1 ) 意味着如果 page 有值得话返回这个值，如果是 null 的话，返回 1

            if (!string.IsNullOrEmpty(SelectedClassID))
            {
                ViewBag.ClassID = SelectedClassID;
                var studentlist = BLL.StudentBS.GetStudentList(SelectedClassID);
                return View(studentlist.ToPagedList(pageNumber, pageSize));
            }
            return View();
        }

        /*public ActionResult ScoreListByStudent(FormCollection form)
        {
            Models.Student model = BLL.StudentBS.FindStudent(form["StudentID"]);
            if (model != null)
            {
                return View(model);
            }
            return View();
        }*/
        public ActionResult ScoreListByStudent(string StudentID)
        {
            ViewBag.ID = StudentID;
            List<SEMS.Models.Entry_score> list = BLL.Entry_scoreBS.GetStuEntry_scoreList(StudentID);//获取
            List<SEMS.Models.Evaluation_entry> list_2 = BLL.Evaluation_entryBS.GetAllEvaluation_entryList();
            ViewBag.entry_scorelist = list;
            ViewBag.Evaluation_entrylist = list_2;
            Models.Student model = BLL.StudentBS.FindStudent(StudentID);
            if (model != null)
            {
                return View(model);
            }
            else if(StudentID!=null)
                ModelState.AddModelError("", "无此学号!");
            return View();
        }

        /*
        public ActionResult ScoreListByStudent(string StudentID)
        {

        }*/
        public ActionResult Del ( string student_id, int entry_id )
        {
            BLL.Entry_scoreBS.DelEntry_score(student_id, entry_id);
            return RedirectToAction("ScoreListByStudent", new
            {
                StudentID=student_id
            });
        }
    }
}
