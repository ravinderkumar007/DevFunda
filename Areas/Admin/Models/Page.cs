using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Devfunda.Areas.Admin.Models;

public partial class Page
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Slug { get; set; } = null!;
    [AllowHtml]
    public string? Content { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool IsPublished { get; set; }
}
