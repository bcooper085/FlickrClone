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
    }
}
