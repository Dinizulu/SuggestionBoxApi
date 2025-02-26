﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SuggestionBoxApi.Data;

#nullable disable

namespace SuggestionBoxApi.Migrations
{
    [DbContext(typeof(SuggboxContext))]
    partial class SuggboxContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SuggestionBoxApi.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("CategoryID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("CategoryId")
                        .HasName("PK__Categori__19093A2B536F8F83");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("SuggestionBoxApi.Models.Feedback", b =>
                {
                    b.Property<int>("FeedbackId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("FeedbackID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FeedbackId"));

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("FeedbackText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SuggestionId")
                        .HasColumnType("int")
                        .HasColumnName("SuggestionID");

                    b.HasKey("FeedbackId")
                        .HasName("PK__Feedback__6A4BEDF65AF723C4");

                    b.HasIndex(new[] { "SuggestionId" }, "IX_Feedback_SuggestionID");

                    b.ToTable("Feedback", (string)null);
                });

            modelBuilder.Entity("SuggestionBoxApi.Models.Suggestion", b =>
                {
                    b.Property<int>("SuggestionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("SuggestionID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SuggestionId"));

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int")
                        .HasColumnName("CategoryID");

                    b.Property<bool>("IsAnonymous")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("SubmittedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("SuggestionText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    b.HasKey("SuggestionId")
                        .HasName("PK__Suggesti__9409952888F40B11");

                    b.HasIndex(new[] { "CategoryId" }, "IX_Suggestions_CategoryID");

                    b.HasIndex(new[] { "UserId" }, "IX_Suggestions_UserID");

                    b.ToTable("Suggestions");
                });

            modelBuilder.Entity("SuggestionBoxApi.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Email")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("UserPassword")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("UserId")
                        .HasName("PK__Users__1788CCAC4D3CA7CE");

                    b.HasIndex(new[] { "Email" }, "UQ__Users__A9D10534A253293B")
                        .IsUnique()
                        .HasFilter("([Email] IS NOT NULL)");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SuggestionBoxApi.Models.Feedback", b =>
                {
                    b.HasOne("SuggestionBoxApi.Models.Suggestion", "Suggestion")
                        .WithMany("Feedbacks")
                        .HasForeignKey("SuggestionId")
                        .HasConstraintName("FK__Feedback__Sugges__44FF419A");

                    b.Navigation("Suggestion");
                });

            modelBuilder.Entity("SuggestionBoxApi.Models.Suggestion", b =>
                {
                    b.HasOne("SuggestionBoxApi.Models.Category", "Category")
                        .WithMany("Suggestions")
                        .HasForeignKey("CategoryId")
                        .HasConstraintName("FK__Suggestio__Categ__412EB0B6");

                    b.HasOne("SuggestionBoxApi.Models.User", "User")
                        .WithMany("Suggestions")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__Suggestio__UserI__403A8C7D");

                    b.Navigation("Category");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SuggestionBoxApi.Models.Category", b =>
                {
                    b.Navigation("Suggestions");
                });

            modelBuilder.Entity("SuggestionBoxApi.Models.Suggestion", b =>
                {
                    b.Navigation("Feedbacks");
                });

            modelBuilder.Entity("SuggestionBoxApi.Models.User", b =>
                {
                    b.Navigation("Suggestions");
                });
#pragma warning restore 612, 618
        }
    }
}
