namespace BabySteps.API.DTOs.Routines;

public class RoutineResponse
{
    public int Id { get; set; }
    public int BabyId { get; set; }
    public DateTime Date { get; set; }
    public string? WakeUpTime { get; set; }
    public bool HasWokenUpToday => !string.IsNullOrEmpty(WakeUpTime);
    public List<RoutineItemDto> PlanItems { get; set; } = new();
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}