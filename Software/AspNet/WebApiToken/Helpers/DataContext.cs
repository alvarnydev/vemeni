using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WebApiToken.Entities;

namespace WebApiToken.Helpers
{
    public partial class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Chat> Chats { get; set; }
        public virtual DbSet<Job> Jobs { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Chat>(entity =>
            {
                entity.ToTable("chats");

                entity.HasIndex(e => e.User1)
                    .HasName("user1");

                entity.HasIndex(e => e.User2)
                    .HasName("user2");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.User1)
                    .HasColumnName("user1")
                    .HasColumnType("int(11)");

                entity.Property(e => e.User2)
                    .HasColumnName("user2")
                    .HasColumnType("int(11)");

            });

            modelBuilder.Entity<Job>(entity =>
            {
                entity.ToTable("jobs");

                //entity.HasOne(e => e.User);

                entity.HasIndex(e => e.AcceptedBy)
                    .HasName("accepted_by");

                entity.HasIndex(e => e.User)
                    .HasName("user");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AcceptedBy)
                    .HasColumnName("accepted_by")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.Category)
                    .HasColumnName("category")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.LocationLat).HasColumnName("location_lat");

                entity.Property(e => e.LocationLon).HasColumnName("location_lon");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasColumnType("int(11)");

                entity.Property(e => e.User)
                    .HasColumnName("user")
                    .HasColumnType("int(11)");

            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("messages");

                entity.HasIndex(e => e.Author)
                    .HasName("author");

                entity.HasIndex(e => e.ChatId)
                    .HasName("chat_id");

                entity.HasIndex(e => e.Receiver)
                    .HasName("receiver");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Author)
                    .HasColumnName("author")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ChatId)
                    .HasColumnName("chat_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnName("content");

                entity.Property(e => e.Receiver)
                    .HasColumnName("receiver")
                    .HasColumnType("int(11)");

            });

            modelBuilder.Entity<Rating>(entity =>
            {
                entity.ToTable("ratings");

                entity.HasIndex(e => e.GivenBy)
                    .HasName("given_by");

                entity.HasIndex(e => e.JobId)
                    .HasName("job_id");

                entity.HasIndex(e => e.User)
                    .HasName("FK_user");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.GivenBy)
                    .HasColumnName("given_by")
                    .HasColumnType("int(11)");

                entity.Property(e => e.JobId)
                    .HasColumnName("job_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Score)
                    .HasColumnName("score")
                    .HasColumnType("int(11)");

                entity.Property(e => e.User)
                    .HasColumnName("user")
                    .HasColumnType("int(11)");

            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AddressCity)
                    .HasColumnName("address_city")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.AddressPlz)
                    .HasColumnName("address_plz")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.AddressStreet)
                    .HasColumnName("address_street")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AddressStrnmbr)
                    .HasColumnName("address_strnmbr")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Img)
                    .HasColumnName("img")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Lastvisit)
                    .HasColumnName("lastvisit")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasColumnType("varbinary(64)");

                entity.Property(e => e.PasswordSalt)
                    .IsRequired()
                    .HasColumnName("passwordSalt")
                    .HasColumnType("varbinary(128)");

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
