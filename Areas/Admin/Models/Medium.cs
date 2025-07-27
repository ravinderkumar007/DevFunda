using System;
using System.Collections.Generic;

namespace Devfunda.Areas.Admin.Models;

public partial class Medium
{
    public int Id { get; set; }

    public string FileName { get; set; } = null!;

    public string FilePath { get; set; } = null!;

    public string? FileType { get; set; }

    public int? UploadedBy { get; set; }

    public DateTime UploadedAt { get; set; }

    public virtual User? UploadedByNavigation { get; set; }
}
