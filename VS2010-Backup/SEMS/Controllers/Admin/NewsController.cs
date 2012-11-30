using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using SEMS.Filters;

namespace SEMS.Controllers.Admin
{
    [VaildateLogin]
    public class NewsController : Controller
    {
        //
        // GET: /News/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SetNews ( )
        {
            return View();
        }

        [HttpPost]
        public ActionResult SetNews ( SEMS.Models.News model )
        {
            model.new_date = DateTime.Now;
            model.admin_id = User.Identity.Name;
            if (BLL.NewsBS.AddNews(model))
            {
                return RedirectToAction("Index");
            }
            else
                ModelState.AddModelError("", "发布失败!");
            return View(model);
        }

        public ActionResult NewsDetails(int? page)
        {
            var newslist = BLL.NewsBS.GetNewsList();
            int pageSize = 5;  //一页显示5个
            int pageNumber = (page ?? 1);       // ( page ?? 1 ) 意味着如果 page 有值得话返回这个值，如果是 null 的话，返回 1
            return View(newslist.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult EditorNews ( int id )
        {
            SEMS.Models.News news = BLL.NewsBS.GetNewsList(id);
            return View(news);
        }

        [HttpPost]
        public ActionResult EditorNews ( int id , SEMS.Models.News model )
        {
            model.admin_id = User.Identity.Name;
            if (BLL.NewsBS.ModifyNews(id, model))
            {
                return RedirectToAction("Index");
            }
            else
                ModelState.AddModelError("", "编辑失败!");

            return View(model);
        }

        public ActionResult DeleteNews ( int id )
        {
            BLL.NewsBS.DelNews(id);
            return RedirectToAction("Index");
        }
    }
    
}
