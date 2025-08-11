using Dapper;
using Devfunda.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;

namespace Devfunda.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TutorialTopicController : Controller
    {
        private readonly IConfiguration _config;
        private readonly string _conn;

        public TutorialTopicController(IConfiguration config)
        {
            _config = config;
            _conn = _config.GetConnectionString("DefaultConnection");
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            using var conn = new SqlConnection(_conn);
            var topics = await conn.QueryAsync<TutorialTopic>("SELECT * FROM TutorialTopics");
            return View(topics);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            using var conn = new SqlConnection(_conn);
            var tutorials = await conn.QueryAsync<Tutorial>("SELECT Id, Title FROM Tutorials WHERE IsActive = 1");

            ViewBag.Tutorials = tutorials.Select(t => new SelectListItem
            {
                Value = t.Id.ToString(),
                Text = t.Title
            }).ToList();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TutorialTopic model)
        {
            using var conn = new SqlConnection(_conn);
            var query = @"INSERT INTO TutorialTopics 
                        (TutorialId, TopicName, TopicContent, Slug, PublishedOn, UpdatedOn, IsActive)
                        VALUES 
                        (@TutorialId, @TopicName, @TopicContent, @Slug, @PublishedOn, @UpdatedOn, @IsActive)";
            await conn.ExecuteAsync(query, model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            using var conn = new SqlConnection(_conn);
            var topic = await conn.QueryFirstOrDefaultAsync<TutorialTopic>("SELECT * FROM TutorialTopics WHERE Id = @Id", new { Id = id });
            if (topic == null) return NotFound();

            var tutorials = await conn.QueryAsync<Tutorial>("SELECT Id, Title FROM Tutorials WHERE IsActive = 1");

            ViewBag.Tutorials = tutorials.Select(t => new SelectListItem
            {
                Value = t.Id.ToString(),
                Text = t.Title
            }).ToList();

            return View(topic);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TutorialTopic model)
        {
            using var conn = new SqlConnection(_conn);
            var query = @"UPDATE TutorialTopics SET 
                        TutorialId = @TutorialId,
                        TopicName = @TopicName,
                        TopicContent = @TopicContent,
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
