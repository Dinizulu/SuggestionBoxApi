using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SuggestionBoxApi.Models;

namespace SuggestionBoxApi.Data;

public partial class SuggboxContext : DbContext
{
    public SuggboxContext(DbContextOptions<SuggboxContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Suggestion> Suggestions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Categori__19093A2B536F8F83");

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CategoryName).HasMaxLength(100);
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackId).HasName("PK__Feedback__6A4BEDF65AF723C4");

            entity.ToTable("Feedback");

            entity.HasIndex(e => e.SuggestionId, "IX_Feedback_SuggestionID");

            entity.Property(e => e.FeedbackId).HasColumnName("FeedbackID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.SuggestionId).HasColumnName("SuggestionID");

            entity.HasOne(d => d.Suggestion).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.SuggestionId)
                .HasConstraintName("FK__Feedback__Sugges__44FF419A");
        });

        modelBuilder.Entity<Suggestion>(entity =>
        {
            entity.HasKey(e => e.SuggestionId).HasName("PK__Suggesti__9409952888F40B11");

            entity.HasIndex(e => e.CategoryId, "IX_Suggestions_CategoryID");

            entity.HasIndex(e => e.UserId, "IX_Suggestions_UserID");

            entity.Property(e => e.SuggestionId).HasColumnName("SuggestionID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.SubmittedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Category).WithMany(p => p.Suggestions)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Suggestio__Categ__412EB0B6");

            entity.HasOne(d => d.User).WithMany(p => p.Suggestions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Suggestio__UserI__403A8C7D");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC4D3CA7CE");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534A253293B")
                .IsUnique()
                .HasFilter("([Email] IS NOT NULL)");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.UserPassword).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
