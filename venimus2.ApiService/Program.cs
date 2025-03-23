using Microsoft.EntityFrameworkCore;
using venimus2.ApiService.Database;
using venimus2.ApiService.Groups;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.AddNpgsqlDbContext<VenimusDbContext>(connectionName: "venimusdb");

var app = builder.Build();
app.RegisterGroupEndpoints();


// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<VenimusDbContext>();
    context.Database.EnsureCreated();

    if (!await context.Groups.AnyAsync())
    {
        var grp = new Group
        {
            Description = "York Code Dodo",
            Name = "YorkCodeDojo",
            IsActive = true,
            StrapLine = "Learn by practice",
            Slug = "YCD",
            SlackChannelName = "york-code-dojo"
        };
        context.Groups.Add(grp);
        await context.SaveChangesAsync();

    }
}

app.MapDefaultEndpoints();
app.Run();

