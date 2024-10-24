using JobBoard.Models;

namespace JobBoard{
    public class Admin{
        public int Id {get; set; }
        public int UserId { get; set; }
        public DateTime DateInscription {get; set; } = DateTime.Now;
        public User? User {get; set; }
    }
}