namespace BabySteps.API.DTOs.Context
{
    public class BabyDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;

        public string RelationshipToBaby { get; set; } = string.Empty;

        public DateTime BirthDate { get; set; }
    }
}