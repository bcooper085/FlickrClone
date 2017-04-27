using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FlickrClone.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace FlickrClone.Controllers
{
    public class PostsController : Controller
    {
        private readonly FlickrCloneDbContext _db;
        private readonly UserManager<User> _userManager;

        public PostsController(UserManager<User> userManager, FlickrCloneDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.Posts.ToList());
        }
        public IActionResult Details(int id)
        {
            var thisPost = _db.Posts
                .Include(p => p.Comments)
                .FirstOrDefault(posts => posts.PostId == id);
            return View(thisPost);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(int postId, string Body)
        {
            var comment = new Comment();
            comment.Body = Body;
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            comment.Post = _db.Posts
                .FirstOrDefault(post => post.PostId == postId);
            comment.User = currentUser;
            _db.Comments.Add(comment);
            _db.SaveChanges();
            return RedirectToAction("Details", "Posts",  new { id = postId });
        }

        public IActionResult Edit(int id)
        {
            var thisPost = _db.Posts.FirstOrDefault(post => post.PostId == id);
            return View(thisPost);
        }
        [HttpPost]
        public IActionResult Edit(Post post)
        {
            _db.Entry(post).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            var thisPost = _db.Posts.FirstOrDefault(post => post.PostId == id);
            return View(thisPost);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var thisPost = _db.Posts.FirstOrDefault(post => post.PostId == id);
            _db.Posts.Remove(thisPost);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
