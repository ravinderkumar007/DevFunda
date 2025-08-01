namespace Devfunda.Areas.Admin.Models
{
    public class TutorialTopic
    {
        public int Id { get; set; }
        public int TutorialId { get; set; }
        public string TopicName { get; set; }
        public string TopicContent { get; set; }
        public string Slug { get; set; }
        public DateTime PublishedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool IsActive { get; set; }
    }

}
