using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Devfunda.Areas.Admin.Models;

public partial class DevfundaDbContext : DbContext
{
    public DevfundaDbContext()
    {
    }

    public DevfundaDbContext(DbContextOptions<DevfundaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Medium> Media { get; set; }

    public virtual DbSet<Page> Pages { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<SeoMetadatum> SeoMetadata { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<User> Users { get; set; }
    public DbSet<Author> Authors { get; set; }
   
}
