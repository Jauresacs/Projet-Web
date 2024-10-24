using Microsoft.EntityFrameworkCore;

namespace JobBoard.Models
{
    public class JobBoardContext : DbContext
    {

        private readonly IConfiguration _configuration;
         public JobBoardContext(DbContextOptions<JobBoardContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }
        public DbSet<User> Users { get; set; }
        public DbSet<Recruteur> Recruteurs { get; set; }
        public DbSet<Entreprise> Entreprises { get; set; }
        public DbSet<Candidature> Candidatures { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Skills> Skills { get; set; }

        public DbSet<Candidat> Candidats {get; set; }

        public DbSet<Admin> Admin {get; set; }
        public DbSet<FavoriteOffer> FavoriteOffers { get;  set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


    // Utilisation du service de hachage de mot de passe
    var userServices = new UserServices();
   var adminPassword = _configuration["AdminPassword"];
   var user1password = _configuration["User1"];
   var user2password = _configuration["User2"];
   var user3password = _configuration["User3"];
   var user4password = _configuration["User4"];
   var user5password = _configuration["User5"];

    if (string.IsNullOrEmpty(adminPassword))
    {
        throw new Exception("Le mot de passe administrateur n'est pas défini dans les variables d'environnement.");
    }

    var password = userServices.HashPassword(null, adminPassword); // Null si l'objet Admin n'a pas d'identité utilisateur spécifique
    var password1 = userServices.HashPassword(null, user1password);
    var password2 = userServices.HashPassword(null, user2password);
    var password3 = userServices.HashPassword(null, user3password);
    var password4 = userServices.HashPassword(null, user4password);
    var password5 = userServices.HashPassword(null, user5password);

            // Data Seed: Entreprises
            modelBuilder.Entity<Entreprise>().HasData(
                new Entreprise { Id = 1, Name = "TechCorp", Description = "Tech solutions company", SiteWeb = "https://techcorp.com", Adresse = "123 Tech Street" },
                new Entreprise { Id = 2, Name = "HealthPlus", Description = "Healthcare services provider", SiteWeb = "https://healthplus.com", Adresse = "456 Health Avenue" },
                new Entreprise { Id = 3, Name = "FinSolutions", Description = "Financial consulting firm", SiteWeb = "https://finsolutions.com", Adresse = "789 Finance Blvd" },
                new Entreprise { Id = 4, Name = "EduLearn", Description = "Educational platform", SiteWeb = "https://edulearn.com", Adresse = "101 Learning Way" },
                new Entreprise { Id = 5, Name = "GreenEnergy", Description = "Renewable energy provider", SiteWeb = "https://greenenergy.com", Adresse = "202 Solar Street" }
            );

            // Data Seed: Users (Recruteurs)
            modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Nom = "Jaures/Aurore", Role = "admin", Email = "jobboard@gmail.com", PasswordHash = password},
            new User { Id = 2, Nom = "Alice Johnson", Role = "recruiter", Email = "alice@techcorp.com", PasswordHash = password1},
            new User { Id = 3, Nom = "Bob Smith", Role = "recruiter", Email = "bob@healthplus.com", PasswordHash = password2},
            new User { Id = 4, Nom = "Carol White", Role = "recruiter", Email = "carol@finsolutions.com", PasswordHash = password3},
            new User { Id = 5, Nom = "David Brown", Role = "recruiter", Email = "david@edulearn.com", PasswordHash = password4},
            new User { Id = 6, Nom = "Emma Green", Role = "recruiter", Email = "emma@greenenergy.com", PasswordHash = password5}
            );

            modelBuilder.Entity<Admin>().HasData(
            new Admin { Id = 1, UserId = 1}
            );

