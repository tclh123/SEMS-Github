using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SEMS.DAL;
using SEMS.Models;

namespace SEMS.BLL
{
    public class NewsBS
    {
        /// <summary>
        /// 获取所有公告列表
        /// </summary>
        static public List<News> GetNewsList()
        {
            using (var db = new SEMSDBContext())
            {
                return db.News.ToList();
            }
        }

        /// <summary>
        /// 添加新公告
        /// </summary>
        static public bool AddNews(News model)
        {
            try
            {
                using (var db = new SEMSDBContext())
                {
                    db.News.Add(model);
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception ee)
            {
                throw ee.InnerException;
                //return false;
            }
        }

        /// <summary>
        /// 修改公告
        /// </summary>
        static public bool ModifyNews(int news_id, News model)
        {
            try
            {
                using (var db = new SEMSDBContext())
                {
                    var temp = db.News.Find(news_id);
                    temp.admin_id = model.admin_id;
                    temp.new_date = model.new_date;
                    temp.news_content = model.news_content;
                    temp.news_title = model.news_title;
                    db.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        ///  删除公告
        /// </summary>
        static public bool DelNews(int news_id)
        {
            try
            {
                using (var db = new SEMSDBContext())
                {
                    var temp = db.News.Find(news_id);
                    db.News.Remove(temp);
                    db.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 获取特定公告
        /// </summary>
        static public News GetNewsList ( int news_id )
        {
            using (var db = new SEMSDBContext())
            {
                return db.News.Find(news_id);
            }
        }
    }
}