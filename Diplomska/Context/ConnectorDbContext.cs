using System;
using Diplomska.Entities;
using Microsoft.EntityFrameworkCore;

namespace Diplomska.Context
{
    public class ConnectorDbContext : DbContext
    {
        public ConnectorDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<User> Users{ get; set; }
        public DbSet<Education> Educations{ get; set; }
        public DbSet<Course> Courses{ get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<Post> Posts{ get; set; }
        //public DbSet<Comment> Comments{ get; set; }
        public DbSet<UserCourse> UserCourses{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User()
                {
                    Id = Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f9b35"),
                    Email = "ilijastaninov@gmail.com",
                    Bio = "Student currently working on my last exam",
                    FirstName = "Ilija",
                    LastName = "Staninov",
                    Username = "ilestaninov",
                    Password = "timduncan22",
                    Status = "Web developer"
                },
                new User()
                {
                    Id = Guid.Parse("da2fd609-d754-4feb-8acd-c4f9ff13ba96"),
                    Email = "nikolina@gmail.com",
                    Bio = "Employee currently working on my masters exam",
                    FirstName = "Nikolina",
                    LastName = "Staninova",
                    Username = "nikolinastaninova",
                    Password = "123456",
                    Status = "Economist"
                },
                new User()
                {
                    Id = Guid.Parse("2902b665-1190-4c70-9915-b9c2d7680450"),
                    Email = "dedeile@gmail.com",
                    Bio = "Old man who likes to sleep",
                    FirstName = "Dede",
                    LastName = "Staninov",
                    Username = "dedeile",
                    Password = "123456",
                    Status = "Old man"
                }
            );
            modelBuilder.Entity<Education>().HasData(
                new Education()
                {
                    EducationId = Guid.Parse("5b1c2b4d-48c7-402a-80c3-cc796ad49c6b"),
                    UserId = Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f9b35"),
                    Degree = "Bachelor",
                    From = new DateTime(2014,09,15),
                    To = new DateTime(2020,09,15)
                },
                new Education()
                {
                    EducationId = Guid.Parse("d8663e5e-7494-4f81-8739-6e0de1bea7ee"),
                    UserId = Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f9b35"),
                    Degree = "High school",
                    From = new DateTime(2010, 09, 01),
                    To = new DateTime(2014, 06, 10)
                },
                new Education()
                {
                    EducationId = Guid.Parse("d173e20d-159e-4127-9ce9-b0ac2564ad97"),
                    UserId = Guid.Parse("da2fd609-d754-4feb-8acd-c4f9ff13ba96"),
                    Degree = "Economics Bachelor",
                    From = new DateTime(2006, 09, 01),
                    To = new DateTime(2011, 06, 10)
                },
                new Education()
                {
                    EducationId = Guid.Parse("40ff5488-fdab-45b5-bc3a-14302d59869a"),
                    UserId = Guid.Parse("2902b665-1190-4c70-9915-b9c2d7680450"),
                    Degree = "Economics Bachelor",
                    From = new DateTime(1950, 09, 01),
                    To = new DateTime(1956, 06, 10)
                }

            );
            modelBuilder.Entity<Course>().HasData(
                new Course()
                {
                    CourseId = Guid.Parse("c8c8082b-a769-44ba-84a4-15a5853f8c3a"),
                    CourseName = "Web programiranje"
                },
                new Course()
                {
                    CourseId = Guid.Parse("62659233-1f07-46a8-8ed7-79c37bea1b64"),
                    CourseName = "Web Aplikacii"
                },
                new Course()
                {
                    CourseId = Guid.Parse("ac9c56b9-48e8-4a58-9cdc-a367f90de5b7"),
                    CourseName = "Operativni sistemi"
                },
                new Course()
                {
                    CourseId = Guid.Parse("b32b3430-1a1b-4b04-976e-04be069242c9"),
                    CourseName = "Marketing"
                }
            );
            modelBuilder.Entity<Experience>().HasData(
                new Experience()
                {
                    ExperienceId = Guid.Parse("0e447fdb-6224-4e61-ad28-2395cd9a118f"),
                    Company = "FINKI",
                    Title = "Web developer",
                    Current = false,
                    From = new DateTime(2015,12,20),
                    To = new DateTime(2016,12,20),
                    UserId = Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f9b35")
                },
                new Experience()
                {
                    ExperienceId = Guid.Parse("6efd04b2-5803-4a46-b065-972d99bcc5f1"),
                    Company = "Stadia Connect",
                    Title = "Web developer",
                    Current = false,
                    From = new DateTime(2016, 12, 30),
                    To = new DateTime(2017, 12, 20),
                    UserId = Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f9b35")
                }
            );
            modelBuilder.Entity<UserCourse>().HasKey(uc => new {uc.UserId,uc.CourseId});
            /*modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c=>c.UserId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Post)
                .WithMany()
                .HasForeignKey(c=>c.PostId).OnDelete(DeleteBehavior.Restrict);*/
            base.OnModelCreating(modelBuilder);
        }
    }
}