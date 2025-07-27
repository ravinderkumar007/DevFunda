using System;
using System.Collections.Generic;

namespace Devfunda.Areas.Admin.Models;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = string.Empty;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = string.Empty;

    public string? DisplayName { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<Medium> Media { get; set; } = new List<Medium>();



    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
