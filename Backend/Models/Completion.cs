namespace Backend.Models;

public class Completion
{
    public int Id { get; set; }

    public int HabitId { get; set; }

    public DateOnly CompletedDate { get; set; }
}