            // Data Seed: Recruteurs
            modelBuilder.Entity<Recruteur>().HasData(
            new Recruteur { Id = 1, UserId = 2, EntrepriseId = 1, Role = "HR Manager" },
            new Recruteur { Id = 2, UserId = 3, EntrepriseId = 2, Role = "HR Specialist" },
            new Recruteur { Id = 3, UserId = 4, EntrepriseId = 3, Role = "HR Director" },
            new Recruteur { Id = 4, UserId = 5, EntrepriseId = 4, Role = "HR Consultant" },
            new Recruteur { Id = 5, UserId = 6, EntrepriseId = 5, Role = "HR Executive" }
            );

            // Data Seed: Jobs
            modelBuilder.Entity<Job>().HasData(
                // Jobs for Alice (Recruteur 1)
                new Job { Id = 1, Title = "Software Developer", Description = "Develop software applications", CompanyName = "TechCorp", JobType = "Full-Time", Location = "Remote", Salary = 60000, PostedDate = DateTime.Now, RecruteurId = 1, Category = "Technology" },
                new Job { Id = 2, Title = "UI/UX Designer", Description = "Design user interfaces", CompanyName = "TechCorp", JobType = "Full-Time", Location = "San Francisco", Salary = 55000, PostedDate = DateTime.Now, RecruteurId = 1, Category = "Design" },

                // Jobs for Bob (Recruteur 2)
                new Job { Id = 3, Title = "Data Analyst", Description = "Analyze healthcare data", CompanyName = "HealthPlus", JobType = "Part-Time", Location = "New York", Salary = 40000, PostedDate = DateTime.Now, RecruteurId = 2, Category = "Healthcare" },
                new Job { Id = 4, Title = "Healthcare Consultant", Description = "Provide healthcare consulting services", CompanyName = "HealthPlus", JobType = "Contract", Location = "New York", Salary = 65000, PostedDate = DateTime.Now, RecruteurId = 2, Category = "Consulting" },

                // Jobs for Carol (Recruteur 3)
                new Job { Id = 5, Title = "Financial Analyst", Description = "Analyze financial data", CompanyName = "FinSolutions", JobType = "Full-Time", Location = "Chicago", Salary = 70000, PostedDate = DateTime.Now, RecruteurId = 3, Category = "Finance" },
                new Job { Id = 6, Title = "Accountant", Description = "Manage financial records", CompanyName = "FinSolutions", JobType = "Full-Time", Location = "New York", Salary = 60000, PostedDate = DateTime.Now, RecruteurId = 3, Category = "Finance" },

                // Jobs for David (Recruteur 4)
                new Job { Id = 7, Title = "Instructional Designer", Description = "Design educational content", CompanyName = "EduLearn", JobType = "Contract", Location = "Remote", Salary = 50000, PostedDate = DateTime.Now, RecruteurId = 4, Category = "Education" },
                new Job { Id = 8, Title = "Education Consultant", Description = "Consulting for educational platforms", CompanyName = "EduLearn", JobType = "Full-Time", Location = "Remote", Salary = 60000, PostedDate = DateTime.Now, RecruteurId = 4, Category = "Consulting" },

                // Jobs for Emma (Recruteur 5)
                new Job { Id = 9, Title = "Renewable Energy Engineer", Description = "Design renewable energy systems", CompanyName = "GreenEnergy", JobType = "Full-Time", Location = "Los Angeles", Salary = 75000, PostedDate = DateTime.Now, RecruteurId = 5, Category = "Energy" },
                new Job { Id = 10, Title = "Environmental Scientist", Description = "Study environmental impacts", CompanyName = "GreenEnergy", JobType = "Full-Time", Location = "Denver", Salary = 65000, PostedDate = DateTime.Now, RecruteurId = 5, Category = "Science" },

                // Additional Jobs for other recruteurs (distribute more jobs)
                new Job { Id = 11, Title = "Marketing Specialist", Description = "Plan and execute marketing campaigns", CompanyName = "TechCorp", JobType = "Full-Time", Location = "Remote", Salary = 50000, PostedDate = DateTime.Now, RecruteurId = 1, Category = "Marketing" },
                new Job { Id = 12, Title = "Project Manager", Description = "Manage IT projects", CompanyName = "TechCorp", JobType = "Contract", Location = "Remote", Salary = 70000, PostedDate = DateTime.Now, RecruteurId = 1, Category = "Management" },
                new Job { Id = 13, Title = "Sales Representative", Description = "Sell healthcare services", CompanyName = "HealthPlus", JobType = "Full-Time", Location = "Chicago", Salary = 45000, PostedDate = DateTime.Now, RecruteurId = 2, Category = "Sales" },
                new Job { Id = 14, Title = "Network Engineer", Description = "Maintain network infrastructure", CompanyName = "TechCorp", JobType = "Full-Time", Location = "Dallas", Salary = 65000, PostedDate = DateTime.Now, RecruteurId = 1, Category = "Technology" },
                new Job { Id = 15, Title = "Content Writer", Description = "Write content for websites and blogs", CompanyName = "TechCorp", JobType = "Contract", Location = "Remote", Salary = 40000, PostedDate = DateTime.Now, RecruteurId = 1, Category = "Content Writing" }
            );


