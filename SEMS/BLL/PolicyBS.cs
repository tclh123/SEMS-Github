using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SEMS.Models;
using SEMS.DAL;
using System.Data.Entity.Validation;

namespace SEMS.BLL
{
    public class PolicyBS
    {
        /// <summary>
        /// 获取所有政策列表
        /// </summary>
        static public List<Policy> GetPolicyList()
        {
            using (var db = new SEMSDBContext())
            {
                var query = from p in db.Policy
                            where p.module_id == "M1"
                            select p;
                return db.Policy.ToList();
            }
        }

        /// <summary>
        /// 获取相应模块的政策
        /// </summary>
        static public Policy GetPolicy(string module_id)
        {
            using (var db = new SEMSDBContext())
            {
                return db.Policy.FirstOrDefault(x => x.module_id == module_id);
            }
        }

        /// <summary>
        /// 修改相应模块的政策
        /// </summary>
        static public bool ModifyPolicy(string module_id, Policy model)
        {
            try
            {
                using (var db = new SEMSDBContext())
                {
                    var temp = db.Policy.Find(module_id);
                    temp.policy_basic = model.policy_basic;
                    temp.policy_excellent = model.policy_excellent;
                    temp.policy_good = model.policy_good;
                    temp.policy_pass = model.policy_pass;

                    //model.module_id = temp.module_id;
                    //model.Module = temp.Module;

                    db.SaveChanges();
                }
                return true;
            }
            catch(DbEntityValidationException dbEx)
            {
                return false;
            }
        }
    }
}
