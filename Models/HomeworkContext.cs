using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Homework.Models;

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

    public virtual DbSet<AuxiliartableLoan> AuxiliartableLoans { get; set; }

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

        modelBuilder.Entity<AuxiliartableLoan>(entity =>
        {
            entity.HasKey(e => e.IdAuxiliar).HasName("AuxiliarTable_loans_pkey");

            entity.ToTable("auxiliartable_loans");

            entity.Property(e => e.IdAuxiliar)
                .HasDefaultValueSql("nextval('\"AuxiliarTable_loans_Id_auxiliar_seq\"'::regclass)")
                .HasColumnName("Id_auxiliar");
            entity.Property(e => e.IdBook).HasColumnName("id_book");
            entity.Property(e => e.IdLoan).HasColumnName("id_loan");

            entity.HasOne(d => d.IdBookNavigation).WithMany(p => p.AuxiliarTable)
                .HasForeignKey(d => d.IdBook)
                .HasConstraintName("FK_auxiliar_book");

            entity.HasOne(d => d.IdLoanNavigation).WithMany(p => p.AuxiliarTable)
                .HasForeignKey(d => d.IdLoan)
                .HasConstraintName("FK_auxiliar_loan");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
