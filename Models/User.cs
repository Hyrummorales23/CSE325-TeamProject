using Microsoft.AspNetCore.Identity;

namespace BrodihyHabitTracker.Models
{
    // This extends the built-in IdentityUser
    public class User : IdentityUser
    {
        // Add any additional user properties if needed
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property for habits
        public ICollection<Habit> Habits { get; set; } = new List<Habit>();
    }
}