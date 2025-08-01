namespace Devfunda.Areas.Admin.Models
{
    public class Tutorial
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public string ImageUrl { get; set; }
        public string Slug { get; set; }
        public DateTime PublishedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool IsActive { get; set; }
    }

}
