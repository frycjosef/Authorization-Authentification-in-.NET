using BP.Common.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BP.Common.Data;

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
    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
            
            entity.HasMany(e => e.UserSubjects)
                .WithOne(s => s.Subject);
        });
        
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.FirstName).HasMaxLength(255);
            entity.Property(e => e.LastName).HasMaxLength(255);
            entity.Property(e => e.Username).HasMaxLength(255);

            entity.HasMany(e => e.UserSubjects)
                .WithOne(s => s.User);
        });
        
        modelBuilder.Entity<UserSubject>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.SubjectId });

            entity.HasOne(d => d.User)
                .WithMany(p => p.UserSubjects)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasForeignKey("FK_UserSubjects_Users");

            entity.HasOne(d => d.Subject)
                .WithMany(p => p.UserSubjects)
                .HasForeignKey(d => d.SubjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasForeignKey("FK_UserSubjects_Subjects");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}