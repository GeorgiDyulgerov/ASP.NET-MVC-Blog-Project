using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using BlogProject.Extensions;
using BlogProject.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebGrease.Css.Extensions;

namespace BlogProject.Controllers
{
    public class CommentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Comments
        [Authorize(Roles = "Administrators")]
        public ActionResult Index()
        {
             List<CommentIndexPageModel> models = new List<CommentIndexPageModel>();
            var comments = db.Comments.Include(c => c.User).ToList();
            var posts = db.Posts.ToList();
            foreach (var comment in comments)
            {
                CommentIndexPageModel  model = new CommentIndexPageModel();
                model.Comments = comment;
                model.Posts = posts.Find(p=>p.Id==comment.PostId);
                models.Add(model);

            }
            
            
            
            return View(models);
        }

        // GET: Comments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // GET: Comments/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Text,Date,Author,PostId")] Comment comment)
        {
            comment.Date = DateTime.Now;
            UserManager<ApplicationUser> UserManeger = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            ApplicationUser user = UserManeger.FindById(this.User.Identity.GetUserId());
            comment.User = user;

            var post = db.Posts.Find(comment.PostId);
            if (ModelState.IsValid)
            {
                post.Comments.Add(comment);
                db.Comments.Add(comment);
                db.SaveChanges();
                this.AddNotification("Comment Created", NotificationType.SUCCESS);
                return Redirect("/Posts/Details/"+comment.PostId);
            }

            return View(comment);
        }

        // GET: Comments/Edit/5
        [Authorize(Roles = "Administrators")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Administrators")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Text,Date,Author")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                this.AddNotification("Comment Edited", NotificationType.SUCCESS);
                return RedirectToAction("Index");
            }
            return View(comment);
        }

        // GET: Comments/Delete/5
        [Authorize(Roles = "Administrators")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Comments/Delete/5
        [Authorize(Roles = "Administrators")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
            db.SaveChanges();
            this.AddNotification("Comment Deleted", NotificationType.INFO);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
