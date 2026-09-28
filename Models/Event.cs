using BabySteps.API.Enums;

namespace BabySteps.API.Models
{
    public class Event
    {
        public int Id { get; set; }

        public int BabyId { get; set; }

        public EventType Type { get; set; }

        public DateTime OccurredAt { get; set; }

        public string Payload { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Baby? Baby { get; set; }
    }
}