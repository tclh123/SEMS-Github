using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SEMS.DAL;
using SEMS.Models;

namespace SEMS.BLL
{
    public class ModuleBS
    {
        /// <summary>
        /// 返回模块ID
        /// </summary>
        static public List<string> GetModuleIDList()
        {
            try
            {
                using (var db = new SEMSDBContext())
                {
                    List<string> list = new List<string>();
                    foreach (var temp in db.Module)
                    {
                        //return temp.module_id
                        list.Add(temp.module_id);
                    }
                    return list;
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 返回全部模块
        /// </summary>
        static public List<Module> GetModuleList ( )
        {
            try
            {
                using (var db = new SEMSDBContext())
                {
                    return db.Module.ToList();
                }
            }
            catch
            {
                return null;
            }
        }
    }
}