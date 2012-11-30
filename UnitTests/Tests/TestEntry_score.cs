using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SEMS.Models;
using SEMS.BLL;


namespace UnitTests.Tests
{
    class TestEntry_score
    {
        static public void TestAddAndModify()
        {
            Entry_score test = new Entry_score()
            {
               entry_id = 1,
              student_id = "13111089",
              score = 4
             };
            //Entry_scoreBS.ModifyEntry_score(test);
            Console.WriteLine(Entry_scoreBS.AddEntry_score(test));
            List<Entry_score> list = Evaluation_entryBS.GetEntry_scoreList(1);
            foreach (var x in list)
            {
                Console.WriteLine(x.student_id);
            }
        }
    }
}
