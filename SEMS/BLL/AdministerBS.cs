using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SEMS.Models;
using SEMS.DAL;
using System.Data;

namespace SEMS.BLL
{
    public class AdministraterBS
    {
        /// <summary>
        /// 验证管理员用户名密码
        /// </summary>
        /// <returns></returns>
        static public bool IsValid(string uid, string pwd)
        {
            using (var db = new SEMSDBContext())
            {
                return db.Administrater.Any(x => x.admin_id == uid && x.admin_pwd == pwd);
            }
        }

        /// <summary>
        /// 获取管理员信息
        /// </summary>
        /// <returns></returns>
        static public Administrater GetAdministrater ( string uid )
        {
            using (var db = new SEMSDBContext())
            {
                return db.Administrater.Find(uid);
            }
        }

        /// <summary>
        /// 修改管理员密码
        /// </summary>
        static public bool ChangePassword(string uid, string pwd,string oldPwd)
        {
            try
            {
                using (var db = new SEMSDBContext())
                {
                    var admin = db.Administrater.FirstOrDefault(x => x.admin_id == uid);
                    if (admin == null) return false;
                    admin.admin_pwd = pwd;
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
        /// 添加管理员用户
        /// </summary>
        static public bool AddAdministrater(Administrater model)
        {
            try
            {
                using (var db = new SEMSDBContext())
                {
                    db.Administrater.Add(model);
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
        /// 修改管理员身份描述
        /// </summary>
        static public bool ModifyAdmin_descr(string uid,string descr)
        {
            try
            {
                using (var db = new SEMSDBContext())
                {
                    db.Administrater.FirstOrDefault(x => x.admin_id == uid).admin_descr = descr;
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
        /// 删除管理员账户
        /// </summary>
        static public bool DelAdministrater(string uid)
        {
            try
            {
                using (var db = new SEMSDBContext())
                {
                    var temp = db.Administrater.Find(uid);
                    db.Administrater.Remove(temp);
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