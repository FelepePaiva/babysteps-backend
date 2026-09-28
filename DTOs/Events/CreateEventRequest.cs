using BabySteps.API.Enums;

namespace BabySteps.API.DTOs.Events
{
    public class CreateEventRequest
    {
        public int BabyId { get; set; }

        public EventType Type { get; set; }


        public string Payload { get; set; } = string.Empty;
    }
}