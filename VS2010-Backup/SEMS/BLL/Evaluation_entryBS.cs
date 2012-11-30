using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SEMS.DAL;
using SEMS.Models;

namespace SEMS.BLL
{
    public class Evaluation_entryBS
    {
        /// <summary>
        /// 查询已有学年list
        /// </summary>
        /// <returns></returns>
        static public List<string> GetSchool_yearList()
        {
            using (var db = new SEMSDBContext())
            {
                return db.Evaluation_entry.Select(x => x.entry_school_year).Distinct().ToList();   //Select是投影操作
            }
        }

        /// <summary>
        /// 获取项目总数（默认为系统表的学年学期）
        /// </summary>
        static public int GetCurEvaluation_entryCount()
        {
            using (var db = new SEMSDBContext())
            {
                var temp = db.Sysinfo.FirstOrDefault();
                return db.Evaluation_entry.Where(x => x.entry_school_year == temp.sysinfo_school_year && x.entry_semester == temp.sysinfo_semester).Count();
            }
        }

        /// <summary>
        /// 获取当前学年学期该模块下的项目总数
        /// </summary>
        /// <param name="module_id">模块ID</param>
        /// <returns>数量</returns>
        static public int GetCurEvaluation_entryCount(string module_id)
        {
            using (var db = new SEMSDBContext())
            {
                var temp = db.Sysinfo.FirstOrDefault();
                return db.Evaluation_entry.Where(x => x.entry_school_year == temp.sysinfo_school_year && x.entry_semester == temp.sysinfo_semester && x.module_id == module_id).Count();
            }
        }
        /// <summary>
        /// 获取项目列表（默认为系统表的学年学期）
        /// </summary>
        static public List<Evaluation_entry> GetCurEvaluation_entryList()
        {
            using (var db = new SEMSDBContext())
            {
                var temp = db.Sysinfo.FirstOrDefault();
                return GetEvaluation_entryList(temp.sysinfo_school_year, temp.sysinfo_semester);
                //return db.Evaluation_entry.Where(x => x.entry_school_year == temp.sysinfo_school_year && x.entry_semester == temp.sysinfo_semester).ToList();
            }
        }

        /// <summary>
        /// 获取所有项目总数（所有学年学期）
        /// </summary>
        static public int GetAllEvaluation_entryCount()
        {
            using (var db = new SEMSDBContext())
            {
                return db.Evaluation_entry.Count();
            }
        }

        /// <summary>
        /// 获取该模块下项目总数
        /// </summary>
        /// <param name="module_id"></param>
        /// <returns></returns>
        static public int GetAllEvaluation_entryCount(string module_id)
        {
            using (var db = new SEMSDBContext())
            {
                return db.Evaluation_entry.Count(x => x.module_id == module_id);
            }
        }

        /// <summary>
        /// 获取所有项目列表（所有学年学期）
        /// </summary>
        static public List<Evaluation_entry> GetAllEvaluation_entryList()
        {
            using (var db = new SEMSDBContext())
            { 
                return db.Evaluation_entry.ToList();
            }
        }

        static public List<Evaluation_entry> GetAllEvaluation_entryList(string module_id)
        {
            using (var db = new SEMSDBContext())
            {
                return db.Evaluation_entry.Where(x => x.module_id == module_id).ToList();
            }
        }
        /// <summary>
        /// 指定学年学期，获取项目列表
        /// </summary>
        static public List<Evaluation_entry> GetEvaluation_entryList(string entry_school_year, string entry_semester)
        {
            using (var db = new SEMSDBContext())
            {
                return db.Evaluation_entry.Where(x => x.entry_school_year == entry_school_year && x.entry_semester == entry_semester).ToList();
            }
        }

        /// <summary>
        /// 获取该模块下的项目列表（默认为系统表的学年学期）
        /// </summary>
        /// <param name="module_id">模块ID</param>
        /// <returns></returns>
        static public List<Evaluation_entry> GetCurEvaluation_entryList(string module_id)
        {
            using (var db = new SEMSDBContext())
            {
                var temp = db.Sysinfo.FirstOrDefault();
                return GetEvaluation_entryList(module_id, temp.sysinfo_school_year, temp.sysinfo_semester);
                
                //return db.Evaluation_entry.Where(x => x.entry_school_year == temp.sysinfo_school_year && x.entry_semester == temp.sysinfo_semester
                //    && x.Module.module_id == module_id).ToList();
            }
        }

