using Microsoft.EntityFrameworkCore;
using HRManager.APIv2.Models;

namespace HRManager.APIv2.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<UserProfile> UserProfiles { get; set; }

        public DbSet<Candidate> Candidates { get; set; }

        public DbSet<Interview> Interviews { get; set; }

        public DbSet<Position> Positions { get; set; }

        public DbSet<WorkingPlace> WorkingPlaces { get; set; }

        public DbSet<EducationDegree> EducationDegrees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Candidate>()
                .HasMany(c => c.Interviews)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserProfile>().HasData(new UserProfile
            {
                Id = "151dba72-2400-43d6-9e33-cadbb71b865b",
                UserName = "marko@gmail.com",
                Email = "marko@gmail.com",
                FirstName = "Marko",
                LastName = "Curlin",
                Role = "Admin",
            });

            modelBuilder.Entity<UserProfile>().HasData(new UserProfile
            {
                Id = "510df9ac-baca-43b6-9e4a-cdda5f419428",
                UserName = "ana@gmail.com",
                Email = "ana@gmail.com",
                FirstName = "Ana",
                LastName = "Anic",
                Role = "Edit",
            });

            modelBuilder.Entity<UserProfile>().HasData(new UserProfile
            {
                Id = "91dd93b1-403c-4913-b7fe-917bb0c35996",
                UserName = "filip@gmail.com",
                Email = "filip@gmail.com",
                FirstName = "Filip",
                LastName = "Filic",
                Role = "View",
            });

            modelBuilder.Entity<Position>().HasData(new Position
            {
                Id = "151dba72-2400-43d6-9e33-cadbb71b865b",
                Name = "Backend",
            });

            modelBuilder.Entity<Position>().HasData(new Position
            {
                Id = "510df9ac-baca-43b6-9e4a-cdda5f419428",
                Name = "Frontend",
            });

            modelBuilder.Entity<WorkingPlace>().HasData(new WorkingPlace
            {
                Id = "151dba72-2400-43d6-9e33-cadbb71b865b",
                Name = "Backend",
            });

            modelBuilder.Entity<WorkingPlace>().HasData(new WorkingPlace
            {
                Id = "510df9ac-baca-43b6-9e4a-cdda5f419428",
                Name = "Frontend",
            });

            modelBuilder.Entity<EducationDegree>().HasData(new EducationDegree
            {
                Id = "91dd93b1-403c-4913-b7fe-917bb0c35996",
                Name = "univ. bacc. ing.",
            });

            modelBuilder.Entity<EducationDegree>().HasData(new EducationDegree
            {
                Id = "151dba72-2400-43d6-9e33-cadbb71b865b",
                Name = "mag. ing.",
            });

            modelBuilder.Entity<EducationDegree>().HasData(new EducationDegree
            {
                Id = "510df9ac-baca-43b6-9e4a-cdda5f419428",
                Name = "dr. sc.",
            });
        }
    }
}
