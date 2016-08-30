using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogProject.Models;
using Microsoft.Ajax.Utilities;

namespace BlogProject.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var homePage =new HomePageModel();
            var posts = db.Posts.Include(p =>p.Author).OrderByDescending(post => post.Date).Take(5).ToList();
            var tags = db.Tags.ToList();
            SortedDictionary<string, int> tagsCount = new SortedDictionary<string, int>();
            foreach (var tag in tags)
            {
                if (!tagsCount.ContainsKey(tag.Name))
                {
                    tagsCount.Add(tag.Name,1);
                }
                else
                {
                    tagsCount[tag.Name]++;
                }
            }
            
            homePage.Post = posts;
            homePage.TagCount = tagsCount;

            return View(homePage);
        }

    }
}