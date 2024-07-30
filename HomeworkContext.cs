using System;
using System.Collections.Generic;
using Homework.Models;
using Microsoft.EntityFrameworkCore;

namespace Homework;

public partial class HomeworkContext : DbContext
{
    public HomeworkContext()
    {
    }

    public HomeworkContext(DbContextOptions<HomeworkContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Loan> Loans { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.IdAuthor).HasName("authors_pkey");

            entity.ToTable("authors");

            entity.Property(e => e.IdAuthor).HasColumnName("id_author");
            entity.Property(e => e.Email)
                .HasColumnType("character varying")
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.IdBook).HasName("books_pkey");

            entity.ToTable("books");

            entity.Property(e => e.IdBook).HasColumnName("id_book");
            entity.Property(e => e.Available)
                .HasColumnType("character varying")
                .HasColumnName("available");
            entity.Property(e => e.Gender)
                .HasColumnType("character varying")
                .HasColumnName("gender");
            entity.Property(e => e.IdAutor).HasColumnName("id_autor");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.NumPags).HasColumnName("num_pags");

            entity.HasOne(d => d.IdAutorNavigation).WithMany(p => p.Books)
                .HasForeignKey(d => d.IdAutor)
                .HasConstraintName("author_of_book");
        });

        modelBuilder.Entity<Loan>(entity =>
        {
            entity.HasKey(e => e.IdLoan).HasName("loans_pkey");

            entity.ToTable("loans");

            entity.Property(e => e.IdLoan).HasColumnName("Id_loan");
            entity.Property(e => e.DateLoan).HasColumnName("date_loan");
            entity.Property(e => e.DateLoanCompletion).HasColumnName("date_loan_completion");
            entity.Property(e => e.IdBook).HasColumnName("id_book");
            entity.Property(e => e.IdUser).HasColumnName("id_user");

            entity.HasOne(d => d.IdBookNavigation).WithMany(p => p.Loans)
                .HasForeignKey(d => d.IdBook)
                .HasConstraintName("book_loan");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Loans)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("user_loan");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("users_pkey");

            entity.ToTable("users");

            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.Email)
                .HasColumnType("character varying")
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
