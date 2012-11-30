using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SEMS.DAL;
using SEMS.Models;

namespace SEMS.BLL
{
    public class Lk_evaluation_year_classesBS
    {
        /// <summary>
        /// 添加测评年班级链接表
        /// </summary>
        static public bool AddLk_evaluation_year_classes(Lk_evaluation_year_classes model)
        {
            try
            {
                using (var db = new SEMSDBContext())
                {
                    db.Lk_evaluation_year_classes.Add(model);
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
        /// 修改测评年班级链接表
        /// </summary>
        static public bool ModifyLk_evaluation_year_classes(int lk_evaluation_year_classes, string class_id, string class_small_id, Lk_evaluation_year_classes model)
        {
            try
            {
                using (var db = new SEMSDBContext())
                {
                    var temp = db.Lk_evaluation_year_classes.Find(lk_evaluation_year_classes, class_id, class_small_id);
                    temp.class_id = model.class_id;
                    temp.class_small_id = model.class_small_id;
                    temp.evaluation_school_year = model.evaluation_school_year;
                    temp.evaluation_semester = model.evaluation_semester;
                    temp.evaluation_year_id = model.evaluation_year_id;
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
        /// 删除测评年班级链接表
        /// </summary>
        static public bool DelLk_evaluation_year_classes(int evaluation_year_id, string class_id, string class_small_id)
        {
            try
            {
                using (var db = new SEMSDBContext())
                {
                    var temp = db.Lk_evaluation_year_classes.Find(evaluation_year_id, class_id, class_small_id);
                    db.Lk_evaluation_year_classes.Remove(temp);
                    db.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}