namespace BabySteps.API.DTOs.Assessments
{
    public class AssessmentResponse
    {
        public int Id { get; set; }

        public int BabyId { get; set; }

        public DateTime? ExpectedBirthDate { get; set; }

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