using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SimpleBlog.Infrastructure.Models;

public partial class SimpleBlogContext : DbContext
{
    public SimpleBlogContext()
    {
    }

    public SimpleBlogContext(DbContextOptions<SimpleBlogContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Post> Posts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      // #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=SimpleBlog;Trusted_Connection=True;TrustServerCertificate=True;Connect Timeout=60");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Post>(entity =>
        {
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Text).IsUnicode(false);
            entity.Property(e => e.IsActive);
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
