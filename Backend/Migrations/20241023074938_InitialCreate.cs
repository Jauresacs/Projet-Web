using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace JobBoard.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Entreprises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SiteWeb = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Adresse = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entreprises", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Role = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nom = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PasswordHash = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DateInscription = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Admin_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Candidats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    CvUrl = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateInscription = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Candidats_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Recruteurs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    EntrepriseId = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateInscription = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recruteurs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recruteurs_Entreprises_EntrepriseId",
                        column: x => x.EntrepriseId,
                        principalTable: "Entreprises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Recruteurs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Educations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CandidatId = table.Column<int>(type: "int", nullable: false),
                    Institution = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Diplome = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateDebut = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateFin = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Educations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Educations_Candidats_CandidatId",
                        column: x => x.CandidatId,
                        principalTable: "Candidats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Experiences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdUser = table.Column<int>(type: "int", nullable: false),
                    TitrePoste = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NameEntreprise = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TitrePosteType = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateDebut = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateFin = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CandidatId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experiences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Experiences_Candidats_CandidatId",
                        column: x => x.CandidatId,
                        principalTable: "Candidats",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Experiences_Users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Niveau = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CandidatId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Skills_Candidats_CandidatId",
                        column: x => x.CandidatId,
                        principalTable: "Candidats",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Skills_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Category = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CompanyName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    JobType = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Location = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Salary = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    PostedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    RecruteurId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Jobs_Recruteurs_RecruteurId",
                        column: x => x.RecruteurId,
                        principalTable: "Recruteurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Candidatures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CandidatId = table.Column<int>(type: "int", nullable: true),
                    JobId = table.Column<int>(type: "int", nullable: false),
                    DateCandidature = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ApplicantName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ApplicantEmail = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ApplicantNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ApplicantMessage = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Candidatures_Candidats_CandidatId",
                        column: x => x.CandidatId,
                        principalTable: "Candidats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Candidatures_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "FavoriteOffers",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    JobId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteOffers", x => new { x.UserId, x.JobId });
                    table.ForeignKey(
                        name: "FK_FavoriteOffers_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavoriteOffers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Entreprises",
                columns: new[] { "Id", "Adresse", "Description", "Name", "SiteWeb" },
                values: new object[,]
                {
                    { 1, "123 Tech Street", "Tech solutions company", "TechCorp", "https://techcorp.com" },
                    { 2, "456 Health Avenue", "Healthcare services provider", "HealthPlus", "https://healthplus.com" },
                    { 3, "789 Finance Blvd", "Financial consulting firm", "FinSolutions", "https://finsolutions.com" },
                    { 4, "101 Learning Way", "Educational platform", "EduLearn", "https://edulearn.com" },
                    { 5, "202 Solar Street", "Renewable energy provider", "GreenEnergy", "https://greenenergy.com" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Nom", "PasswordHash", "Role" },
                values: new object[,]
                {
                    { 1, "jobboard@gmail.com", "Jaures/Aurore", "AQAAAAIAAYagAAAAEB6t2XVJbKFSvc/yAA7TQAwKYRQCyfsvDWmQTXgQaM+duLUzpo383JHmSr4C0q2wKA==", "admin" },
                    { 2, "alice@techcorp.com", "Alice Johnson", "AQAAAAIAAYagAAAAEBOgvU2meJIfXETppmkDBc13mKRJ5ukzOry1/T2iM1Q/Z5AvY9V6kxZnAzkoXj9d0w==", "recruiter" },
                    { 3, "bob@healthplus.com", "Bob Smith", "AQAAAAIAAYagAAAAEMsvu1o20eR7sPdq+LzZ9da16BpB72jSJohqxhRYJM1JWyew3htf0jigpApfW8tnTQ==", "recruiter" },
                    { 4, "carol@finsolutions.com", "Carol White", "AQAAAAIAAYagAAAAEGcwnv233z1c10eexkYfZ513MF9ZTiRdZkSjsMKbFVIUojNA97MdinvKe8Vz2ZMM1A==", "recruiter" },
                    { 5, "david@edulearn.com", "David Brown", "AQAAAAIAAYagAAAAEMEyqQLqNWn/awYKLBgVkR7MLOX/CZf9CiaF8akdTBQxOIOuq3F4aKNkI/Tx93a35Q==", "recruiter" },
                    { 6, "emma@greenenergy.com", "Emma Green", "AQAAAAIAAYagAAAAEJAqvjL6GuSrLMaK8cfguh4ZLH9JahO4u9NMhmqyy98G4iCpcpPfeuArAZSTpWIR1g==", "recruiter" }
                });

            migrationBuilder.InsertData(
                table: "Admin",
                columns: new[] { "Id", "DateInscription", "UserId" },
                values: new object[] { 1, new DateTime(2024, 10, 23, 9, 49, 37, 708, DateTimeKind.Local).AddTicks(3346), 1 });

            migrationBuilder.InsertData(
                table: "Recruteurs",
                columns: new[] { "Id", "DateInscription", "EntrepriseId", "Role", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 23, 9, 49, 37, 708, DateTimeKind.Local).AddTicks(3497), 1, "HR Manager", 2 },
                    { 2, new DateTime(2024, 10, 23, 9, 49, 37, 708, DateTimeKind.Local).AddTicks(3501), 2, "HR Specialist", 3 },
                    { 3, new DateTime(2024, 10, 23, 9, 49, 37, 708, DateTimeKind.Local).AddTicks(3504), 3, "HR Director", 4 },
                    { 4, new DateTime(2024, 10, 23, 9, 49, 37, 708, DateTimeKind.Local).AddTicks(3506), 4, "HR Consultant", 5 },
                    { 5, new DateTime(2024, 10, 23, 9, 49, 37, 708, DateTimeKind.Local).AddTicks(3514), 5, "HR Executive", 6 }
                });

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "Id", "Category", "CompanyName", "Description", "JobType", "Location", "PostedDate", "RecruteurId", "Salary", "Title" },
                values: new object[,]
                {
                    { 1, "Technology", "TechCorp", "Develop software applications", "Full-Time", "Remote", new DateTime(2024, 10, 23, 9, 49, 37, 708, DateTimeKind.Local).AddTicks(3614), 1, 60000m, "Software Developer" },
                    { 2, "Design", "TechCorp", "Design user interfaces", "Full-Time", "San Francisco", new DateTime(2024, 10, 23, 9, 49, 37, 708, DateTimeKind.Local).AddTicks(3621), 1, 55000m, "UI/UX Designer" },
                    { 3, "Healthcare", "HealthPlus", "Analyze healthcare data", "Part-Time", "New York", new DateTime(2024, 10, 23, 9, 49, 37, 708, DateTimeKind.Local).AddTicks(3637), 2, 40000m, "Data Analyst" },
                    { 4, "Consulting", "HealthPlus", "Provide healthcare consulting services", "Contract", "New York", new DateTime(2024, 10, 23, 9, 49, 37, 708, DateTimeKind.Local).AddTicks(3642), 2, 65000m, "Healthcare Consultant" },
                    { 5, "Finance", "FinSolutions", "Analyze financial data", "Full-Time", "Chicago", new DateTime(2024, 10, 23, 9, 49, 37, 708, DateTimeKind.Local).AddTicks(3682), 3, 70000m, "Financial Analyst" },
                    { 6, "Finance", "FinSolutions", "Manage financial records", "Full-Time", "New York", new DateTime(2024, 10, 23, 9, 49, 37, 708, DateTimeKind.Local).AddTicks(3689), 3, 60000m, "Accountant" },
                    { 7, "Education", "EduLearn", "Design educational content", "Contract", "Remote", new DateTime(2024, 10, 23, 9, 49, 37, 708, DateTimeKind.Local).AddTicks(3694), 4, 50000m, "Instructional Designer" },
                    { 8, "Consulting", "EduLearn", "Consulting for educational platforms", "Full-Time", "Remote", new DateTime(2024, 10, 23, 9, 49, 37, 708, DateTimeKind.Local).AddTicks(3701), 4, 60000m, "Education Consultant" },
                    { 9, "Energy", "GreenEnergy", "Design renewable energy systems", "Full-Time", "Los Angeles", new DateTime(2024, 10, 23, 9, 49, 37, 708, DateTimeKind.Local).AddTicks(3706), 5, 75000m, "Renewable Energy Engineer" },
                    { 10, "Science", "GreenEnergy", "Study environmental impacts", "Full-Time", "Denver", new DateTime(2024, 10, 23, 9, 49, 37, 708, DateTimeKind.Local).AddTicks(3809), 5, 65000m, "Environmental Scientist" },
                    { 11, "Marketing", "TechCorp", "Plan and execute marketing campaigns", "Full-Time", "Remote", new DateTime(2024, 10, 23, 9, 49, 37, 708, DateTimeKind.Local).AddTicks(3815), 1, 50000m, "Marketing Specialist" },
                    { 12, "Management", "TechCorp", "Manage IT projects", "Contract", "Remote", new DateTime(2024, 10, 23, 9, 49, 37, 708, DateTimeKind.Local).AddTicks(3833), 1, 70000m, "Project Manager" },
                    { 13, "Sales", "HealthPlus", "Sell healthcare services", "Full-Time", "Chicago", new DateTime(2024, 10, 23, 9, 49, 37, 708, DateTimeKind.Local).AddTicks(3896), 2, 45000m, "Sales Representative" },
                    { 14, "Technology", "TechCorp", "Maintain network infrastructure", "Full-Time", "Dallas", new DateTime(2024, 10, 23, 9, 49, 37, 708, DateTimeKind.Local).AddTicks(3902), 1, 65000m, "Network Engineer" },
                    { 15, "Content Writing", "TechCorp", "Write content for websites and blogs", "Contract", "Remote", new DateTime(2024, 10, 23, 9, 49, 37, 708, DateTimeKind.Local).AddTicks(3908), 1, 40000m, "Content Writer" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Admin_UserId",
                table: "Admin",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Candidats_UserId",
                table: "Candidats",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Candidatures_CandidatId",
                table: "Candidatures",
                column: "CandidatId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidatures_JobId",
                table: "Candidatures",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_Educations_CandidatId",
                table: "Educations",
                column: "CandidatId");

            migrationBuilder.CreateIndex(
                name: "IX_Experiences_CandidatId",
                table: "Experiences",
                column: "CandidatId");

            migrationBuilder.CreateIndex(
                name: "IX_Experiences_IdUser",
                table: "Experiences",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteOffers_JobId",
                table: "FavoriteOffers",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_RecruteurId",
                table: "Jobs",
                column: "RecruteurId");

            migrationBuilder.CreateIndex(
                name: "IX_Recruteurs_EntrepriseId",
                table: "Recruteurs",
                column: "EntrepriseId");

            migrationBuilder.CreateIndex(
                name: "IX_Recruteurs_UserId",
                table: "Recruteurs",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Skills_CandidatId",
                table: "Skills",
                column: "CandidatId");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_UserId",
                table: "Skills",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropTable(
                name: "Candidatures");

            migrationBuilder.DropTable(
                name: "Educations");

            migrationBuilder.DropTable(
                name: "Experiences");

            migrationBuilder.DropTable(
                name: "FavoriteOffers");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropTable(
                name: "Candidats");

            migrationBuilder.DropTable(
                name: "Recruteurs");

            migrationBuilder.DropTable(
                name: "Entreprises");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
