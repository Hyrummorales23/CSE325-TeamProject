using Microsoft.EntityFrameworkCore;
using BrodihyHabitTracker.Data;
using BrodihyHabitTracker.Models;

namespace BrodihyHabitTracker
{
    public class HabitService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _factory;

        public HabitService(IDbContextFactory<ApplicationDbContext> factory)
        {
            _factory = factory;
        }

        public async Task<List<Habit>> GetUserHabitsAsync(string userId)
        {
            using var ctx = await _factory.CreateDbContextAsync();
            return await ctx.Habits
                .Where(h => h.UserId == userId && h.IsActive)
                .Include(h => h.Completions)
                .ToListAsync();
        }

        public async Task<Habit?> GetHabitByIdAsync(int id, string userId)
        {
            using var ctx = await _factory.CreateDbContextAsync();
            return await ctx.Habits
                .FirstOrDefaultAsync(h => h.Id == id && h.UserId == userId);
        }

        public async Task CreateHabitAsync(Habit habit)
        {
            using var ctx = await _factory.CreateDbContextAsync();
            habit.CreatedAt = DateTime.UtcNow;
            ctx.Habits.Add(habit);
            await ctx.SaveChangesAsync();
        }

        public async Task UpdateHabitAsync(Habit habit)
        {
            using var ctx = await _factory.CreateDbContextAsync();
            ctx.Habits.Update(habit);
            await ctx.SaveChangesAsync();
        }

        public async Task DeleteHabitAsync(int id, string userId)
        {
            using var ctx = await _factory.CreateDbContextAsync();
            var habit = await ctx.Habits.FirstOrDefaultAsync(h => h.Id == id && h.UserId == userId);
            if (habit != null)
            {
                habit.IsActive = false;
                await ctx.SaveChangesAsync();
            }
        }

        public async Task<bool> ToggleCompletionAsync(int habitId, DateTime date, string userId)
        {
            using var ctx = await _factory.CreateDbContextAsync();
            var existing = await ctx.HabitCompletions
                .FirstOrDefaultAsync(hc => hc.HabitId == habitId
                    && hc.CompletionDate.Date == date.Date
                    && hc.UserId == userId);

            if (existing != null)
            {
                ctx.HabitCompletions.Remove(existing);
                await ctx.SaveChangesAsync();
                return false;
            }

            ctx.HabitCompletions.Add(new HabitCompletion
            {
                HabitId = habitId,
                CompletionDate = date.Date,
                UserId = userId
            });
            await ctx.SaveChangesAsync();
            return true;
        }

        public async Task<int> GetWeeklyProgressAsync(int habitId, string userId)
        {
            using var ctx = await _factory.CreateDbContextAsync();
            var start = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
            return await ctx.HabitCompletions
                .CountAsync(hc => hc.HabitId == habitId
                    && hc.UserId == userId
                    && hc.CompletionDate >= start
                    && hc.CompletionDate < start.AddDays(7));
        }
    }
}
