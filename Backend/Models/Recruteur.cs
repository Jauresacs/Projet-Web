namespace JobBoard.Models
{
    public class Recruteur
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int EntrepriseId { get; set; }
        public string? Role { get; set; }

        // Propriétés spécifiques
        public DateTime DateInscription { get; set; } = DateTime.Now;

        // Navigation properties
        public User? User { get; set; }
        public Entreprise? Entreprise { get; set; }
        public ICollection<Job>? Jobs { get; set; }

    }
}
