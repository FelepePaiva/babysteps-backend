using System.Text.Json.Serialization;

namespace BabySteps.API.DTOs.Schedules;

public class ScheduleListResponse
{
    [JsonPropertyName("method")]
    public string Method { get; set; } = string.Empty;

    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    [JsonPropertyName("resource")]
    public ScheduleListResource? Resource { get; set; }
}

public class ScheduleListResource
{
    [JsonPropertyName("total")]
    public int Total { get; set; }

    [JsonPropertyName("items")]
    public List<ScheduleItem> Items { get; set; } = new();
}

public class ScheduleItem
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("when")]
    public string? When { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("message")]
    public ScheduleMessage? Message { get; set; }
}

public class ScheduleMessage
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("to")]
    public string To { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("content")]
    public object? Content { get; set; } // Alterado para object pois o conteúdo do Blip pode vir encapsulado ou variar de tipo
}