using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BabySteps.API.Data;
using BabySteps.API.Models;
using BabySteps.API.DTOs.Routines;

namespace BabySteps.API.Controllers;

[ApiController]
[Route("routines")]
public class RoutinesController : ControllerBase
{
    private readonly AppDbContext _context;
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public RoutinesController(AppDbContext context)
    {
        _context = context;
    }
    [HttpGet("baby/{babyId}")]
    public async Task<ActionResult<RoutineResponse>> GetTodayRoutine(int babyId)
    {
        var brazilTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
        var todayInBrazil = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, brazilTimeZone).Date;

        var routine = await _context.Routines
            .FirstOrDefaultAsync(r => r.BabyId == babyId && r.Date == todayInBrazil);

        if (routine == null)
        {
            return NotFound(new { message = "Nenhuma rotina encontrada para o dia de hoje." });
        }

        var items = string.IsNullOrEmpty(routine.Plan)
            ? new List<RoutineItemDto>()
            : JsonSerializer.Deserialize<List<RoutineItemDto>>(routine.Plan, JsonOptions) ?? new();

        return Ok(new RoutineResponse
        {
            Id = routine.Id,
            BabyId = routine.BabyId,
            Date = routine.Date,
            WakeUpTime = routine.WakeUpTime,
            PlanItems = items,
            CreatedAt = routine.CreatedAt,
            UpdatedAt = routine.UpdatedAt
        });
    }
    [HttpPost]
    public async Task<ActionResult<RoutineResponse>> CreateRoutine([FromBody] CreateRoutineRequest request)
    {
        var targetDate = request.Date.Date;

        var exists = await _context.Routines
            .AnyAsync(r => r.BabyId == request.BabyId && r.Date == targetDate);

        if (exists)
        {
            return BadRequest(new { message = "Já existe uma rotina criada para este bebê nesta data." });
        }

        var jsonPlan = JsonSerializer.Serialize(request.PlanItems, JsonOptions);

        var routine = new Routine
        {
            BabyId = request.BabyId,
            Date = targetDate,
            WakeUpTime = request.WakeUpTime,
            Plan = jsonPlan,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Routines.Add(routine);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTodayRoutine), new { babyId = routine.BabyId, date = routine.Date }, new RoutineResponse
        {
            Id = routine.Id,
            BabyId = routine.BabyId,
            Date = routine.Date,
            WakeUpTime = routine.WakeUpTime,
            PlanItems = request.PlanItems,
            CreatedAt = routine.CreatedAt,
            UpdatedAt = routine.UpdatedAt
        });
    }
    [HttpPut("{id}")]
    public async Task<ActionResult<RoutineResponse>> UpdateRoutine(int id, [FromBody] UpdateRoutineRequest request)
    {
        var routine = await _context.Routines.FindAsync(id);

        if (routine == null)
        {
            return NotFound(new { message = "Rotina não encontrada." });
        }

        if (request.WakeUpTime != null)
            routine.WakeUpTime = request.WakeUpTime;

        routine.Plan = JsonSerializer.Serialize(request.PlanItems, JsonOptions);
        routine.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return Ok(new RoutineResponse
        {
            Id = routine.Id,
            BabyId = routine.BabyId,
            Date = routine.Date,
            WakeUpTime = routine.WakeUpTime,
            PlanItems = request.PlanItems,
            CreatedAt = routine.CreatedAt,
            UpdatedAt = routine.UpdatedAt
        });
    }
}