namespace BabySteps.API.DTOs.Routines;

public class RoutineItemDto
{
    public string Time { get; set; } = string.Empty;     // Ex: "10:30"
    public string Type { get; set; } = string.Empty;     // Ex: "Nap", "Lunch", "Bottle", "NightSleep"
    public string Title { get; set; } = string.Empty;    // Ex: "Soneca da Manhã"
    public string Status { get; set; } = "pending";     // "pending", "completed", "skipped"
    public string? ActualTime { get; set; }             // Ex: "11:30" (quando realizado fora do horário)
}