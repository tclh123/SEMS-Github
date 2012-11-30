using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using SEMS.Filters;

namespace SEMS.Controllers.Admin
{
    [VaildateLogin]
    public class ScoreController : Controller
    {
        //
        // GET: /Score/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SingleModify (string id,int? score)
        {
            //为下拉框填充数据
            List<SEMS.Models.Evaluation_entry> entrys = BLL.Evaluation_entryBS.GetCurEvaluation_entryList();//.GetAllEvaluation_entryList();
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var item in entrys)
            {
                list.Add(new SelectListItem()
                {
                    Text=item.Description,
                    Value=item.entry_id.ToString()
                });
            }
            ViewBag.List = list;
            //填充完毕
            if (id != null&&score!=null)
            {
                SEMS.ViewModels.ModifyScore model = new SEMS.ViewModels.ModifyScore();
                model.student_id = id;
                model.score =(int) score;
                return View(model);
            }
            return View();
        }

        [HttpPost]
        public ActionResult SingleModify (SEMS.ViewModels.ModifyScore model)
        {
            if (BLL.StudentBS.FindStudent(model.student_id)!=null)
            {
                SEMS.Models.Entry_score entry_score = new Models.Entry_score()
                    {
                        student_id = model.student_id,
                        entry_id = int.Parse(model.entry_id),
                        score = model.score
                    };
                if (BLL.Entry_scoreBS.GetEntry_score(entry_score.student_id, entry_score.entry_id) == 0)
                {
                    BLL.Entry_scoreBS.AddEntry_score(entry_score);
                }
                else if (BLL.Entry_scoreBS.GetEntry_score(entry_score.student_id, entry_score.entry_id) == -1)
                {
                    throw new Exception();
                }
                else
                    BLL.Entry_scoreBS.ModifyEntry_score(entry_score.student_id, entry_score.entry_id, entry_score.score);
                return RedirectToAction("Index");
            }
            else
            {
                //为下拉框填充数据
                List<SEMS.Models.Evaluation_entry> entrys = BLL.Evaluation_entryBS.GetCurEvaluation_entryList();
                List<SelectListItem> list = new List<SelectListItem>();
                foreach (var item in entrys)
                {
                    list.Add(new SelectListItem()
                    {
                        Text = item.Description,
                        Value = item.entry_id.ToString()
                    });
                }
                ViewBag.List = list;
                //填充完毕

                ModelState.AddModelError("", "不存在此学生!");
                return View(model);
            }
        }

        public ActionResult MultiModify ()
        {
            //为下拉框填充数据
            List<SEMS.Models.Evaluation_entry> scorelist = BLL.Evaluation_entryBS.GetCurEvaluation_entryList();
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var item in scorelist)
            {
                list.Add(new SelectListItem()
                    {
                        Text = item.Description,
                        Value=item.entry_id.ToString()
                    });
            }
            ViewBag.List = list;
            //填充完毕
            return View();
        }

        [HttpPost]
        public ActionResult MultiModify (SEMS.ViewModels.ModifyScore model)
        {
            int id = int.Parse(model.entry_id);
            if (BLL.Entry_scoreBS.AllModifyEntry_score(id, model.score))
                return RedirectToAction("Index");
            else
            {
                //为下拉框填充数据
                List<SEMS.Models.Evaluation_entry> scorelist = BLL.Evaluation_entryBS.GetCurEvaluation_entryList();
                List<SelectListItem> list = new List<SelectListItem>();
                foreach (var item in scorelist)
                {
                    list.Add(new SelectListItem()
                    {
                        Text = item.Description,
                        Value = item.entry_id.ToString()
                    });
                }
                ViewBag.List = list;
                //填充完毕
                return View(model);
            }
        }

        public ActionResult UploadExcel(int? selectedEntryID)
        {
            //为下拉框填充数据
            var entryList = BLL.Evaluation_entryBS.GetCurEvaluation_entryList();
            ViewBag.SelectedEntryID = new SelectList(entryList, "entry_id", "Description", selectedEntryID);
            //填充完毕
            return View();
        }

        [HttpPost]
        public ActionResult UploadExcel(int? selectedEntryID, HttpPostedFileBase file)
        {
            //为下拉框填充数据
            var entryList = BLL.Evaluation_entryBS.GetCurEvaluation_entryList();
            ViewBag.SelectedEntryID = new SelectList(entryList, "entry_id", "Description", selectedEntryID);
            //填充完毕

            if (selectedEntryID.HasValue// != null
                && file != null && file.ContentLength > 0)
            {
                var filename = Path.GetFileName(file.FileName);

                string path = BLL.IO.SaveToAdminTemp(file);

                int ret;
                if ((ret = BLL.Entry_scoreBS.ImportEntry_score(selectedEntryID.Value, path)) != -1)
                {
                    ViewBag.Ret = ret;  //把ret显示出去
                    return this.View("UploadResult");
                }
            }
            ModelState.AddModelError("", "上传失败!");
            return View();
        }

        public ActionResult MultiDelete(string selectedentry)
        {
            //填充下拉框
            var entrylist=BLL.Evaluation_entryBS.GetCurEvaluation_entryList();
            List<SelectListItem>list=new List<SelectListItem>();
            foreach(var item in entrylist)
            {
                list.Add(new SelectListItem(){ Text =item.module_id+"-"+item.entry_description, Value=item.entry_id.ToString()});
            }
            ViewBag.selectedentry=list;
            //填充完毕
            if (!string.IsNullOrEmpty(selectedentry))
            {
                int id = int.Parse(selectedentry);
                BLL.Entry_scoreBS.AllDelEntry_score(id);
                return RedirectToAction("Index");
            }
            else
                return View();
        }

        public ActionResult DownloadTemplate()
        {
            var path = BLL.IO.Download(@"Template\导入成绩模板.xls");
            //System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.Open, System.IO.FileAccess.Read, FileShare.ReadWrite);
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
