using BabySteps.API.Data;
using Microsoft.EntityFrameworkCore;
using BabySteps.API.Services.Events;
using BabySteps.API.Services.Babies;
using System.Text.Json.Serialization;
using BabySteps.API.Services.Assessments;
using BabySteps.API.Services.Schedules;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new JsonStringEnumConverter()
        );
        //Tratar o valor "null" que vem da blip
        options.JsonSerializerOptions.Converters.Add(
            new BabySteps.API.Utils.EmptyStringToNullableDateTimeConverter()
        );
    });

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);
builder.Services.AddScoped<CreateEventService>();
builder.Services.AddScoped<GetBabyEventsService>();
builder.Services.AddScoped<BuildBabyContextService>();
builder.Services.AddScoped<GetAssessmentByBabyIdService>();
builder.Services.AddHttpClient<ScheduleService>();
builder.Services.AddScoped<ScheduleService>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//app.UseHttpsRedirection();

app.MapControllers();

app.Run();