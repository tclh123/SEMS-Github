using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SEMS.Models;
using SEMS.DAL;
using System.Data;
using System.Diagnostics;
using System.Data.Entity.Validation;

namespace SEMS.BLL
{
    public class ClassesBS
    {
        /// <summary>
        /// 获取所有班级数量
        /// </summary>
        /// <returns></returns>
        static public int GetClassCount()
        {
            using (var db = new SEMSDBContext())
            {
                return db.Classes.Count();
            }
        }

        /// <summary>
        /// 获取所有班级列表
        /// </summary>
        /// <returns></returns>
        static public List<Classes> GetClassList()
        {
            using (var db = new SEMSDBContext())
            {
                return db.Classes.ToList();
            }
        }

        /// <summary>
        /// 获取所有大班号，不重复
        /// </summary>
        /// <returns></returns>
        static public List<string> GetClassIdList()
        {
            using (var db = new SEMSDBContext())
            {
                return db.Classes.Select(x => x.class_id).Distinct().ToList();    //Select是投影操作
            }
        }

        /// <summary>
        /// 获取所有小班号，不重复
        /// </summary>
        /// <returns></returns>
        static public List<string> GetSmall_ClassIdList ( )
        {
            using (var db = new SEMSDBContext())
            {
                return db.Classes.Select(x => x.class_small_id).Distinct().ToList();    //Select是投影操作
            }
        }

        /// <summary>
        /// 添加新班级
        /// </summary>
        ///<returns>成功返回true</returns>
        static public bool AddClass(Classes model)
        {
            try
            {
                using (var db = new SEMSDBContext())
                {
                    /*var classes = new Classes()
                    {
                        class_grade = model.class_grade,
                        class_id = model.class_id,
                        class_small_id = model.class_small_id
                    };*/
                    db.Classes.Add(model);
                    db.SaveChanges();
                }
                return true;
            }
            catch (DbEntityValidationException e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
            catch
            {
                return false;
            }
        }

         /// <summary>
        /// 修改班级信息
        /// </summary>
        ///<returns>成功返回true</returns>
        static public bool ModifyClass(string main_id, string small_id, Classes newModel)
        {
            try
            {
                using (var db = new SEMSDBContext())
                {
                    Classes toChange = db.Classes.Find(main_id, small_id);
                    toChange.class_grade = newModel.class_grade;
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
        /// 修改班级信息
        /// </summary>
        ///<returns>成功返回true</returns>
        static public bool ModifyClass(string class_id, string class_small_id, string newClass_grade)
        {
            try
            {
                using (var db = new SEMSDBContext())
                {
                    var classes = db.Classes.Find(class_id, class_small_id);  //.FirstOrDefault(x => x.class_id == class_id);
                    classes.class_grade = newClass_grade;
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
        /// 删除班级
        /// </summary>
        ///<returns>成功返回true</returns>
        static public bool DelClass(string class_id, string class_small_id)
        {
            try
            {
                using (var db = new SEMSDBContext())
                {
                    var toDel=db.Classes.Find(class_id,class_small_id);         //所有需要级联更新删除的地方数据库里都建好了。。不用这样写，删就是了
                                                                        //... on delete cascade on update cascade)...

                    //foreach (var temp in db.Student.Where(x => x.class_id == class_id && x.class_small_id == class_small_id))
                    //{
                    //    StudentBS.DelStudent(temp.student_id);
                    //    db.Student.Remove(temp);
                    //}
                    //foreach (var temp in db.Lk_evaluation_year_classes.Where(x => x.class_id == class_id && x.class_small_id == class_small_id))
                    //{
                    //    Lk_evaluation_year_classesBS.DelLk_evaluation_year_classes(temp.evaluation_year_id, temp.class_id, temp.class_small_id);
                    //    db.Lk_evaluation_year_classes.Remove(temp);
                    //}
                    db.Classes.Remove(toDel);
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
        /// 查找班级
        /// </summary>
        static public Classes FindClass(string class_id, string class_small_id)
        {
            try
            {
                using (var db = new SEMSDBContext())
                {
                    return db.Classes.Find(class_id, class_small_id);
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
