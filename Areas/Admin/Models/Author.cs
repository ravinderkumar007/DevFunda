using System.ComponentModel.DataAnnotations;

namespace Devfunda.Areas.Admin.Models
{
    public class Author
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string? Bio { get; set; }

        public string? ProfileImageUrl { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
    }

}
