using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SEMS.DAL;
using SEMS.Models;

namespace SEMS.BLL
{
    public class Module_scoreBS
    {
        /// <summary>
        /// 返回模块成绩
        /// </summary>
        static public int GetMoudle_score(string student_id, string module_id, string evaluation_school_year, string evaluation_semester)
        {
            using (var db = new SEMSDBContext())
            {
                var score = db.Module_score.FirstOrDefault(x => x.student_id == student_id && x.module_id == module_id && x.evaluation_school_year == evaluation_school_year && x.evaluation_semester == evaluation_semester);
                return score == null ? 0 : score.score;     //如果找不到总成绩，说明成绩为0！
            }
        }
        
        /// <summary>
        /// 根据测评年返回模块总分
        /// </summary>
        /// <param name="student_id">学号</param>
        /// <param name="evaluation_year_id">测评年id</param>    //用ID即可
        /// <param name="module_id">模块ID</param>
        /// <returns>返回总分;发生异常返回-1</returns>
        static public int GetModule_scoreOfYear(string student_id, int evaluation_year_id, string module_id)
        { 
            try
            {
                using (var db = new SEMSDBContext())
                {
                    var student = db.Student.Find(student_id);
                    var lk = db.Lk_evaluation_year_classes.FirstOrDefault(x=>x.Classes.class_id == student.class_id
                        && x.class_small_id == student.class_small_id 
                        && x.evaluation_year_id == evaluation_year_id);
                    return Module_scoreBS.GetMoudle_score(student_id, module_id, lk.evaluation_school_year, lk.evaluation_semester);
                        //db.Module_score.FirstOrDefault(x => x.student_id == student_id
                        //&& x.module_id == module_id
                        //&& x.evaluation_school_year == lk.evaluation_school_year
                        //&& x.evaluation_semester == lk.evaluation_semester)
                        //.score;
                }
            }
            catch
            {
                return -1;
            }
        }

        //模块总分表写了触发器了！自动维护！
        /*
        /// <summary>
        /// 更新模块成绩，把某学生的模块成绩设为所有项目得分之和
        /// 
        /// 
        /// </summary>
        static public bool RefrushModule_score(string student_id, string module_id, string evaluation_school_year, string evaluation_semester)
        {
            try
            {
                using (var db = new SEMSDBContext())
                {
                    int score = 0;
                    var entrys = db.Evaluation_entry.Where(x => x.module_id == module_id && x.entry_school_year == evaluation_school_year && x.entry_semester == evaluation_semester);  //查询别放在循环里
                    foreach (var model in entrys)
                    {
                        score += db.Entry_score.FirstOrDefault(x => x.entry_id == model.entry_id).score;
                    }
                    //db.Module_score.FirstOrDefault(x => x.student_id == student_id && x.module_id == module_id && x.evaluation_school_year == evaluation_school_year && x.evaluation_semester == evaluation_semester).score = score;
                    
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
        /// 添加模块成绩
        /// </summary>
        static public bool AddModule_score(Module_score model)
        {
            try
            {
                using (var db = new SEMSDBContext())
                {
                    db.Module_score.Add(model);
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
        /// 修改模块成绩
        /// </summary>
        static public bool ModifyModule_score(string student_id, string module_id, string evaluation_school_year, string evaluation_semester, Module_score model)
        {
            try
            {
                using (var db = new SEMSDBContext())
                {
                    var temp = db.Module_score.FirstOrDefault(x => x.student_id == student_id && x.module_id == module_id && x.evaluation_school_year == evaluation_school_year && x.evaluation_semester == evaluation_semester);
                    //temp.module_id = model.module_id;
                    //temp.student_id = model.student_id;       //修改模块ID跟学号是不合常理的。
                    temp.score = model.score;
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
        /// 删除模块成绩
        /// </summary>
        static public bool DelModule_score(string student_id, string module_id, string evaluation_school_year, string evaluation_semester)
        {
            try
            {
                using (var db = new SEMSDBContext())
                {
                    var temp = db.Module_score.FirstOrDefault(x => x.student_id == student_id && x.module_id == module_id && x.evaluation_school_year == evaluation_school_year && x.evaluation_semester == evaluation_semester);
                    db.Module_score.Remove(temp);
                    db.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
         * */
    }
}