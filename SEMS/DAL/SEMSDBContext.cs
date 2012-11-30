using System;
using System.Collections.Generic;
using System.Data.Entity;
using SEMS.Models;

using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;


namespace SEMS.DAL
{
    public class SEMSDBContext : DbContext
    {
        public DbSet<Classes> Classes { get; set; }
        public DbSet<Student> Student { get; set; }     //不能为Students，因为要与数据库表名一致
        public DbSet<Evaluation_year> Evaluation_year { get; set; }
        public DbSet<Lk_evaluation_year_classes> Lk_evaluation_year_classes { get; set; }
        public DbSet<Module> Module { get; set; }
        public DbSet<Evaluation_entry> Evaluation_entry { get; set; }
        public DbSet<Entry_score> Entry_score { get; set; }
        public DbSet<Module_score> Module_score { get; set; }
        public DbSet<Sysinfo> Sysinfo { get; set; }
        public DbSet<Administrater> Administrater { get; set; }
        public DbSet<Policy> Policy { get; set; }
        public DbSet<News> News { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) 
        { 
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();  //移除建模时自动将表名复数化
        }
    }
}