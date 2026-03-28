using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrodihyHabitTracker.Models
{
    public class Habit
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        public HabitType Type { get; set; }

        [Required]
        [Range(1, 7)]
        public int WeeklyFrequency { get; set; } // 1-7 times per week

        [Required]
        public string UserId { get; set; } = string.Empty;

        [ForeignKey("UserId")]
        public User? User { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool IsActive { get; set; } = true;

        // Navigation property for completions
        public ICollection<HabitCompletion> Completions { get; set; } = new List<HabitCompletion>();
    }

    public enum HabitType
    {
        Physical,
        Mental,
        SocialEmotional,
        Spiritual
    }
}