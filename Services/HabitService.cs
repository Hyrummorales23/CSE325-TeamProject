using Microsoft.EntityFrameworkCore;
using BrodihyHabitTracker.Data;
using BrodihyHabitTracker.Models;

namespace BrodihyHabitTracker
{
    public class HabitService
    {
        private readonly ApplicationDbContext _context;

        public HabitService(ApplicationDbContext context)
        {
            _context = context;
        }

        // CRUD Operations

        public async Task<List<Habit>> GetUserHabitsAsync(string userId)
        {
            return await _context.Habits
                .Where(h => h.UserId == userId && h.IsActive)
                .Include(h => h.Completions)
                .ToListAsync();
        }

        public async Task<Habit?> GetHabitByIdAsync(int id, string userId)
        {
            return await _context.Habits
                .FirstOrDefaultAsync(h => h.Id == id && h.UserId == userId);
        }

        public async Task CreateHabitAsync(Habit habit)
        {
            habit.CreatedAt = DateTime.UtcNow;
            _context.Habits.Add(habit);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateHabitAsync(Habit habit)
        {
            _context.Habits.Update(habit);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteHabitAsync(int id, string userId)
        {
            var habit = await GetHabitByIdAsync(id, userId);
            if (habit != null)
            {
                // Soft delete - mark as inactive
                habit.IsActive = false;
                await _context.SaveChangesAsync();
            }
        }

        // Habit Completion Operations

        public async Task<bool> ToggleCompletionAsync(int habitId, DateTime date, string userId)
        {
            var existing = await _context.HabitCompletions
                .FirstOrDefaultAsync(hc => hc.HabitId == habitId
                    && hc.CompletionDate.Date == date.Date
                    && hc.UserId == userId);

            if (existing != null)
            {
                _context.HabitCompletions.Remove(existing);
                await _context.SaveChangesAsync();
                return false; // Completion removed
            }
            else
            {
                var completion = new HabitCompletion
                {
                    HabitId = habitId,
                    CompletionDate = date.Date,
                    UserId = userId
                };
                _context.HabitCompletions.Add(completion);
                await _context.SaveChangesAsync();
                return true; // Completion added
            }
        }

        public async Task<int> GetWeeklyProgressAsync(int habitId, string userId)
        {
            var startOfWeek = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
            var endOfWeek = startOfWeek.AddDays(7);

            return await _context.HabitCompletions
                .CountAsync(hc => hc.HabitId == habitId
                    && hc.UserId == userId
                    && hc.CompletionDate >= startOfWeek
                    && hc.CompletionDate < endOfWeek);
        }
    }
}