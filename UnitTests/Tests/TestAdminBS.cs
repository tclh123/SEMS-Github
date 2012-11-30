using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SEMS.Models;
using SEMS.BLL;
using SEMS.DAL;

namespace UnitTests.Tests
{
    class TestAdminBS
    {
        static public void TestChangePassword()
        {
            using (var db = new SEMSDBContext())
            {
                Administrater temp = db.Administrater.FirstOrDefault();
                Console.WriteLine(temp.admin_pwd);
                //AdministraterBS.ChangePassword(temp.admin_id, "123456", temp.admin_pwd);
                //Console.WriteLine("{0}", temp.admin_pwd);
            }
        }
    }
}
