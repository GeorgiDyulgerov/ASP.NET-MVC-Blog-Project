using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogProject.Models;

namespace BlogProject.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {

            var posts = db.Posts.Include(p =>p.Author).OrderByDescending(post => post.Date).Take(5);
           
            return View(posts.ToList());
        }

    }
}