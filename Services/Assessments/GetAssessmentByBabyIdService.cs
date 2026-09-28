using BabySteps.API.Data;
using BabySteps.API.DTOs.Assessments;
using Microsoft.EntityFrameworkCore;

namespace BabySteps.API.Services.Assessments
{
    public class GetAssessmentByBabyIdService
    {
        private readonly AppDbContext _context;

        public GetAssessmentByBabyIdService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<AssessmentResponse?> Execute(int babyId)
        {
            var assessment = await _context.Assessments
                .FirstOrDefaultAsync(a => a.BabyId == babyId);

            if (assessment is null)
            {
                return null;
            }

            return new AssessmentResponse
            {
                Id = assessment.Id,
                BabyId = assessment.BabyId,
                ExpectedBirthDate = assessment.ExpectedBirthDate,
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
            };
        }
    }
}