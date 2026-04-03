using System.ComponentModel.DataAnnotations.Schema;

namespace LandingPage.Models
{
    public class Subscriber
    {
        public int Id { get; set; }

        public required string Email { get; set; }
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
