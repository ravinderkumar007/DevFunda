using System;
using System.Collections.Generic;

namespace Devfunda.Areas.Admin.Models;

public partial class SeoMetadatum
{
    public int Id { get; set; }

    public string? EntityType { get; set; }

    public int? EntityId { get; set; }

    public string? MetaTitle { get; set; }

    public string? MetaDescription { get; set; }

    public string? MetaKeywords { get; set; }
}
