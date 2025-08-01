using Dapper;
using Devfunda.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Devfunda.Controllers
{
    public class TutorialsController : Controller
    {
        private readonly IConfiguration _config;
        private readonly string _conn;

        public TutorialsController(IConfiguration config)
        {
            _config = config;
            _conn = _config.GetConnectionString("DefaultConnection");
        }

        // List of all categories
        public async Task<IActionResult> Index()
        {
            using var conn = new SqlConnection(_conn);
            var categories = await conn.QueryAsync<TutorialCategory>("SELECT * FROM TutorialCategories WHERE IsActive = 1");
            return View(categories);
        }

        // List of all topics under a category (from all its tutorials)
        public async Task<IActionResult> Category(string categorySlug)
        {
            using var conn = new SqlConnection(_conn);

            var category = await conn.QueryFirstOrDefaultAsync<TutorialCategory>(
                "SELECT * FROM TutorialCategories WHERE Slug = @Slug AND IsActive = 1", new { Slug = categorySlug });

            if (category == null) return NotFound();

            var topics = await conn.QueryAsync<TutorialTopic>(
                @"SELECT t.* FROM TutorialTopics t
              INNER JOIN Tutorials tu ON tu.Id = t.TutorialId
              WHERE tu.CategoryId = @CategoryId AND t.IsActive = 1 AND tu.IsActive = 1",
                new { CategoryId = category.Id });

            ViewBag.Category = category;
            return View(topics);
        }

        // Topic Details Page
        public async Task<IActionResult> Topic(string categorySlug, string topicSlug)
        {
            using var conn = new SqlConnection(_conn);

            var topic = await conn.QueryFirstOrDefaultAsync<TutorialTopic>(
                "SELECT * FROM TutorialTopics WHERE Slug = @Slug AND IsActive = 1", new { Slug = topicSlug });

            if (topic == null) return NotFound();

            return View(topic);
        }
    }

}

