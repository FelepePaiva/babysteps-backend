using BabySteps.API.Data;
using BabySteps.API.Models;
using Microsoft.AspNetCore.Mvc;
using BabySteps.API.DTOs.Babies;
using BabySteps.API.Services.Babies;

namespace BabySteps.API.Controllers
{
    [ApiController]
    [Route("babies")]
    public class BabiesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly BuildBabyContextService _buildBabyContextService;

        public BabiesController(
            AppDbContext context,
            BuildBabyContextService buildBabyContextService
        )
        {
            _context = context;
            _buildBabyContextService = buildBabyContextService;
        }

        [HttpPost]
public async Task<IActionResult> CreateBaby(CreateBabyRequest request)
{
    var baby = new Baby
{
    Name = request.Name,
    UserName = request.UserName,
    RelationshipToBaby = request.RelationshipToBaby,
    BirthDate = DateTime.SpecifyKind(
        request.BirthDate,
        DateTimeKind.Utc
    ),
    FeedingType = request.FeedingType,
    FocusGoal = request.FocusGoal
};

    await _context.Babies.AddAsync(baby);

    await _context.SaveChangesAsync();

    var response = new BabyResponse
{
    Id = baby.Id,
    Name = baby.Name,
    UserName = baby.UserName,
    RelationshipToBaby = baby.RelationshipToBaby,
    BirthDate = baby.BirthDate,
    FeedingType = baby.FeedingType,
    FocusGoal = baby.FocusGoal,
    CreatedAt = baby.CreatedAt
};

    return CreatedAtAction(
        nameof(CreateBaby),
        new { id = baby.Id },
        response
    );
}

        [HttpGet("{id}/context")]
        public async Task<IActionResult> GetBabyContext(int id)
        {
            var result = await _buildBabyContextService.Execute(id);

            if (result is null)
            {
                return NotFound("Baby not found.");
            }

            return Ok(result);
        }
    }
}