using System;
using System.Collections.Generic;

namespace Devfunda.Areas.Admin.Models;
public class Post
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string Content { get; set; } = null!;

    public string? Summary { get; set; }

    public int? CategoryId { get; set; }

    public int? AuthorId { get; set; }  // FK to Author

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? PublishedAt { get; set; }

    public bool IsPublished { get; set; }

    public  Author? Author { get; set; }  // ✅ only this

    public virtual Category? Category { get; set; }

    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
}
