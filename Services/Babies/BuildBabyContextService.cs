using BabySteps.API.Data;
using BabySteps.API.DTOs.Context;
using BabySteps.API.DTOs.Events;
using Microsoft.EntityFrameworkCore;

namespace BabySteps.API.Services.Babies
{
    public class BuildBabyContextService
    {
        private readonly AppDbContext _context;

        public BuildBabyContextService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<BabyContextResponse?> Execute(int babyId)
        {
            var baby = await _context.Babies
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == babyId);

            if (baby is null)
            {
                return null;
            }

            var assessment = await _context.Assessments
                .AsNoTracking()
                .Where(a => a.BabyId == babyId)
                .OrderByDescending(a => a.CreatedAt)
                .FirstOrDefaultAsync();

            var recentEvents = await _context.Events
                .AsNoTracking()
                .Where(e => e.BabyId == babyId)
                .OrderByDescending(e => e.OccurredAt)
                .Take(20)
                .ToListAsync();

            return new BabyContextResponse
{
    Baby = new BabyDto
    {
        Id = baby.Id,
        Name = baby.Name,
        UserName = baby.UserName,
        RelationshipToBaby = baby.RelationshipToBaby,
        BirthDate = baby.BirthDate
    },

    Assessment = assessment is null
        ? null
        : new AssessmentDto
        {
            Id = assessment.Id,
            MedicalContext = assessment.MedicalContext,
            FeedingType = assessment.FeedingType,
            FeedingFrequency = assessment.FeedingFrequency,
            WakeUpTime = assessment.WakeUpTime,
            SleepTime = assessment.SleepTime,
            NapFrequency = assessment.NapFrequency,
            NightWakeFrequency = assessment.NightWakeFrequency,
            ActivityFrequency = assessment.ActivityFrequency,
            ImprovementGoal = assessment.ImprovementGoal,
            CreatedAt = assessment.CreatedAt
        },

    RecentEvents = recentEvents.Select(e => new EventDto
    {
        Id = e.Id,
        Type = e.Type,
        OccurredAt = e.OccurredAt,
        Payload = e.Payload
    }).ToList()
};
        }
    }
}