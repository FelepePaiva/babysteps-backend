using BabySteps.API.DTOs;
using BabySteps.API.Services.Schedules;
using Microsoft.AspNetCore.Mvc;

namespace BabySteps.API.Controllers;

[ApiController]
[Route("schedules")]
public class SchedulesController : ControllerBase
{
    private readonly ScheduleService _scheduleService;

    public SchedulesController(ScheduleService scheduleService)
    {
        _scheduleService = scheduleService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateScheduleRequest request)
    {
        var result = await _scheduleService.ProcessSchedules(request);

        if (!result)
            return BadRequest(new { error = "Failed to process schedules" });

        return Ok(new { success = true, total = request.Schedules.Count });
    }
}