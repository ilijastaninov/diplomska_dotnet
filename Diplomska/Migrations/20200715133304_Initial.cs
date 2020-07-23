using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Diplomska.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    CourseId = table.Column<Guid>(nullable: false),
                    CourseName = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.CourseId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Username = table.Column<string>(maxLength: 30, nullable: false),
                    Password = table.Column<string>(maxLength: 30, nullable: false),
                    FirstName = table.Column<string>(maxLength: 50, nullable: true),
                    LastName = table.Column<string>(maxLength: 50, nullable: true),
                    Email = table.Column<string>(nullable: false),
                    Status = table.Column<string>(nullable: false),
                    Bio = table.Column<string>(maxLength: 1500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    CommentId = table.Column<Guid>(nullable: false),
                    Text = table.Column<string>(maxLength: 500, nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_Comments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Educations",
                columns: table => new
                {
                    EducationId = table.Column<Guid>(nullable: false),
                    Degree = table.Column<string>(maxLength: 200, nullable: false),
                    From = table.Column<DateTime>(nullable: false),
                    To = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Educations", x => x.EducationId);
                    table.ForeignKey(
                        name: "FK_Educations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Experiences",
                columns: table => new
                {
                    ExperienceId = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(maxLength: 100, nullable: false),
                    Company = table.Column<string>(maxLength: 100, nullable: false),
                    From = table.Column<DateTime>(nullable: false),
                    To = table.Column<DateTime>(nullable: false),
                    Current = table.Column<bool>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experiences", x => x.ExperienceId);
                    table.ForeignKey(
                        name: "FK_Experiences_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    PostId = table.Column<Guid>(nullable: false),
                    Text = table.Column<string>(maxLength: 500, nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.PostId);
                    table.ForeignKey(
                        name: "FK_Posts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserCourses",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    CourseId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCourses", x => new { x.UserId, x.CourseId });
                    table.ForeignKey(
                        name: "FK_UserCourses_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCourses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "CourseId", "CourseName" },
                values: new object[,]
                {
                    { new Guid("c8c8082b-a769-44ba-84a4-15a5853f8c3a"), "Web programiranje" },
                    { new Guid("62659233-1f07-46a8-8ed7-79c37bea1b64"), "Web Aplikacii" },
                    { new Guid("ac9c56b9-48e8-4a58-9cdc-a367f90de5b7"), "Operativni sistemi" },
                    { new Guid("b32b3430-1a1b-4b04-976e-04be069242c9"), "Marketing" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Bio", "Email", "FirstName", "LastName", "Password", "Status", "Username" },
                values: new object[,]
                {
                    { new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35"), "Student currently working on my last exam", "ilijastaninov@gmail.com", "Ilija", "Staninov", "timduncan22", "Web developer", "ilestaninov" },
                    { new Guid("da2fd609-d754-4feb-8acd-c4f9ff13ba96"), "Employee currently working on my masters exam", "nikolina@gmail.com", "Nikolina", "Staninova", "123456", "Economist", "nikolinastaninova" },
                    { new Guid("2902b665-1190-4c70-9915-b9c2d7680450"), "Old man who likes to sleep", "dedeile@gmail.com", "Dede", "Staninov", "123456", "Old man", "dedeile" }
                });

            migrationBuilder.InsertData(
                table: "Educations",
                columns: new[] { "EducationId", "Degree", "From", "To", "UserId" },
                values: new object[,]
                {
                    { new Guid("5b1c2b4d-48c7-402a-80c3-cc796ad49c6b"), "Bachelor", new DateTime(2014, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35") },
                    { new Guid("d8663e5e-7494-4f81-8739-6e0de1bea7ee"), "High school", new DateTime(2010, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2014, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35") },
                    { new Guid("d173e20d-159e-4127-9ce9-b0ac2564ad97"), "Economics Bachelor", new DateTime(2006, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2011, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("da2fd609-d754-4feb-8acd-c4f9ff13ba96") },
                    { new Guid("40ff5488-fdab-45b5-bc3a-14302d59869a"), "Economics Bachelor", new DateTime(1950, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1956, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("2902b665-1190-4c70-9915-b9c2d7680450") }
                });

            migrationBuilder.InsertData(
                table: "Experiences",
                columns: new[] { "ExperienceId", "Company", "Current", "From", "Title", "To", "UserId" },
                values: new object[,]
                {
                    { new Guid("0e447fdb-6224-4e61-ad28-2395cd9a118f"), "FINKI", false, new DateTime(2015, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Web developer", new DateTime(2016, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35") },
                    { new Guid("6efd04b2-5803-4a46-b065-972d99bcc5f1"), "Stadia Connect", false, new DateTime(2016, 12, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Web developer", new DateTime(2017, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Educations_UserId",
                table: "Educations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Experiences_UserId",
                table: "Experiences",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UserId",
                table: "Posts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCourses_CourseId",
                table: "UserCourses",
                column: "CourseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Educations");

            migrationBuilder.DropTable(
                name: "Experiences");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "UserCourses");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
