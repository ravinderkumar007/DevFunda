namespace Devfunda.Models.ViewModels
{
    public class TutorialTopicViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Content { get; set; }
        public DateTime? PublishedAt { get; set; }
        public string CategorySlug { get; set; }
        public string TutorialTitle { get; set; }
    }
}
