using Dapper;
using Devfunda.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Devfunda.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TutorialCategoryController : Controller
    {
        private readonly IConfiguration _config;
        private readonly string _conn;

        public TutorialCategoryController(IConfiguration config)
        {
            _config = config;
            _conn = _config.GetConnectionString("DefaultConnection");
        }

        public async Task<IActionResult> Index()
        {
            using var conn = new SqlConnection(_conn);
            var categories = await conn.QueryAsync<TutorialCategory>("SELECT * FROM TutorialCategories");
            return View(categories);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(TutorialCategory model)
        {
            using var conn = new SqlConnection(_conn);
            var query = @"INSERT INTO TutorialCategories 
                     (Name, ImageUrl, Slug, PublishedOn, Description, IsActive)
                     VALUES (@Name, @ImageUrl, @Slug, @PublishedOn, @Description, @IsActive)";
            await conn.ExecuteAsync(query, model);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            using var conn = new SqlConnection(_conn);
            var category = await conn.QueryFirstOrDefaultAsync<TutorialCategory>(
                "SELECT * FROM TutorialCategories WHERE Id = @Id", new { Id = id });

            if (category == null)
                return NotFound();

            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TutorialCategory model)
        {
            if (!ModelState.IsValid)
                return View(model);

            using var conn = new SqlConnection(_conn);
            var query = @"UPDATE TutorialCategories 
                  SET Name = @Name,
                      ImageUrl = @ImageUrl,
                      Slug = @Slug,
                      PublishedOn = @PublishedOn,
                      Description = @Description,
                      IsActive = @IsActive
                  WHERE Id = @Id";
            await conn.ExecuteAsync(query, model);
            return RedirectToAction("Index");
        }
    }
}
