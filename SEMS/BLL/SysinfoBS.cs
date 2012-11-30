using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SEMS.DAL;
using SEMS.Models;

namespace SEMS.BLL
{
    /// <summary>
    /// 系统表，有效ID始终为0
    /// </summary>
    public class SysinfoBS
    {
        /// <summary>
        /// 获取当前系统表
        /// </summary>
        static public Sysinfo GetSysinfo()
        {
            using (var db = new SEMSDBContext())
            {
                return db.Sysinfo.FirstOrDefault();
            }
        }

        /// <summary>
        /// 修改系统表
        /// </summary>
        static public bool ModifySysinfo(Sysinfo model)
        {
            try
            {
                using (var db = new SEMSDBContext())
                {
                    var temp = db.Sysinfo.FirstOrDefault();
                    temp.sysinfo_id = model.sysinfo_id;
                    temp.sysinfo_school_year = model.sysinfo_school_year;
                    temp.sysinfo_semester = model.sysinfo_semester;
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