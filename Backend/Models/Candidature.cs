namespace JobBoard.Models
{
    public class Candidature
    {
        public int Id { get; set; }
        public int ? CandidatId { get; set; }
        public int JobId { get; set; }
        public DateTime DateCandidature { get; set; } = DateTime.Now;
        public string? ApplicantName { get; set; }
        public string? ApplicantEmail { get; set; }
        public string? ApplicantNumber { get; set; }
        public string? ApplicantMessage { get; set; }

        public Candidat? Candidat { get; set; }
        public Job? Job { get; set; }
    }
}
