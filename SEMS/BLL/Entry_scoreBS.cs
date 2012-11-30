using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SEMS.DAL;
using SEMS.Models;
using System.Data.OleDb;
using System.Data;
using System.Reflection;
namespace SEMS.BLL
{

    public class Entry_scoreBS
    {
        /// <summary>
        /// 获得指定项目下所有项目成绩
        /// </summary>
        static public List<Entry_score> GetEntry_scoreList(int entry_id)
        {
            using (var db = new SEMSDBContext())
            {
                return db.Entry_score.Where(x => x.Evaluation_entry.entry_id == entry_id).ToList();
            }
        }

        /// <summary>
        /// 获得学生所有项目成绩
        /// </summary>
        static public List<Entry_score> GetStuEntry_scoreList(string student_id)
        {
            using (var db = new SEMSDBContext())
            {
                return db.Entry_score.Where(x => x.student_id == student_id).ToList();
            }
        }

        /// <summary>
        /// 返回一个班级中的项目成绩数量
        /// </summary>
        /// <param name="class_id"></param>
        /// <param name="class_small_id"></param>
        /// <returns></returns>
        static public int GetEntry_scoreCountOfClass(string class_id, string class_small_id)
        {
            using (var db = new SEMSDBContext())
            {
                int count = 0;
                foreach (var stu in db.Student.Where(x => x.class_id == class_id && x.class_small_id == class_small_id).ToList())
                {
                    count += db.Entry_score.Count(x => x.student_id == stu.student_id);
                }
                return count;
            }
        }
        /// <summary>
        /// 获得项目成绩
        /// </summary>
        /// <returns>返回得分;0表示未录入;-1表示出现异常</returns>
        static public int GetEntry_score(string student_id, int entry_id)
        {
            using (var db = new SEMSDBContext())
            {
                try
                {
                    var entry_score = db.Entry_score.Find(student_id, entry_id);
                    return entry_score == null ? 0 : entry_score.score;     //若不存在此成绩则为0
                }
                catch
                {
                    return -1;
                }
            }
        }

        /// <summary>
        /// 添加项目成绩
        /// </summary>
        /// <param name="model">要添加的项目成绩</param>
        ///<returns>成功返回true</returns>
        static public bool AddEntry_score(Entry_score model)
        {
            try
            {
                using (var db = new SEMSDBContext())
                {
                    db.Entry_score.Add(model);
                    /*db.Entry_score.Add(new Entry_score()
                    {
                        entry_id=model.entry_id,
                        score=model.score,
                        student_id=model.student_id
                    });*/
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception e)
            {
                //throw e.InnerException;
                return false;
            }
        }

        /// <summary>
        /// 修改项目成绩
        /// </summary>
        /// <param name="model">修改后的model</param>
        static public bool ModifyEntry_score(string student_id, int entry_id, Entry_score model)
        {
            try
            {
                using (var db = new SEMSDBContext())
                {
                    var temp = db.Entry_score.Find(student_id, entry_id);
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
        /// 修改项目成绩
        /// </summary>
        static public bool ModifyEntry_score ( string student_id, int entry_id, int score )
        {
            try
            {
                using (var db = new SEMSDBContext())
                {
                    var temp = db.Entry_score.Find(student_id, entry_id);
                    temp.score = score;
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
        /// 删除项目成绩
        /// </summary>
        /// <param name="entry_id">项目ID</param>
        /// <param name="student_id">学号</param>
        static public bool DelEntry_score(string student_id, int entry_id)
        {
            try
            {
                using (var db = new SEMSDBContext())
                {
                    var temp = db.Entry_score.FirstOrDefault(x => x.student_id == student_id && x.entry_id == entry_id);
                    db.Entry_score.Remove(temp);
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
        /// 批量修改项目成绩
        /// </summary>
        /// <param name="entry_id">项目ID</param>
        /// <param name="score">待改分数</param>
        static public bool AllModifyEntry_score(int entry_id, int score)
        {
            try
            {
                using (var db = new SEMSDBContext())
                {
                    foreach (var temp in db.Entry_score.Where(x => x.entry_id == entry_id))
                    {
                        temp.score = score;
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
        /// 批量删除项目成绩
        /// </summary>
        static public bool AllDelEntry_score(int entry_id)
        {
            try
            {
                using (var db = new SEMSDBContext())
                {
                    foreach (var temp in db.Entry_score.Where(x => x.entry_id == entry_id))
                    {
                        db.Entry_score.Remove(temp);
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
        /// 从excel批量导入特定项目成绩
        /// 说明：必须是“Sheet1”，必须有学号、得分列。
        /// </summary>
        /// <param name="entry_id">项目ID</param>
        /// <param name="fileName">excel文件的绝对路径</param>
        /// <returns>有效影添加或修改的记录数,为-1表示操作出错</returns>
        static public int ImportEntry_score(int entry_id, string fileName)
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
                                Entry_score score = new Entry_score()
                                {
                                    entry_id = entry_id,
                                    score = int.Parse(dr["得分"].ToString()),// Convert.ToInt32(dr["得分"].ToString()),
                                    student_id = dr["学号"].ToString()
                                };
                                if (Entry_scoreBS.AddEntry_score(score))
                                {
                                    ret++;
                                }
                                else if (Entry_scoreBS.ModifyEntry_score(score.student_id, score.entry_id, score))
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
    }
}
