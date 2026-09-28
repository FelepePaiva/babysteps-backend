namespace BabySteps.API.DTOs.Routines;

public class CreateRoutineRequest
{
    public int BabyId { get; set; }
    public DateTime Date { get; set; }
    public string? WakeUpTime { get; set; }
    public List<RoutineItemDto> PlanItems { get; set; } = new();
}