namespace BabySteps.API.DTOs.Routines;

public class UpdateRoutineRequest
{
    public string? WakeUpTime { get; set; }
    public List<RoutineItemDto> PlanItems { get; set; } = new();
}