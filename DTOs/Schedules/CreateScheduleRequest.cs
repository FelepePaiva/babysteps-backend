using System.Text.Json.Serialization;

namespace BabySteps.API.DTOs;

public class CreateScheduleRequest
{
    public int BabyId { get; set; }
    public List<SleepScheduleDto> Schedules { get; set; } = new();
}

public class SleepScheduleDto
{
    public string Id { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Time { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string When { get; set; } = string.Empty;
    public string ContactTo { get; set; } = string.Empty;
}