        /// <summary>
        /// 指定学年学期，获取获取该模块下的项目列表
        /// </summary>
        /// <param name="module_id">模块ID</param>
        /// <param name="entry_school_year">测评学年</param>
        /// <param name="entry_semester">测评学期</param>
        /// <returns></returns>
        static public List<Evaluation_entry> GetEvaluation_entryList(string module_id, string entry_school_year, string entry_semester)
        {
            using (var db = new SEMSDBContext())
            {
                return db.Evaluation_entry.Where(x => x.entry_school_year == entry_school_year && x.entry_semester == entry_semester
                    && x.Module.module_id == module_id).ToList();
            }
        }

        /// <summary>
        /// 获取项目的成绩清单
        /// </summary>
        /// <param name="entry_id">项目ID</param>
        static public List<Entry_score> GetEntry_scoreList(int entry_id)
        {
            using (var db = new SEMSDBContext())
            {
                return db.Entry_score.Where(x => x.entry_id == entry_id).ToList();
            }
        }

        /// <summary>
        /// 添加新测评项目
        /// </summary>
        /// <param name="module_id">要添加的项目</param>
        /// <returns></returns>
        static public bool AddEvaluation_entry(Evaluation_entry model)
        {
            try
            {
                using (var db = new SEMSDBContext())
                {
                    db.Evaluation_entry.Add(model);
                    db.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 修改测评项目
        /// </summary>
        static public bool ModifyEvaluation_entry(int entry_id, Evaluation_entry model)
        {
            try
            {
                using (var db = new SEMSDBContext())
                {
                    var temp = db.Evaluation_entry.Find(entry_id);
                    //db.Evaluation_entry.Attach(temp);
                    temp.entry_date = model.entry_date;
                    temp.entry_description = model.entry_description;
                    temp.entry_school_year = model.entry_school_year;
                    temp.entry_semester = model.entry_semester;
                    temp.module_id = model.module_id;
                    db.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 删除测评项目
        /// </summary>
        static public bool DelEvaluation_entry(int entry_id)
        {
            try
            {
                using (var db = new SEMSDBContext())
                {
                    var temp = db.Evaluation_entry.Find(entry_id);
                    //Entry_scoreBS.AllDelEntry_score(entry_id);
                    db.Evaluation_entry.Remove(temp);
                    db.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 查找测评项目
        /// </summary>
        /// <param name="entry_id">待查找项目的ID</param>
        /// <returns></returns>
        static public Evaluation_entry FindEvaluation_entry ( int entry_id )
        {
            try
            {
                using (var db = new SEMSDBContext())
                {
                    Evaluation_entry temp = db.Evaluation_entry.Find(entry_id);
                    return temp;
                }
            }
            catch
            {
                return null;
            }
        }

        /*
        /// <summary>
        /// 获得学生相应测评年的项目列表
        /// </summary>
        static public List<Evaluation_entry> GetStuEvaluation_entryList2(string entry_school_year, string entry_semester, string student_id)
        {
            using (var db = new SEMSDBContext())
            {
                //return db.Module_score.Find(student_id, module_id).score;
                List<Entry_score> score = Entry_scoreBS.GetStuEntry_scoreList(student_id);
                List<Evaluation_entry> list = new List<Evaluation_entry>();
                foreach (var temp in score)
                {
                    var entry = db.Evaluation_entry.Find(temp.entry_id);
                    if (entry.entry_school_year == entry_school_year && entry.entry_semester == entry_semester)
                        list.Add(entry);
                }
                return list;
            }
        }
        */
        
        /// <summary>
        /// 获取指定学生、测评年的项目
        /// </summary>
        /// <param name="student_id"></param>
        /// <param name="evaluation_year_id">测评年id</param>
        /// <param name="module_id"></param>
        /// <returns></returns>
        static public List<Evaluation_entry> GetStuEvaluation_entryList(string student_id, int evaluation_year_id, string module_id)
        {
            try
            {
                using (var db = new SEMSDBContext())
                {
                    var student = db.Student.Find(student_id);
                    var lk = db.Lk_evaluation_year_classes.FirstOrDefault(x => x.Classes.class_id == student.class_id
                        && x.class_small_id == student.class_small_id
                        && x.evaluation_year_id == evaluation_year_id);

                    return db.Evaluation_entry.Where(x => x.Entry_scores.Any(s=>s.student_id == student_id)
                        && x.module_id == module_id
                        && x.entry_school_year == lk.evaluation_school_year
                        && x.entry_semester == lk.evaluation_semester).ToList();

                }
            }
            catch
            {
                return null;
            }
        }

        static public List<Evaluation_entry> GetOldEvaluation_entry()
        {
            var system = BLL.SysinfoBS.GetSysinfo();
            using (var db = new SEMSDBContext())
            {
                return db.Evaluation_entry.Where(x=>x.entry_school_year!=system.sysinfo_school_year||x.entry_semester!=system.sysinfo_semester).ToList();
            }
        }
    }
}