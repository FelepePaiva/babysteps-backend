using BabySteps.API.Data;
using BabySteps.API.DTOs.Events;
using BabySteps.API.Models;

namespace BabySteps.API.Services.Events
{
    public class CreateEventService
    {
        private readonly AppDbContext _context;

        public CreateEventService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<EventResponse?> Execute(CreateEventRequest request)
        {
            var babyExists = await _context.Babies.FindAsync(request.BabyId);

            if (babyExists is null)
            {
                return null;
            }

            var newEvent = new Event
            {
                BabyId = request.BabyId,
                Type = request.Type,
                OccurredAt = DateTime.UtcNow,
                Payload = request.Payload
            };

            await _context.Events.AddAsync(newEvent);

            await _context.SaveChangesAsync();

            return new EventResponse
            {
                Id = newEvent.Id,
                BabyId = newEvent.BabyId,
                Type = newEvent.Type,
                OccurredAt = newEvent.OccurredAt,
                Payload = newEvent.Payload,
                CreatedAt = newEvent.CreatedAt
            };
        }
    }
}