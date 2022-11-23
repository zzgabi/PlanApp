using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ManageAccommodation.Models.DBObjects;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ManageAccommodation.Models
{
    public partial class ApplicationDbContext : IdentityDbContext 
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Administrator> Administrators { get; set; } = null!;
        public virtual DbSet<Dorm> Dorms { get; set; } = null!;
        public virtual DbSet<Notice> Notices { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;
        public virtual DbSet<Room> Rooms { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=DefaultConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Administrator>(entity =>
            {
                entity.HasKey(e => e.Idadministrator)
                    .HasName("PK__Administ__E4075E64D3893FBA");

                entity.Property(e => e.Idadministrator)
                    .ValueGeneratedNever()
                    .HasColumnName("IDAdministrator");

                entity.Property(e => e.Iddorm).HasColumnName("IDDorm");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IddormNavigation)
                    .WithMany(p => p.Administrators)
                    .HasForeignKey(d => d.Iddorm)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Administrators_Dorms");
            });

            modelBuilder.Entity<Dorm>(entity =>
            {
                entity.HasKey(e => e.Iddorm)
                    .HasName("PK__Dorms__2D6DE8E9D275C638");

                entity.Property(e => e.Iddorm)
                    .ValueGeneratedNever()
                    .HasColumnName("IDDorm");

                entity.Property(e => e.Adress)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DormName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Dorm_Name");

                entity.Property(e => e.TotalRooms).HasColumnName("Total_Rooms");
            });

            modelBuilder.Entity<Notice>(entity =>
            {
                entity.HasKey(e => e.Idnotification)
                    .HasName("PK__Notices__5456E7BCCC45E5AD");

                entity.Property(e => e.Idnotification)
                    .ValueGeneratedNever()
                    .HasColumnName("IDNotification");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.EmailStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Email_Status");

                entity.Property(e => e.MobileStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Mobile_Status");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(e => e.Idpayment)
                    .HasName("PK__Payments__A89D5100108FE67C");

                entity.Property(e => e.Idpayment)
                    .ValueGeneratedNever()
                    .HasColumnName("IDPayment");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Iddorm).HasColumnName("IDDorm");

                entity.Property(e => e.Idroom).HasColumnName("IDRoom");

                entity.Property(e => e.Idstudent).HasColumnName("IDStudent");

                entity.HasOne(d => d.IddormNavigation)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.Iddorm)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Payments_Dorms");

                entity.HasOne(d => d.IdroomNavigation)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.Idroom)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Payments_Room");

                entity.HasOne(d => d.IdstudentNavigation)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.Idstudent)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Payments_Student");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasKey(e => e.Idroom)
                    .HasName("PK__Room__A1BA0EAFA09393F2");

                entity.ToTable("Room");

                entity.Property(e => e.Idroom)
                    .ValueGeneratedNever()
                    .HasColumnName("IDRoom");

                entity.Property(e => e.Iddorm).HasColumnName("IDDorm");

                entity.Property(e => e.PricePerSt)
                    .HasColumnType("money")
                    .HasColumnName("Price_Per_St");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IddormNavigation)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.Iddorm)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Room_Dorms");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.Idstudent)
                    .HasName("PK__Student__8B47233AAD318525");

                entity.ToTable("Student");

                entity.Property(e => e.Idstudent)
                    .ValueGeneratedNever()
                    .HasColumnName("IDStudent");

                entity.Property(e => e.Debt).HasColumnType("money");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Idroom).HasColumnName("IDRoom");

                entity.Property(e => e.Mobile)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PaymStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Paym_Status");

                entity.Property(e => e.StudentName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Student_Name");

                entity.HasOne(d => d.IdroomNavigation)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.Idroom)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Student_Room");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
