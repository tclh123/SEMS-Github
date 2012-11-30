using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SEMS.Models;
using SEMS.BLL;
using SEMS.DAL;

namespace UnitTests.Tests
{
    class TestMoude_sorceBS
    {
        static public void TestAddModule_sorce()
        {
            Entry_score temp=new Entry_score()
            {
                entry_id=1,
                student_id="13111089",
                score=5
            };
            Entry_scoreBS.AddEntry_score(temp);
        }
    }
}
