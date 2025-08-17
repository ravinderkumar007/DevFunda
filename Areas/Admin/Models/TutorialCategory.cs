namespace Devfunda.Areas.Admin.Models
{
    public class TutorialCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Slug { get; set; }
        public DateTime PublishedOn { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        public List<Tutorial>? Tutorials { get; set; }
    }

}
