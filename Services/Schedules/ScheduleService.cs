using System.Net.Http.Json;
using BabySteps.API.DTOs;
using BabySteps.API.Data;
using BabySteps.API.DTOs.Schedules;
using Microsoft.Extensions.Configuration;

namespace BabySteps.API.Services.Schedules;

public class ScheduleService
{
    private readonly AppDbContext _context;
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public ScheduleService(AppDbContext context, HttpClient httpClient, IConfiguration configuration)
    {
        _context = context;
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<bool> ProcessSchedules(CreateScheduleRequest request)
    {
        if (request.Schedules == null || !request.Schedules.Any())
            return false;

        await CancelPreviousSchedules(request.BabyId, request.Schedules[0].ContactTo);

        foreach (var schedule in request.Schedules)
        {
            await CreateSchedule(schedule);
        }

        return true;
    }

    private async Task CancelPreviousSchedules(int babyId, string contactTo)
    {
        var botKey = _configuration["Blip:BotKey"];
        var userIdentity = contactTo.Split('@')[0];

        var listCommand = new
        {
            id = Guid.NewGuid().ToString(),
            to = "postmaster@scheduler.msging.net",
            method = "get",
            uri = "/schedules?skip=0&take=100"
        };

        // URL alterada para o gateway global estável
        var request = new HttpRequestMessage(HttpMethod.Post, "https://http.msging.net/commands")
        {
            Content = JsonContent.Create(listCommand)
        };
        request.Headers.Add("Authorization", $"Key {botKey}");

        var listResponse = await _httpClient.SendAsync(request);
        var listResult = await listResponse.Content.ReadFromJsonAsync<ScheduleListResponse>();

        if (listResult?.Resource?.Items == null) return;

        foreach (var item in listResult.Resource.Items)
        {
            var itemToIdentity = item.Message?.To?.Split('@')[0];

            if (item.Status == "scheduled" && 
                itemToIdentity == userIdentity && 
                !string.IsNullOrEmpty(item.Name))
            {
                var deleteCommand = new
                {
                    id = Guid.NewGuid().ToString(),
                    to = "postmaster@scheduler.msging.net",
                    method = "delete",
                    uri = $"/schedules/{item.Name}"
                };

                var deleteRequest = new HttpRequestMessage(HttpMethod.Post, "https://http.msging.net/commands")
                {
                    Content = JsonContent.Create(deleteCommand)
                };
                deleteRequest.Headers.Add("Authorization", $"Key {botKey}");

                await _httpClient.SendAsync(deleteRequest);
            }
        }
    }

    private async Task CreateSchedule(SleepScheduleDto schedule)
    {
        var botKey = _configuration["Blip:BotKey"];

        var command = new
        {
            id = Guid.NewGuid().ToString(),
            to = "postmaster@scheduler.msging.net",
            method = "set",
            uri = "/schedules",
            type = "application/vnd.iris.schedule+json",
            resource = new
            {
                name = schedule.Id, 
                message = new
                {
                    id = schedule.Id,
                    to = schedule.ContactTo,
                    type = "text/plain",
                    content = schedule.Message
                },
                when = schedule.When
            }
        };

        var request = new HttpRequestMessage(HttpMethod.Post, "https://http.msging.net/commands")
        {
            Content = JsonContent.Create(command)
        };
        request.Headers.Add("Authorization", $"Key {botKey}");

        await _httpClient.SendAsync(request);
    }
}