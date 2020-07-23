﻿// <auto-generated />
using System;
using Diplomska.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Diplomska.Migrations
{
    [DbContext(typeof(ConnectorDbContext))]
    partial class ConnectorDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Diplomska.Entities.Course", b =>
                {
                    b.Property<Guid>("CourseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CourseName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("CourseId");

                    b.ToTable("Courses");

                    b.HasData(
                        new
                        {
                            CourseId = new Guid("c8c8082b-a769-44ba-84a4-15a5853f8c3a"),
                            CourseName = "Web programiranje"
                        },
                        new
                        {
                            CourseId = new Guid("62659233-1f07-46a8-8ed7-79c37bea1b64"),
                            CourseName = "Web Aplikacii"
                        },
                        new
                        {
                            CourseId = new Guid("ac9c56b9-48e8-4a58-9cdc-a367f90de5b7"),
                            CourseName = "Operativni sistemi"
                        },
                        new
                        {
                            CourseId = new Guid("b32b3430-1a1b-4b04-976e-04be069242c9"),
                            CourseName = "Marketing"
                        });
                });

            modelBuilder.Entity("Diplomska.Entities.Education", b =>
                {
                    b.Property<Guid>("EducationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Degree")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<DateTime>("From")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("To")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("EducationId");

                    b.HasIndex("UserId");

                    b.ToTable("Educations");

                    b.HasData(
                        new
                        {
                            EducationId = new Guid("5b1c2b4d-48c7-402a-80c3-cc796ad49c6b"),
                            Degree = "Bachelor",
                            From = new DateTime(2014, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            To = new DateTime(2020, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35")
                        },
                        new
                        {
                            EducationId = new Guid("d8663e5e-7494-4f81-8739-6e0de1bea7ee"),
                            Degree = "High school",
                            From = new DateTime(2010, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            To = new DateTime(2014, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35")
                        },
                        new
                        {
                            EducationId = new Guid("d173e20d-159e-4127-9ce9-b0ac2564ad97"),
                            Degree = "Economics Bachelor",
                            From = new DateTime(2006, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            To = new DateTime(2011, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = new Guid("da2fd609-d754-4feb-8acd-c4f9ff13ba96")
                        },
                        new
                        {
                            EducationId = new Guid("40ff5488-fdab-45b5-bc3a-14302d59869a"),
                            Degree = "Economics Bachelor",
                            From = new DateTime(1950, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            To = new DateTime(1956, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = new Guid("2902b665-1190-4c70-9915-b9c2d7680450")
                        });
                });

            modelBuilder.Entity("Diplomska.Entities.Experience", b =>
                {
                    b.Property<Guid>("ExperienceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Company")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<bool>("Current")
                        .HasColumnType("bit");

                    b.Property<DateTime>("From")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<DateTime>("To")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ExperienceId");

                    b.HasIndex("UserId");

                    b.ToTable("Experiences");

                    b.HasData(
                        new
                        {
                            ExperienceId = new Guid("0e447fdb-6224-4e61-ad28-2395cd9a118f"),
                            Company = "FINKI",
                            Current = false,
                            From = new DateTime(2015, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Web developer",
                            To = new DateTime(2016, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35")
                        },
                        new
                        {
                            ExperienceId = new Guid("6efd04b2-5803-4a46-b065-972d99bcc5f1"),
                            Company = "Stadia Connect",
                            Current = false,
                            From = new DateTime(2016, 12, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Web developer",
                            To = new DateTime(2017, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35")
                        });
                });

            modelBuilder.Entity("Diplomska.Entities.Post", b =>
                {
                    b.Property<Guid>("PostId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("Diplomska.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Bio")
                        .HasColumnType("nvarchar(1500)")
                        .HasMaxLength(1500);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35"),
                            Bio = "Student currently working on my last exam",
                            Email = "ilijastaninov@gmail.com",
                            FirstName = "Ilija",
                            LastName = "Staninov",
                            Password = "timduncan22",
                            Status = "Web developer",
                            Username = "ilestaninov"
                        },
                        new
                        {
                            Id = new Guid("da2fd609-d754-4feb-8acd-c4f9ff13ba96"),
                            Bio = "Employee currently working on my masters exam",
                            Email = "nikolina@gmail.com",
                            FirstName = "Nikolina",
                            LastName = "Staninova",
                            Password = "123456",
                            Status = "Economist",
                            Username = "nikolinastaninova"
                        },
                        new
                        {
                            Id = new Guid("2902b665-1190-4c70-9915-b9c2d7680450"),
                            Bio = "Old man who likes to sleep",
                            Email = "dedeile@gmail.com",
                            FirstName = "Dede",
                            LastName = "Staninov",
                            Password = "123456",
                            Status = "Old man",
                            Username = "dedeile"
                        });
                });

            modelBuilder.Entity("Diplomska.Entities.UserCourse", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CourseId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "CourseId");

                    b.HasIndex("CourseId");

                    b.ToTable("UserCourses");
                });

            modelBuilder.Entity("Diplomska.Entities.Education", b =>
                {
                    b.HasOne("Diplomska.Entities.User", "User")
                        .WithMany("Educations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Diplomska.Entities.Experience", b =>
                {
                    b.HasOne("Diplomska.Entities.User", "User")
                        .WithMany("Experiences")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Diplomska.Entities.Post", b =>
                {
                    b.HasOne("Diplomska.Entities.User", "User")
                        .WithMany("Posts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Diplomska.Entities.UserCourse", b =>
                {
                    b.HasOne("Diplomska.Entities.Course", "Course")
                        .WithMany("UserCourses")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Diplomska.Entities.User", "User")
                        .WithMany("UserCourses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
