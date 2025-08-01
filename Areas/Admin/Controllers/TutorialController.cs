using Dapper;
using Devfunda.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;

namespace Devfunda.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TutorialController : Controller
    {
        private readonly IConfiguration _config;
        private readonly string _conn;

        public TutorialController(IConfiguration config)
        {
            _config = config;
            _conn = _config.GetConnectionString("DefaultConnection");
        }

        public async Task<IActionResult> Index()
        {
            using var conn = new SqlConnection(_conn);
            var tutorials = await conn.QueryAsync<Tutorial>("SELECT * FROM Tutorials");
            return View(tutorials);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            using var conn = new SqlConnection(_conn);
            var categories = await conn.QueryAsync<TutorialCategory>("SELECT Id, Name FROM TutorialCategories WHERE IsActive = 1");

            ViewBag.Categories = categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Tutorial model)
        {
            using var conn = new SqlConnection(_conn);
            var query = @"INSERT INTO Tutorials 
                        (CategoryId, Title, Details, ImageUrl, Slug, PublishedOn, UpdatedOn, IsActive)
                        VALUES 
                        (@CategoryId, @Title, @Details, @ImageUrl, @Slug, @PublishedOn, @UpdatedOn, @IsActive)";
            await conn.ExecuteAsync(query, model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            using var conn = new SqlConnection(_conn);

            var tutorial = await conn.QueryFirstOrDefaultAsync<Tutorial>("SELECT * FROM Tutorials WHERE Id = @Id", new { Id = id });
            if (tutorial == null) return NotFound();

            var categories = await conn.QueryAsync<TutorialCategory>("SELECT Id, Name FROM TutorialCategories WHERE IsActive = 1");
            ViewBag.Categories = categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();

            return View(tutorial);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Tutorial model)
        {
            using var conn = new SqlConnection(_conn);
            var query = @"UPDATE Tutorials SET 
                        CategoryId = @CategoryId,
                        Title = @Title,
                        Details = @Details,
                        ImageUrl = @ImageUrl,
                        Slug = @Slug,
                        PublishedOn = @PublishedOn,
                        UpdatedOn = @UpdatedOn,
                        IsActive = @IsActive
                        WHERE Id = @Id";
            await conn.ExecuteAsync(query, model);
            return RedirectToAction("Index");
        }
    }
}
