
using ASP_MVC_App.Entities;
using Microsoft.EntityFrameworkCore;

namespace ASP_MVC_App.Data;

public partial class BPContext : DbContext
{
    public BPContext()
    {
    }

    public BPContext(DbContextOptions<BPContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Subject> Subjects { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Subjects", "BP-scheme");

            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}