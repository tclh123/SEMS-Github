using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SEMS.Controllers
{
    public class NewsController : Controller
    {
        //
        // GET: /News/

        public ActionResult Index()
        {
            var newslist = BLL.NewsBS.GetNewsList();
            return View(newslist);
        }

        public ActionResult ShowNews ( int news_id )
        {
            SEMS.Models.News news = BLL.NewsBS.GetNewsList(news_id);
            return View(news);
        }
         
    }
}
