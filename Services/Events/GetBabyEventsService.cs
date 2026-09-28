using BabySteps.API.Data;
using BabySteps.API.DTOs.Events;
using Microsoft.EntityFrameworkCore;

namespace BabySteps.API.Services.Events
{
    public class GetBabyEventsService
    {
        private readonly AppDbContext _context;

        public GetBabyEventsService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<EventResponse>?> Execute(int babyId)
        {
            var babyExists = await _context.Babies
                .AnyAsync(b => b.Id == babyId);

            if (!babyExists)
            {
                return null;
            }

            var events = await _context.Events
                .Where(e => e.BabyId == babyId)
                .OrderByDescending(e => e.OccurredAt)
                .Select(e => new EventResponse
                {
                    Id = e.Id,
                    BabyId = e.BabyId,
                    Type = e.Type,
                    OccurredAt = e.OccurredAt,
                    Payload = e.Payload,
                    CreatedAt = e.CreatedAt
                })
                .ToListAsync();

            return events;
        }
    }
}