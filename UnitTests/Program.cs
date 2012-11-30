using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SEMS.DAL;
using SEMS;
using SEMS.Models;
using SEMS.BLL;

namespace UnitTests
{
    class Program
    {
        static void Main(string[] args)
        {
            /*Student temp = new Student()
            {
                class_id = "131111",
                class_small_id = "02",
                student_id = "13111089",
                student_name = "张晖",
                student_sex = "男"
            };
            //Tests.TestStudentBS.TestStuAdd(temp);
            List<Student> list=StudentBS.GetStudentList();
            foreach (var stu in list)
            {
                Console.WriteLine("{0}   {1}", stu.student_id, stu.student_name);
            }*/
            //Tests.TestAdminBS.TestChangePassword();
            //Tests.TestClassesBS.TestAddClass();
            //Tests.TestClassesBS.TestGetClassList();
            //Tests.TestEntry_score.TestAddAndModify();
            //Tests.TestMoude_sorceBS.TestAddModule_sorce();
            /*List<string> list = ModuleBS.GetModuleList();
            foreach (var temp in list)
            {
                Console.WriteLine(temp);
            }*/
            /*using (var db = new SEMSDBContext())
            {
                Entry_score model = new Entry_score()
                {
                    entry_id = 1,
                    score = 2,
                    student_id = "13111089",
                    //Student = db.Student.FirstOrDefault(x => x.student_id == "13111089"),
                    //Evaluation_entry = db.Evaluation_entry.FirstOrDefault(x => x.entry_id == 3)
                };
                //Entry_scoreBS.AddEntry_score(model);
                //Entry_scoreBS.DelEntry_score("13111089", 1);
                //Console.WriteLine(Entry_scoreBS.ImportEntry_score(1,@"E:\暑期项目\综合测评管理系统需求\导入系统表.xlsx"));
                List<Entry_score> list1 = Entry_scoreBS.GetStuEntry_score("13111089");
                foreach (var temp1 in list1)
                {
                    Console.WriteLine(temp1.entry_id + " " + temp1.score);
                }
            }*/
            //Console.WriteLine(Entry_scoreBS.GetEntry_score("13111089", 1));
            /*Console.WriteLine(StudentBS.ImportStudent("131111","02",@"E:\暑期项目\综合测评管理系统需求\13111102名单.xlsx"));
            List<Student> list = StudentBS.GetStudentList("131111", "02");
            foreach (var temp in list)
            {
                Console.WriteLine(temp.student_id+" "+temp.student_name);
            }*/
            //Tests.TestEntry_score.TestAddAndModify();
            //Classes classes = ClassesBS.FindClass("131011", "01");
            //Console.WriteLine(classes.class_id);

            //var ttt = ClassesBS.GetClassCount();

            var t = PolicyBS.GetPolicyList();
        }
    }
}
