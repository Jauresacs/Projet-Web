﻿// <auto-generated />
using System;
using JobBoard.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace JobBoard.Migrations
{
    [DbContext(typeof(JobBoardContext))]
    [Migration("20241023074938_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Job", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("JobType")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("PostedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("RecruteurId")
                        .HasColumnType("int");

                    b.Property<decimal>("Salary")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("RecruteurId");

                    b.ToTable("Jobs");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Category = "Technology",
                            CompanyName = "TechCorp",
                            Description = "Develop software applications",
                            JobType = "Full-Time",
                            Location = "Remote",
                            PostedDate = new DateTime(2024, 10, 23, 9, 49, 37, 708, DateTimeKind.Local).AddTicks(3614),
                            RecruteurId = 1,
                            Salary = 60000m,
                            Title = "Software Developer"
                        },
                        new
                        {
                            Id = 2,
                            Category = "Design",
                            CompanyName = "TechCorp",
                            Description = "Design user interfaces",
                            JobType = "Full-Time",
                            Location = "San Francisco",
                            PostedDate = new DateTime(2024, 10, 23, 9, 49, 37, 708, DateTimeKind.Local).AddTicks(3621),
                            RecruteurId = 1,
                            Salary = 55000m,
                            Title = "UI/UX Designer"
                        },
                        new
                        {
                            Id = 3,
                            Category = "Healthcare",
                            CompanyName = "HealthPlus",
                            Description = "Analyze healthcare data",
                            JobType = "Part-Time",
                            Location = "New York",
                            PostedDate = new DateTime(2024, 10, 23, 9, 49, 37, 708, DateTimeKind.Local).AddTicks(3637),
                            RecruteurId = 2,
                            Salary = 40000m,
                            Title = "Data Analyst"
                        },
                        new
                        {
                            Id = 4,
                            Category = "Consulting",
                            CompanyName = "HealthPlus",
                            Description = "Provide healthcare consulting services",
                            JobType = "Contract",
                            Location = "New York",
                            PostedDate = new DateTime(2024, 10, 23, 9, 49, 37, 708, DateTimeKind.Local).AddTicks(3642),
                            RecruteurId = 2,
                            Salary = 65000m,
                            Title = "Healthcare Consultant"
                        },
                        new
                        {
                            Id = 5,
                            Category = "Finance",
                            CompanyName = "FinSolutions",
                            Description = "Analyze financial data",
                            JobType = "Full-Time",
                            Location = "Chicago",
                            PostedDate = new DateTime(2024, 10, 23, 9, 49, 37, 708, DateTimeKind.Local).AddTicks(3682),
                            RecruteurId = 3,
                            Salary = 70000m,
                            Title = "Financial Analyst"
                        },
                        new
                        {
                            Id = 6,
                            Category = "Finance",
                            CompanyName = "FinSolutions",
                            Description = "Manage financial records",
                            JobType = "Full-Time",
                            Location = "New York",
                            PostedDate = new DateTime(2024, 10, 23, 9, 49, 37, 708, DateTimeKind.Local).AddTicks(3689),
                            RecruteurId = 3,
                            Salary = 60000m,
                            Title = "Accountant"
                        },
                        new
                        {
                            Id = 7,
                            Category = "Education",
                            CompanyName = "EduLearn",
                            Description = "Design educational content",
                            JobType = "Contract",
                            Location = "Remote",
                            PostedDate = new DateTime(2024, 10, 23, 9, 49, 37, 708, DateTimeKind.Local).AddTicks(3694),
                            RecruteurId = 4,
                            Salary = 50000m,
                            Title = "Instructional Designer"
                        },
                        new
                        {
                            Id = 8,
                            Category = "Consulting",
                            CompanyName = "EduLearn",
                            Description = "Consulting for educational platforms",
                            JobType = "Full-Time",
                            Location = "Remote",
                            PostedDate = new DateTime(2024, 10, 23, 9, 49, 37, 708, DateTimeKind.Local).AddTicks(3701),
                            RecruteurId = 4,
                            Salary = 60000m,
                            Title = "Education Consultant"
                        },
                        new
                        {
                            Id = 9,
                            Category = "Energy",
                            CompanyName = "GreenEnergy",
                            Description = "Design renewable energy systems",
                            JobType = "Full-Time",
                            Location = "Los Angeles",
                            PostedDate = new DateTime(2024, 10, 23, 9, 49, 37, 708, DateTimeKind.Local).AddTicks(3706),
                            RecruteurId = 5,
                            Salary = 75000m,
                            Title = "Renewable Energy Engineer"
                        },
                        new
                        {
                            Id = 10,
                            Category = "Science",
                            CompanyName = "GreenEnergy",
                            Description = "Study environmental impacts",
                            JobType = "Full-Time",
                            Location = "Denver",
                            PostedDate = new DateTime(2024, 10, 23, 9, 49, 37, 708, DateTimeKind.Local).AddTicks(3809),
                            RecruteurId = 5,
                            Salary = 65000m,
                            Title = "Environmental Scientist"
                        },
                        new
                        {
                            Id = 11,
                            Category = "Marketing",
                            CompanyName = "TechCorp",
                            Description = "Plan and execute marketing campaigns",
                            JobType = "Full-Time",
                            Location = "Remote",
                            PostedDate = new DateTime(2024, 10, 23, 9, 49, 37, 708, DateTimeKind.Local).AddTicks(3815),
                            RecruteurId = 1,
                            Salary = 50000m,
                            Title = "Marketing Specialist"
                        },
                        new
                        {
                            Id = 12,
                            Category = "Management",
                            CompanyName = "TechCorp",
                            Description = "Manage IT projects",
                            JobType = "Contract",
                            Location = "Remote",
                            PostedDate = new DateTime(2024, 10, 23, 9, 49, 37, 708, DateTimeKind.Local).AddTicks(3833),
                            RecruteurId = 1,
                            Salary = 70000m,
                            Title = "Project Manager"
                        },
                        new
                        {
                            Id = 13,
                            Category = "Sales",
                            CompanyName = "HealthPlus",
                            Description = "Sell healthcare services",
                            JobType = "Full-Time",
                            Location = "Chicago",
                            PostedDate = new DateTime(2024, 10, 23, 9, 49, 37, 708, DateTimeKind.Local).AddTicks(3896),
                            RecruteurId = 2,
                            Salary = 45000m,
                            Title = "Sales Representative"
                        },
                        new
                        {
                            Id = 14,
                            Category = "Technology",
                            CompanyName = "TechCorp",
                            Description = "Maintain network infrastructure",
                            JobType = "Full-Time",
                            Location = "Dallas",
                            PostedDate = new DateTime(2024, 10, 23, 9, 49, 37, 708, DateTimeKind.Local).AddTicks(3902),
                            RecruteurId = 1,
                            Salary = 65000m,
                            Title = "Network Engineer"
                        },
                        new
                        {
                            Id = 15,
                            Category = "Content Writing",
                            CompanyName = "TechCorp",
                            Description = "Write content for websites and blogs",
                            JobType = "Contract",
                            Location = "Remote",
                            PostedDate = new DateTime(2024, 10, 23, 9, 49, 37, 708, DateTimeKind.Local).AddTicks(3908),
                            RecruteurId = 1,
                            Salary = 40000m,
                            Title = "Content Writer"
                        });
                });

            modelBuilder.Entity("JobBoard.Admin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateInscription")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Admin");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateInscription = new DateTime(2024, 10, 23, 9, 49, 37, 708, DateTimeKind.Local).AddTicks(3346),
                            UserId = 1
                        });
                });

            modelBuilder.Entity("JobBoard.Models.Candidat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CvUrl")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("DateInscription")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Candidats");
                });

            modelBuilder.Entity("JobBoard.Models.Candidature", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ApplicantEmail")
                        .HasColumnType("longtext");

                    b.Property<string>("ApplicantMessage")
                        .HasColumnType("longtext");

                    b.Property<string>("ApplicantName")
                        .HasColumnType("longtext");

                    b.Property<string>("ApplicantNumber")
                        .HasColumnType("longtext");

                    b.Property<int?>("CandidatId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCandidature")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("JobId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CandidatId");

                    b.HasIndex("JobId");

                    b.ToTable("Candidatures");
                });

            modelBuilder.Entity("JobBoard.Models.Education", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CandidatId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateDebut")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateFin")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("Diplome")
                        .HasColumnType("longtext");

                    b.Property<string>("Institution")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("CandidatId");

                    b.ToTable("Educations");
                });

            modelBuilder.Entity("JobBoard.Models.Entreprise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Adresse")
                        .HasColumnType("longtext");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("SiteWeb")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Entreprises");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Adresse = "123 Tech Street",
                            Description = "Tech solutions company",
                            Name = "TechCorp",
                            SiteWeb = "https://techcorp.com"
                        },
                        new
                        {
                            Id = 2,
                            Adresse = "456 Health Avenue",
                            Description = "Healthcare services provider",
                            Name = "HealthPlus",
                            SiteWeb = "https://healthplus.com"
                        },
                        new
                        {
                            Id = 3,
                            Adresse = "789 Finance Blvd",
                            Description = "Financial consulting firm",
                            Name = "FinSolutions",
                            SiteWeb = "https://finsolutions.com"
                        },
                        new
                        {
                            Id = 4,
                            Adresse = "101 Learning Way",
                            Description = "Educational platform",
                            Name = "EduLearn",
                            SiteWeb = "https://edulearn.com"
                        },
                        new
                        {
                            Id = 5,
                            Adresse = "202 Solar Street",
                            Description = "Renewable energy provider",
                            Name = "GreenEnergy",
                            SiteWeb = "https://greenenergy.com"
                        });
                });

            modelBuilder.Entity("JobBoard.Models.Experience", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CandidatId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateDebut")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateFin")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<int>("IdUser")
                        .HasColumnType("int");

                    b.Property<string>("NameEntreprise")
                        .HasColumnType("longtext");

                    b.Property<string>("TitrePoste")
                        .HasColumnType("longtext");

                    b.Property<string>("TitrePosteType")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("CandidatId");

                    b.HasIndex("IdUser");

                    b.ToTable("Experiences");
                });

            modelBuilder.Entity("JobBoard.Models.FavoriteOffer", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("JobId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "JobId");

                    b.HasIndex("JobId");

                    b.ToTable("FavoriteOffers");
                });

            modelBuilder.Entity("JobBoard.Models.Recruteur", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateInscription")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("EntrepriseId")
                        .HasColumnType("int");

                    b.Property<string>("Role")
                        .HasColumnType("longtext");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EntrepriseId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Recruteurs");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateInscription = new DateTime(2024, 10, 23, 9, 49, 37, 708, DateTimeKind.Local).AddTicks(3497),
                            EntrepriseId = 1,
                            Role = "HR Manager",
                            UserId = 2
                        },
                        new
                        {
                            Id = 2,
                            DateInscription = new DateTime(2024, 10, 23, 9, 49, 37, 708, DateTimeKind.Local).AddTicks(3501),
                            EntrepriseId = 2,
                            Role = "HR Specialist",
                            UserId = 3
                        },
                        new
                        {
                            Id = 3,
                            DateInscription = new DateTime(2024, 10, 23, 9, 49, 37, 708, DateTimeKind.Local).AddTicks(3504),
                            EntrepriseId = 3,
                            Role = "HR Director",
                            UserId = 4
                        },
                        new
                        {
                            Id = 4,
                            DateInscription = new DateTime(2024, 10, 23, 9, 49, 37, 708, DateTimeKind.Local).AddTicks(3506),
                            EntrepriseId = 4,
                            Role = "HR Consultant",
                            UserId = 5
                        },
                        new
                        {
                            Id = 5,
                            DateInscription = new DateTime(2024, 10, 23, 9, 49, 37, 708, DateTimeKind.Local).AddTicks(3514),
                            EntrepriseId = 5,
                            Role = "HR Executive",
                            UserId = 6
                        });
                });

            modelBuilder.Entity("JobBoard.Models.Skills", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CandidatId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("Niveau")
                        .HasColumnType("longtext");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CandidatId");

                    b.HasIndex("UserId");

                    b.ToTable("Skills");
                });

            modelBuilder.Entity("JobBoard.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Role")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "jobboard@gmail.com",
                            Nom = "Jaures/Aurore",
                            PasswordHash = "AQAAAAIAAYagAAAAEB6t2XVJbKFSvc/yAA7TQAwKYRQCyfsvDWmQTXgQaM+duLUzpo383JHmSr4C0q2wKA==",
                            Role = "admin"
                        },
                        new
                        {
                            Id = 2,
                            Email = "alice@techcorp.com",
                            Nom = "Alice Johnson",
                            PasswordHash = "AQAAAAIAAYagAAAAEBOgvU2meJIfXETppmkDBc13mKRJ5ukzOry1/T2iM1Q/Z5AvY9V6kxZnAzkoXj9d0w==",
                            Role = "recruiter"
                        },
                        new
                        {
                            Id = 3,
                            Email = "bob@healthplus.com",
                            Nom = "Bob Smith",
                            PasswordHash = "AQAAAAIAAYagAAAAEMsvu1o20eR7sPdq+LzZ9da16BpB72jSJohqxhRYJM1JWyew3htf0jigpApfW8tnTQ==",
                            Role = "recruiter"
                        },
                        new
                        {
                            Id = 4,
                            Email = "carol@finsolutions.com",
                            Nom = "Carol White",
                            PasswordHash = "AQAAAAIAAYagAAAAEGcwnv233z1c10eexkYfZ513MF9ZTiRdZkSjsMKbFVIUojNA97MdinvKe8Vz2ZMM1A==",
                            Role = "recruiter"
                        },
                        new
                        {
                            Id = 5,
                            Email = "david@edulearn.com",
                            Nom = "David Brown",
                            PasswordHash = "AQAAAAIAAYagAAAAEMEyqQLqNWn/awYKLBgVkR7MLOX/CZf9CiaF8akdTBQxOIOuq3F4aKNkI/Tx93a35Q==",
                            Role = "recruiter"
                        },
                        new
                        {
                            Id = 6,
                            Email = "emma@greenenergy.com",
                            Nom = "Emma Green",
                            PasswordHash = "AQAAAAIAAYagAAAAEJAqvjL6GuSrLMaK8cfguh4ZLH9JahO4u9NMhmqyy98G4iCpcpPfeuArAZSTpWIR1g==",
                            Role = "recruiter"
                        });
                });

            modelBuilder.Entity("Job", b =>
                {
                    b.HasOne("JobBoard.Models.Recruteur", "Recruteur")
                        .WithMany("Jobs")
                        .HasForeignKey("RecruteurId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Recruteur");
                });

            modelBuilder.Entity("JobBoard.Admin", b =>
                {
                    b.HasOne("JobBoard.Models.User", "User")
                        .WithOne("Admin")
                        .HasForeignKey("JobBoard.Admin", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("JobBoard.Models.Candidat", b =>
                {
                    b.HasOne("JobBoard.Models.User", "User")
                        .WithOne("Candidat")
                        .HasForeignKey("JobBoard.Models.Candidat", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("User");
                });

            modelBuilder.Entity("JobBoard.Models.Candidature", b =>
                {
                    b.HasOne("JobBoard.Models.Candidat", "Candidat")
                        .WithMany("Candidatures")
                        .HasForeignKey("CandidatId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Job", "Job")
                        .WithMany("Candidatures")
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Candidat");

                    b.Navigation("Job");
                });

            modelBuilder.Entity("JobBoard.Models.Education", b =>
                {
                    b.HasOne("JobBoard.Models.Candidat", "Candidat")
                        .WithMany("Educations")
                        .HasForeignKey("CandidatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Candidat");
                });

            modelBuilder.Entity("JobBoard.Models.Experience", b =>
                {
                    b.HasOne("JobBoard.Models.Candidat", null)
                        .WithMany("Experiences")
                        .HasForeignKey("CandidatId");

                    b.HasOne("JobBoard.Models.User", null)
                        .WithMany()
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("JobBoard.Models.FavoriteOffer", b =>
                {
                    b.HasOne("Job", "Job")
                        .WithMany()
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JobBoard.Models.User", "User")
                        .WithMany("FavoriteOffers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Job");

                    b.Navigation("User");
                });

            modelBuilder.Entity("JobBoard.Models.Recruteur", b =>
                {
                    b.HasOne("JobBoard.Models.Entreprise", "Entreprise")
                        .WithMany("Recruteurs")
                        .HasForeignKey("EntrepriseId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("JobBoard.Models.User", "User")
                        .WithOne("Recruteur")
                        .HasForeignKey("JobBoard.Models.Recruteur", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Entreprise");

                    b.Navigation("User");
                });

            modelBuilder.Entity("JobBoard.Models.Skills", b =>
                {
                    b.HasOne("JobBoard.Models.Candidat", null)
                        .WithMany("Competences")
                        .HasForeignKey("CandidatId");

                    b.HasOne("JobBoard.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Job", b =>
                {
                    b.Navigation("Candidatures");
                });

            modelBuilder.Entity("JobBoard.Models.Candidat", b =>
                {
                    b.Navigation("Candidatures");

                    b.Navigation("Competences");

                    b.Navigation("Educations");

                    b.Navigation("Experiences");
                });

            modelBuilder.Entity("JobBoard.Models.Entreprise", b =>
                {
                    b.Navigation("Recruteurs");
                });

            modelBuilder.Entity("JobBoard.Models.Recruteur", b =>
                {
                    b.Navigation("Jobs");
                });

            modelBuilder.Entity("JobBoard.Models.User", b =>
                {
                    b.Navigation("Admin");

                    b.Navigation("Candidat");

                    b.Navigation("FavoriteOffers");

                    b.Navigation("Recruteur");
                });
#pragma warning restore 612, 618
        }
    }
}
