using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrodihyHabitTracker.Models
{
    public class HabitCompletion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int HabitId { get; set; }

        [ForeignKey("HabitId")]
        public Habit? Habit { get; set; }

        [Required]
        public DateTime CompletionDate { get; set; }

        public string UserId { get; set; } = string.Empty;

        [ForeignKey("UserId")]
        public User? User { get; set; }
    }
}