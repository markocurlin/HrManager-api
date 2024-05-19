using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRManager.APIv2.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Candidates",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DateOfApplication = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    DateOfBirth = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Profession = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Employment = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    EducationDegree = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Education = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CandidateFile = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EducationDegrees",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationDegrees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkingPlaces",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingPlaces", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Interviews",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateOfInterview = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Position = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Evaluation = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    SelectEmployment = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    DateOfEmployment = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CandidateId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Interviews_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CandidateWorkingPlace",
                columns: table => new
                {
                    CandidatesId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WorkingPlacesId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateWorkingPlace", x => new { x.CandidatesId, x.WorkingPlacesId });
                    table.ForeignKey(
                        name: "FK_CandidateWorkingPlace_Candidates_CandidatesId",
                        column: x => x.CandidatesId,
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CandidateWorkingPlace_WorkingPlaces_WorkingPlacesId",
                        column: x => x.WorkingPlacesId,
                        principalTable: "WorkingPlaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "EducationDegrees",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "151dba72-2400-43d6-9e33-cadbb71b865b", "mag. ing." },
                    { "510df9ac-baca-43b6-9e4a-cdda5f419428", "dr. sc." },
                    { "91dd93b1-403c-4913-b7fe-917bb0c35996", "univ. bacc. ing." }
                });

            migrationBuilder.InsertData(
                table: "Positions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "151dba72-2400-43d6-9e33-cadbb71b865b", "Backend" },
                    { "510df9ac-baca-43b6-9e4a-cdda5f419428", "Frontend" }
                });

            migrationBuilder.InsertData(
                table: "UserProfiles",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Role", "UserName" },
                values: new object[,]
                {
                    { "151dba72-2400-43d6-9e33-cadbb71b865b", "marko@gmail.com", "Marko", "Curlin", "Admin", "marko@gmail.com" },
                    { "510df9ac-baca-43b6-9e4a-cdda5f419428", "ana@gmail.com", "Ana", "Anic", "Edit", "ana@gmail.com" },
                    { "91dd93b1-403c-4913-b7fe-917bb0c35996", "filip@gmail.com", "Filip", "Filic", "View", "filip@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "WorkingPlaces",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "151dba72-2400-43d6-9e33-cadbb71b865b", "Backend" },
                    { "510df9ac-baca-43b6-9e4a-cdda5f419428", "Frontend" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CandidateWorkingPlace_WorkingPlacesId",
                table: "CandidateWorkingPlace",
                column: "WorkingPlacesId");

            migrationBuilder.CreateIndex(
                name: "IX_Interviews_CandidateId",
                table: "Interviews",
                column: "CandidateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CandidateWorkingPlace");

            migrationBuilder.DropTable(
                name: "EducationDegrees");

            migrationBuilder.DropTable(
                name: "Interviews");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "UserProfiles");

            migrationBuilder.DropTable(
                name: "WorkingPlaces");

            migrationBuilder.DropTable(
                name: "Candidates");
        }
    }
}
