using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SEMS.Models;
using SEMS.BLL;
using SEMS.DAL;

namespace UnitTests.Tests
{
    class TestStudentBS
    {
        static public void TestStuAdd(Student model)
        {
            StudentBS.AddStudent(model);
        }
    }
}
