using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HabitController(AppDbContext context) : ControllerBase
{
    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveHabit(int id)
    {
        var habit = await context.Habits.FindAsync(id);

        if (habit == null)
        {
            return NotFound();
        }

        context.Habits.Remove(habit);
        await context.SaveChangesAsync();

        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Habit>> GetHabit(int id)
    {
        var habit = await context.Habits
            .Include(h => h.Completions)
            .FirstOrDefaultAsync(h => h.Id == id);

        if (habit == null)
        {
            return NotFound();
        }

        return habit;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Habit>>> GetHabits()
    {
        return await context.Habits
            .Include(h => h.Completions)
            .OrderByDescending(h => h.CreatedAt)
            .ToListAsync();
    }

    [HttpPost("{id}/complete")]
    public async Task<ActionResult<Habit>> CompleteHabit(int id)
    {
        var habit = await context.Habits
            .Include(h => h.Completions)
            .FirstOrDefaultAsync(h => h.Id == id);

        if (habit == null)
        {
            return NotFound();
        }

        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        var alreadyCompleted = habit.Completions.Any(c => c.CompletedDate == today);

        if (!alreadyCompleted)
        {
            var completion = new Completion { HabitId = id, CompletedDate = today };
            context.Completions.Add(completion);

            habit.CurrentStreak++;
            await context.SaveChangesAsync();
        }

        return Ok(habit);
    }

    [HttpPost]
    public async Task<ActionResult<Habit>> CreateHabit(Habit habit)
    {
        context.Habits.Add(habit);
        await context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetHabit), new { id = habit.Id }, habit);
    }
}