            //Relation un-à-un entre User et Admin
            modelBuilder.Entity<User>()
                .HasOne(u => u.Admin)
                .WithOne(a => a.User)
                .HasForeignKey<Admin>(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            // Relation un-à-un entre User et Candidat
            modelBuilder.Entity<User>()
                .HasOne(u => u.Candidat)
                .WithOne(c => c.User)
                .HasForeignKey<Candidat>(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Education>()
                .HasOne(e => e.Candidat)
                .WithMany(c => c.Educations)
                .HasForeignKey(e => e.CandidatId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relation un-à-un entre User et Recruteur
            modelBuilder.Entity<User>()
                .HasOne(u => u.Recruteur)
                .WithOne(r => r.User)
                .HasForeignKey<Recruteur>(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relation un-à-plusieurs : Entreprise -> Recruteurs
            modelBuilder.Entity<Recruteur>()
                .HasOne(r => r.Entreprise)
                .WithMany(e => e.Recruteurs)
                .HasForeignKey(r => r.EntrepriseId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relation un-à-plusieurs : Candidat -> Experiences
            modelBuilder.Entity<Experience>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(e => e.IdUser)
                .OnDelete(DeleteBehavior.Cascade);

            // Relation un-à-plusieurs : Candidat -> Candidatures
           modelBuilder.Entity<Candidature>()
                .HasOne(c => c.Candidat)  // Une candidature a un candidat
                .WithMany(c => c.Candidatures)  // Un candidat a plusieurs candidatures
                .HasForeignKey(c => c.CandidatId)  // Utilise CandidatId comme clé étrangère
                .OnDelete(DeleteBehavior.Cascade); 

            // Relation un-à-plusieurs : Recruteur -> Jobs
            modelBuilder.Entity<Job>()
                .HasOne(j => j.Recruteur)
                .WithMany(r => r.Jobs)
                .HasForeignKey(j => j.RecruteurId)
                .OnDelete(DeleteBehavior.SetNull);

            // Relation un-à-plusieurs : Skills -> User (Candidat)
            modelBuilder.Entity<Skills>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relation un-à-plusieurs : Candidature -> Job
            modelBuilder.Entity<Candidature>()
                .HasOne(c => c.Job)
                .WithMany(j => j.Candidatures)
                .HasForeignKey(c => c.JobId)
                .OnDelete(DeleteBehavior.Cascade); 

            modelBuilder.Entity<FavoriteOffer>()
                .HasKey(f => new { f.UserId, f.JobId });

            modelBuilder.Entity<FavoriteOffer>()
                .HasOne(f => f.User)
                .WithMany(u => u.FavoriteOffers)
                .HasForeignKey(f => f.UserId);

            modelBuilder.Entity<FavoriteOffer>()
                .HasOne(f => f.Job)
                .WithMany()
                .HasForeignKey(f => f.JobId);

            base.OnModelCreating(modelBuilder);
        }
    }
}