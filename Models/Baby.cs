namespace BabySteps.API.Models
{
    public class Baby
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;

        public string RelationshipToBaby { get; set; } = string.Empty;

        public DateTime BirthDate { get; set; }

        public string FeedingType { get; set; } = string.Empty;

        public string FocusGoal { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<Event> Events { get; set; } = [];
        public ICollection<Assessment> Assessments { get; set; } = [];
    }
}