namespace BabySteps.API.DTOs.Context
{
    public class AssessmentDto
    {
        public int Id { get; set; }

        public string MedicalContext { get; set; } = string.Empty;

        public string FeedingType { get; set; } = string.Empty;

        public string FeedingFrequency { get; set; } = string.Empty;

        public string WakeUpTime { get; set; } = string.Empty;

        public string SleepTime { get; set; } = string.Empty;

        public string NapFrequency { get; set; } = string.Empty;

        public string NightWakeFrequency { get; set; } = string.Empty;

        public string ActivityFrequency { get; set; } = string.Empty;

        public string ImprovementGoal { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
    }
}