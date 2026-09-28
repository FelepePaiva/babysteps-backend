using BabySteps.API.Enums;

namespace BabySteps.API.DTOs.Events
{
    public class EventDto
    {
        public int Id { get; set; }

        public EventType Type { get; set; }

        public DateTime OccurredAt { get; set; }

        public string Payload { get; set; } = string.Empty;
    }
}