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
        //public async Task<IActionResult> Category(string categorySlug)
        //{
        //    using var conn = new SqlConnection(_conn);

        //    var category = await conn.QueryFirstOrDefaultAsync<TutorialCategory>(
        //        "SELECT * FROM TutorialCategories WHERE Slug = @Slug AND IsActive = 1", new { Slug = categorySlug });

        //    if (category == null) return NotFound();

        //    var topics = await conn.QueryAsync<TutorialTopic>(
        //        @"SELECT t.* FROM TutorialTopics t
        //      INNER JOIN Tutorials tu ON tu.Id = t.TutorialId
        //      WHERE tu.CategoryId = @CategoryId AND t.IsActive = 1 AND tu.IsActive = 1",
        //        new { CategoryId = category.Id });

        //    ViewBag.Category = category;
        //    return View(topics);
        //}


        public async Task<IActionResult> Category(string categorySlug)
        {
            var tutorialDict = new Dictionary<int, Tutorial>();
            string categoryName = null;
            string categoryImage = null; // <-- new variable for image

            using (var connection = new SqlConnection(_conn))
            {
                string sql = @"
    SELECT 
        t.Id, t.CategoryId, t.Title, t.Details, t.ImageUrl, t.Slug, 
        t.PublishedOn, t.UpdatedOn, t.IsActive,
        tt.Id, tt.TutorialId, tt.TopicName, tt.TopicContent, tt.Slug, 
        tt.PublishedOn, tt.UpdatedOn, tt.IsActive,
        c.Name, c.ImageUrl AS CategoryImage
    FROM Tutorials t
    INNER JOIN TutorialCategories c ON t.CategoryId = c.Id
    LEFT JOIN TutorialTopics tt ON t.Id = tt.TutorialId
    WHERE c.Slug = @CategorySlug
    ORDER BY t.Id, tt.Id;";

                var tutorials = await connection.QueryAsync<Tutorial, TutorialTopic, string, string, Tutorial>(
                    sql,
                    (tutorial, topic, catName, catImage) =>
                    {
                        if (categoryName == null) // store category name once
                            categoryName = catName;

                        if (categoryImage == null) // store category image once
                            categoryImage = catImage;

                        if (!tutorialDict.TryGetValue(tutorial.Id, out var tut))
                        {
                            tut = tutorial;
                            tut.Topics = new List<TutorialTopic>();
                            tutorialDict.Add(tut.Id, tut);
                        }

                        if (topic != null && topic.Id != 0)
                        {
                            tut.Topics.Add(topic);
                        }

                        return tut;
                    },
                    new { CategorySlug = categorySlug },
                    splitOn: "Id,Name,CategoryImage"
                );
            }

            ViewBag.Category = categoryName;
            ViewBag.CategoryImage = categoryImage; // <-- set image to ViewBag
            return View(tutorialDict.Values.ToList());
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

