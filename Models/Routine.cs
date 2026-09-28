namespace BabySteps.API.Models;

public class Routine
{
    public int Id { get; set; }
    public int BabyId { get; set; }
    public DateTime Date { get; set; }
    public string? WakeUpTime { get; set; } 
    
    public string Plan { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public Baby Baby { get; set; } = null!;
}