using Devfunda.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace DevFunda.Controllers
{
    public class blogController : Controller
    {
        private readonly DevfundaDbContext _context;

        public blogController(DevfundaDbContext context)
        {
            _context = context;
        }
        // GET: blog
         public async Task<IActionResult> Index()
        {

            var posts = await _context.Posts
                .Include(p => p.Category)
                .Include(p => p.Author) // if you have an Author FK
                .Where(p => p.IsPublished && p.PublishedAt <= DateTime.Now)
                .OrderByDescending(p => p.PublishedAt)
                .ToListAsync();

            return View(posts);
        }
        [Route("blog/{slug}")]
        public async Task<IActionResult> detail(string slug)
        {
            var post = await _context.Posts
                .Include(p => p.Category)
                .Include(p => p.Author)
                .FirstOrDefaultAsync(p => p.Slug == slug && p.IsPublished);

            if (post == null)
                return NotFound();

            return View(post);
        }



    }
}