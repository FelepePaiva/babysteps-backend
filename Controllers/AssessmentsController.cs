using BabySteps.API.Data;
using BabySteps.API.Models;
using Microsoft.AspNetCore.Mvc;
using BabySteps.API.DTOs.Assessments;
using BabySteps.API.Services.Assessments;

namespace BabySteps.API.Controllers
{
    [ApiController]
    [Route("assessments")]
    public class AssessmentsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly GetAssessmentByBabyIdService _getAssessmentByBabyIdService;

        public AssessmentsController(
     AppDbContext context,
     GetAssessmentByBabyIdService getAssessmentByBabyIdService)
        {
            _context = context;
            _getAssessmentByBabyIdService = getAssessmentByBabyIdService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAssessment([FromBody] CreateAssessmentRequest request)
        {
            var babyExists = await _context.Babies.FindAsync(request.BabyId);

            if (babyExists is null)
            {
                return NotFound("Baby not found.");
            }

            var assessment = new Assessment
            {
                BabyId = request.BabyId,
                ExpectedBirthDate = request.ExpectedBirthDate.HasValue
         ? DateTime.SpecifyKind(
             request.ExpectedBirthDate.Value,
             DateTimeKind.Utc)
         : null,
                MedicalContext = request.MedicalContext,
                FeedingType = request.FeedingType,
                FeedingFrequency = request.FeedingFrequency,
                WakeUpTime = request.WakeUpTime,
                SleepTime = request.SleepTime,
                NapFrequency = request.NapFrequency,
                NightWakeFrequency = request.NightWakeFrequency,
                ActivityFrequency = request.ActivityFrequency,
                ImprovementGoal = request.ImprovementGoal
            };

            await _context.Assessments.AddAsync(assessment);

            await _context.SaveChangesAsync();

            var response = new AssessmentResponse
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

            return CreatedAtAction(
                nameof(CreateAssessment),
                new { id = assessment.Id },
                response
            );
        }
        [HttpGet("baby/{babyId}")]
        public async Task<IActionResult> GetByBabyId(int babyId)
        {
            var assessment = await _getAssessmentByBabyIdService.Execute(babyId);

            if (assessment is null)
            {
                return NotFound("Assessment not found.");
            }

            return Ok(assessment);
        }
    }
}