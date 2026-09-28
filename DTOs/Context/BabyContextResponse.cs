using BabySteps.API.DTOs.Events;

namespace BabySteps.API.DTOs.Context
{
    public class BabyContextResponse
    {
        public BabyDto Baby { get; set; } = null!;

        public AssessmentDto? Assessment { get; set; }

        public List<EventDto> RecentEvents { get; set; } = new();
    }
}