namespace BabySteps.API.DTOs.Babies
{
    public class BabyResponse
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;

        public string RelationshipToBaby { get; set; } = string.Empty;

        public DateTime BirthDate { get; set; }

        public string FeedingType { get; set; } = string.Empty;

        public string FocusGoal { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
    }
}