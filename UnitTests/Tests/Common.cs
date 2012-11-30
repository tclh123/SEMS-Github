using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Data;
using SEMS.DAL;
using System.Linq.Expressions;

namespace UnitTests.Tests
{
    public class Common<TEntity> where TEntity : class
    {
        // 不行。 Context跟取出时的Context不一样
        //public static void PrintValues(TEntity entity)
        //{
        //    using (var db = new SEMSDBContext())
        //    {
                
        //        var values = db.Entry<TEntity>(entity).CurrentValues;
        //        foreach (var propertyName in values.PropertyNames)
        //        {
        //            Console.WriteLine("{0}={1}", propertyName, values[propertyName]);
        //        }
        //    }
        //}
    }
}
