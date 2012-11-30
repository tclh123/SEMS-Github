using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SEMS.DAL;
using SEMS.Models;

namespace SEMS.BLL
{
    public class Evaluation_yearBS
    {
        /// <summary>
        /// 获得测评年列表
        /// </summary>
        static public List<Evaluation_year> GetEvaluation_yearList()
        {
            using (var db = new SEMSDBContext())
            {
                //var query = from p in db.Evaluation_year
                //            where p.evaluation_year_id==1
                //            select p;

                return db.Evaluation_year.ToList();
            }
        }

        /// <summary>
        /// 查找测评年
        /// </summary>
        static public Evaluation_year GetEvaluation_year( int evaluation_id )
        {
            using (var db = new SEMSDBContext())
            {
                //var query = from p in db.Evaluation_year
                //            where p.evaluation_year_id==1
                //            select p;

                return db.Evaluation_year.Find(evaluation_id);
            }
        }

        /// <summary>
        /// 根据测评年名称返回ID
        /// </summary>
        /// <param name="evaluation_year_name"></param>
        /// <returns></returns>
        static public int GetEvaluation_yearId(string evaluation_year_name)
        {
            try
            {
                using (var db = new SEMSDBContext())
                {
                    return db.Evaluation_year.FirstOrDefault(x => x.evaluation_year_name == evaluation_year_name).evaluation_year_id;
                }
            }
            catch
            {
                return 0;
            }
        }
        /// <summary>
        /// 添加测评年
        /// </summary>
        static public bool AddEvaluation_year(int evaluation_year_id, string evaluation_year_name)
        {
            try
            {
                using (var db = new SEMSDBContext())
                {
                    db.Evaluation_year.Add(new Evaluation_year()
                    {
                        evaluation_year_id = evaluation_year_id,
                        evaluation_year_name = evaluation_year_name
                    });
                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 修改测评年
        /// </summary>
        static public bool ModifyEvaluation_year(int evaluation_year_id, Evaluation_year model)
        {
            try
            {
                using (var db = new SEMSDBContext())
                {
                    var temp = db.Evaluation_year.Find(evaluation_year_id);
                    temp.evaluation_year_id = model.evaluation_year_id;
                    temp.evaluation_year_name = model.evaluation_year_name;
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
        /// 删除测评年
        /// </summary>
        static public bool DelEvaluation_year(int evaluation_year_id)
        {
            try
            {
                using (var db = new SEMSDBContext())
                {
                    var temp = db.Evaluation_year.Find(evaluation_year_id);
                    db.Evaluation_year.Remove(temp);
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