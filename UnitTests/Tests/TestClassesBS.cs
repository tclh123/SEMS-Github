using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SEMS.Models;
using SEMS.BLL;

namespace UnitTests.Tests
{
    public class TestClassesBS
    {
        /// <summary>
        /// 获取所有班级列表
        /// </summary>
        /// <returns></returns>
        static public void TestGetClassList()
        {
            List<Classes> list = ClassesBS.GetClassList();

            foreach (var e in list)
            {
                Console.WriteLine("{0}", e.class_id_tot);
            }
        }

        static public void TestAddClass()
        {
            Console.WriteLine(ClassesBS.AddClass(new Classes()
                {
                    class_id="131111",
                    class_small_id="02",
                    class_grade="2011"
                }));
        }
    }
}
