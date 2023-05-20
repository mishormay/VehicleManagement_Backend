using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using VehicleManagement.Entities;
using VehicleManagement.Helpers;

namespace VehicleManagement.Entities
{
    public partial class MyContext : DbContext
    {
        public MyContext()
        {
        }

        public MyContext(DbContextOptions<MyContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AppSettings> AppSettings { get; set; }
        public virtual DbSet<PasswordReset> PasswordReset { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }
        public virtual DbSet<UserInRole> UserInRole { get; set; }

        public virtual DbSet<VehicleManufacturer> VehicleManufacturers { get; set; }
        public virtual DbSet<VehicleModel> VehicleModels { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppSettings>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(getdate())");
            });


            modelBuilder.Entity<PasswordReset>(entity =>
            {
                entity.HasIndex(e => e.Email)
                    .HasName("IX_PasswordReset");
            });


            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email)
                    .HasName("IX_Email")
                    .IsUnique();

                entity.HasIndex(e => e.Username)
                    .HasName("IX_Username")
                    .IsUnique();

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");



            });

            modelBuilder.Entity<UserRole>()
            .Property(f => f.Id)
            .ValueGeneratedOnAdd();


            // Composit Key
            modelBuilder.Entity<UserInRole>()
            .HasKey(bc => new
            {
                bc.UserId,
                bc.RoleId
            });

            modelBuilder.Entity<UserInRole>(entity =>
            {
                entity.HasOne(d => d.user)
                    .WithMany(p => p.userRoles)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_UserInRoles");

                entity.HasOne(d => d.userRole)
                    .WithMany(p => p.userRoles)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRoles_UserInRoles");
            });

            modelBuilder.Entity<VehicleModel>(entity =>
            {
                entity.HasOne(d => d.Manufacturer)
                    .WithMany(p => p._vehicleModels)
                    .HasForeignKey(d => d.ManufacturerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Manufacturer_vehicleModels");

              
            });




            // Ends Here
            OnModelCreatingPartial(modelBuilder);

            #region Initialize Datas
            byte[] passwordHash, passwordSalt;
            AuthenticationHelper.CreatePasswordHash("123456", out passwordHash, out passwordSalt);

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                Email = "admin@abc.com",
                Username = "admin@abc.com",
                FirstName = "Vehicle ",
                LastName = "Master",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                IsAdmin = true,
                IsEmployee = false,
                CreatedDate = DateTime.Now
            });

            modelBuilder.Entity<UserRole>().HasData(
                new UserRole { Id = 1, RoleName = "Admin" }
            );

            modelBuilder.Entity<UserInRole>().HasData(
               new UserInRole { UserId = 1, RoleId = 1 }
           );

            modelBuilder.Entity<VehicleManufacturer>().HasData(
              new VehicleManufacturer { Id =1, Name = "Toyota"  },
              new VehicleManufacturer { Id = 2, Name = "BMW Group" },
              new VehicleManufacturer { Id = 3, Name = "Honda Motor Co." },
              new VehicleManufacturer { Id = 4, Name = "Suzuki Motor Corporation" },
              new VehicleManufacturer { Id = 5, Name = "Nissan Motor Co." }
          );
            modelBuilder.Entity<VehicleModel>().HasData(
                new VehicleModel { Id = 1, ManufacturerId = 1, ModelYear = "2000", Name = "Toyota Corolla" },
                new VehicleModel { Id = 2, ManufacturerId = 2, ModelYear = "2000", Name = "BMW 7 Series" },
                new VehicleModel { Id = 3, ManufacturerId = 3, ModelYear = "2000", Name = "Honda City" },
                new VehicleModel { Id = 4, ManufacturerId = 4, ModelYear = "2000", Name = "Suzuki Vitara" },
                new VehicleModel { Id = 5, ManufacturerId = 5, ModelYear = "2000", Name = "Nissan Altima" },
                new VehicleModel { Id = 6, ManufacturerId = 1, ModelYear = "2000", Name = "Toyota Hilux" }
                );

            #endregion
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);


    }
}
