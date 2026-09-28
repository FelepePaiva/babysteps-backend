using System.Text.Json;
using BabySteps.API.DTOs.Events;
using BabySteps.API.Services.Events;
using Microsoft.AspNetCore.Mvc;

namespace BabySteps.API.Controllers
{
    [ApiController]
    [Route("events")]
    public class EventsController : ControllerBase
    {
        private readonly CreateEventService _createEventService;
        private readonly GetBabyEventsService _getBabyEventsService;

        public EventsController(
            CreateEventService createEventService,
            GetBabyEventsService getBabyEventsService
        )
        {
            _createEventService = createEventService;
            _getBabyEventsService = getBabyEventsService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent(CreateEventRequest request)
        {
            var result = await _createEventService.Execute(request);

            if (result is null)
            {
                return NotFound("Baby not found.");
            }

            return CreatedAtAction(
                nameof(CreateEvent),
                new { id = result.Id },
                result
            );
        }

        [HttpGet("baby/{babyId}")]
        public async Task<IActionResult> GetBabyEvents(int babyId)
        {
            var result = await _getBabyEventsService.Execute(babyId);

            if (result is null)
            {
                return NotFound("Baby not found.");
            }

            return Ok(result);
        }
        [HttpPost("debug")]
public IActionResult Debug([FromBody] JsonElement body)
{
    return Ok(body);
}
    }
}