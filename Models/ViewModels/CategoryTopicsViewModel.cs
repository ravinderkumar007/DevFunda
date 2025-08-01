namespace Devfunda.Models.ViewModels
{
    public class CategoryTopicsViewModel
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public List<TopicSummaryViewModel> Topics { get; set; }
    }
}
