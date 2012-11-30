using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SEMS.Models;
using SEMS.DAL;
using System.Data.OleDb;
using System.Data;
using System.Reflection;

namespace SEMS.BLL
{
    public class StudentBS
    {
        /// <summary>
        /// 获取所有学生数量
        /// </summary>
        /// <returns>数量</returns>
        static public int GetStudentCount()
        {
            using (var db = new SEMSDBContext())
            {
                return db.Student.Count();
            }
        }

        /// <summary>
        /// 获取所有学生列表
        /// </summary>
        /// <returns>学生列表</returns>
        static public List<Student> GetStudentList()
        {
            using (var db = new SEMSDBContext())
            {
                return db.Student.ToList();
            }
        }

        /// <summary>
        /// 获取全班学生
        /// </summary>
        /// <param name="class_id">大班号</param>
        /// <returns>全班学生列表</returns>
        static public List<Student> GetStudentList(string class_id)
        {
            using (var db = new SEMSDBContext())
            {
                return db.Student.Where(x => x.class_id == class_id).ToList();
            }
        }

        /// <summary>
        /// 获取全班学生
        /// </summary>
        /// <param name="class_id">大班号</param>
        /// <param name="class_small_id">小班号</param>
        /// <returns>全班学生列表</returns>
        static public List<Student> GetStudentList(string class_id, string class_small_id)
        {
            using (var db = new SEMSDBContext())
            {
                return db.Student.Where(x => x.class_id == class_id && x.class_small_id == class_small_id).ToList();
            }
        }

        /// <summary>
        /// 返回指定班级里的学生数量
        /// </summary>
        /// <param name="class_id">大班号</param>
        /// <param name="small_class_id">小班号</param>
        /// <returns>数量</returns>
        static public int GetStudentCountOfClass(string class_id, string small_class_id)
        {
            using (var db = new SEMSDBContext())
            {
                //return db.Student.Count(x => x.class_id == class_id && x.class_small_id == small_class_id);
                return db.Student.Where(x => x.class_id == class_id && x.class_small_id == small_class_id).Count();
            }
        }

        /// <summary>
        /// 添加新学生
        /// </summary>
        /// <param name="model">要添加的学生</param>
        /// <returns>成功返回true</returns>
        static public bool AddStudent(Student model)
        {
            try
            {
                using (var db = new SEMSDBContext())
                {
                    db.Student.Add(model);
                    db.SaveChanges();
                }
                return true;
            }
            catch// (Exception ee)
            {
                //throw ee.InnerException;
                return false;
            }
        }

        /// <summary>
        /// 修改学生信息
        /// </summary>
        /// <param name="class_id">学号</param>
        /// <param name="model">修改后的学生信息</param>
        /// <returns>成功返回true</returns>
        static public bool ModifyStudent(string student_id, Student model)
        {
            try
            {
                using (var db = new SEMSDBContext())
                {
                    Student temp = db.Student.Find(student_id);
                    //db.Student.Attach(temp);
                    temp.student_name = model.student_name;
                    temp.student_sex = model.student_sex;
                    temp.class_id = model.class_id;
                    temp.class_small_id = model.class_small_id;
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
        /// 删除学生信息
        /// </summary>
        /// <param name="class_id">学号</param>
        /// <returns>成功返回true</returns>
        static public bool DelStudent(string student_id)
        {
            try
            {
                using (var db = new SEMSDBContext())
                {
                    var temp = db.Student.Find(student_id);
                    db.Student.Remove(temp);
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
        /// 查找学生信息
        /// </summary>
        /// <param name="class_id">学号</param>
        /// <returns>成功返回true</returns>
        static public Student FindStudent ( string student_id )
        {
            try
            {
                using (var db = new SEMSDBContext())
                {
                    var temp = db.Student.Find(student_id);
                    return temp;
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 从Excel批量导入学生信息
        /// 说明：必须是“Sheet1”，必须有姓名、学号、性别列。
        /// </summary>
        /// <returns>成功添加或修改的学生个数，若出错返回-1</returns>
        static public int ImportStudent(string class_id, string class_small_id, string fileName)
        {
            try
            {
                int ret = 0;
                using (var db = new SEMSDBContext())
                {
                    //string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Extended Properties='Excel 12.0;HDR=YES;IMEX=1'Data Source=" + fileName + ";";  //HDR=Yes，代表第一行是标题，不做为数据使用 ，如果用HDR=NO，则表示第一行不是标题，做为数据来使用。系统默认的是YES。IMEX=1，表示只读
                    string strConn = "Provider=Microsoft.ACE.OLEDB.12.0; Persist Security Info=False;Data Source=" + fileName + "; Extended Properties='Excel 12.0;HDR=Yes;IMEX=1'";
                    using (OleDbConnection conn = new OleDbConnection(strConn))
                    {

                        try
                        {
                            conn.Open();
                            string sql = "select * from [Sheet1$]";
                            OleDbCommand cmd = new OleDbCommand(sql, conn);
                            OleDbDataReader dr = cmd.ExecuteReader();
                            while (dr.Read())
                            {
                                Student student = new Student()
                                {
                                    student_name = dr["姓名"].ToString(),
                                    student_id = dr["学号"].ToString(),
                                    student_sex = dr["性别"].ToString(),
                                    class_id = class_id,
                                    class_small_id = class_small_id
                                };
                                if (StudentBS.AddStudent(student))
                                {
                                    ret++;
                                }
                                else if (StudentBS.ModifyStudent(student.student_id, student))
                                {
                                    ret++;
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            throw e;
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }
                }
                return ret;
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// 批量删除学生信息
        /// </summary>
        static public bool AllDelStudent(string class_id, string class_small_id)
        {
            try
            {
                using (var db = new SEMSDBContext())
                {
                    foreach (var temp in db.Student.Where(x => x.class_id == class_id && x.class_small_id == class_small_id))
                    {
                        db.Student.Remove(temp);
                    }
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
        /// 查询模块不合格学生信息
        /// </summary>
        /// <returns>返回不合格学生的list其中每一项的Module_scores集合里存放了模块成绩信息</returns>
        static public List<Student> FindUnqualifiedStudent()
        {
            try
            {
                List<Student> UnqualifiedStu = new List<Student>();
                Dictionary<string, bool> isExist = new Dictionary<string, bool>();
                using (var db = new SEMSDBContext())
                {
                    foreach (var temp in db.Module_score.Where(x => x.score < db.Policy.FirstOrDefault(y => y.module_id == x.module_id).policy_pass))
                    {
                        if (!isExist[temp.student_id])
                        {
                            Student stu = db.Student.Find(temp.student_id);
                            foreach (var toAdd in db.Module_score.Where(x => x.student_id == temp.student_id))
                            {
                                stu.Module_scores.Add(toAdd);
                            }
                            UnqualifiedStu.Add(stu);
                        }
                    }
                }
                return UnqualifiedStu;
            }
            catch
            {
                return null;
            }
        }
    }
}