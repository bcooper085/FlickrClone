using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FlickrClone.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace FlickrClone.Controllers
{
    public class CommentController : Controller
    {
        private readonly FlickrCloneDbContext _db;
        private readonly UserManager<User> _userManager;

        public CommentController(UserManager<User> userManager, FlickrCloneDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(int postId, string Body)
        {
            var comment = new Comment();
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            comment.Post = _db.Posts
                .FirstOrDefault(post => post.PostId == postId);
            comment.User = currentUser;
            _db.Comments.Add(comment);
            _db.SaveChanges();
            return RedirectToAction("Index", "Post", null);
        }
    }
}
