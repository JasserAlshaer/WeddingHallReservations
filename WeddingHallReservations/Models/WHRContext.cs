using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WeddingHallReservations.Models
{
    public partial class WHRContext : DbContext
    {
        public WHRContext()
        {
        }

        public WHRContext(DbContextOptions<WHRContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Catregory> Catregory { get; set; }
        public virtual DbSet<Media> Media { get; set; }
        public virtual DbSet<Payment> Payment { get; set; }
        public virtual DbSet<Prodouct> Prodouct { get; set; }
        public virtual DbSet<Reservaition> Reservaition { get; set; }
        public virtual DbSet<ReservaitionProducts> ReservaitionProducts { get; set; }
        public virtual DbSet<Service> Service { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-7945E40\\SQLEXPRESS;Database=WHR;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Catregory>(entity =>
            {
                entity.HasKey(e => e.CategoeyId);

                entity.Property(e => e.CategoeyId).HasColumnName("CategoeyID");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Media>(entity =>
            {
                entity.Property(e => e.MediaId).HasColumnName("MediaID");

                entity.Property(e => e.ImagePath).IsUnicode(false);

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Media)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_Media_Prodouct");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.Property(e => e.Balance).HasColumnName("balance");

                entity.Property(e => e.Cvv2)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.ExpireDate).HasColumnType("datetime");

                entity.Property(e => e.Visa)
                    .HasMaxLength(16)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Prodouct>(entity =>
            {
                entity.HasKey(e => e.ProductId);

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ServiceId).HasColumnName("ServiceID");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.Prodouct)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("FK_Prodouct_Service");
            });

            modelBuilder.Entity<Reservaition>(entity =>
            {
                entity.HasKey(e => e.ResrvitionId);

                entity.Property(e => e.ResrvitionId).ValueGeneratedNever();

                entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.Notes)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ReservaitionDateFrom).HasColumnType("datetime");

                entity.Property(e => e.ReservaitionDateTo).HasColumnType("datetime");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.Reservaition)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("FK_Reservaition_Service");
            });

            modelBuilder.Entity<ReservaitionProducts>(entity =>
            {
                entity.Property(e => e.ReservaitionProductsId).HasColumnName("ReservaitionProductsID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.Property(e => e.ServiceId).HasColumnName("ServiceID");

                entity.Property(e => e.Address)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ProfileImage)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ServiceProvided)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
