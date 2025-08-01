namespace Devfunda.Models.ViewModels
{
    public class TopicDetailViewModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string CategorySlug { get; set; }
        public DateTime? PublishedAt { get; set; }
        public int ReadTimeMinutes { get; set; }
    }
}
