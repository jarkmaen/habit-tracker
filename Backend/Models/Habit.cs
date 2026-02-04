using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

public class Habit
{
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string Name { get; set; } = string.Empty;

    public int CurrentStreak { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public List<Completion> Completions { get; set; } = [];
}
