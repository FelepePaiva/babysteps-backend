using BabySteps.API.Enums;

namespace BabySteps.API.DTOs.Events
{
    public class EventResponse
    {
        public int Id { get; set; }

        public int BabyId { get; set; }

        public EventType Type  { get; set; }

        public DateTime OccurredAt { get; set; }

        public string Payload { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
    }
}