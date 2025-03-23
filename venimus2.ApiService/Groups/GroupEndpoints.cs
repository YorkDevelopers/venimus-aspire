using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using venimus2.ApiService.Database;

namespace venimus2.ApiService.Groups;


public static class GroupEndpoints
{
    public static IEndpointRouteBuilder RegisterGroupEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/api/groups", CreateGroup)
            .WithName(nameof(CreateGroup))
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "Allows you to create a new group"
            });
        
        endpoints.MapGet("/api/groups", ListGroups)
            .WithName(nameof(ListGroups))
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "Returns a list of all active groups in this community"
            });
        
        return endpoints;
    }

    public static async Task<Ok<List<ListGroupsApiModel>>> ListGroups([FromServices] VenimusDbContext context)
    {
        var result = await context.Groups.Where(grp => grp.IsActive).Select(grp => new ListGroupsApiModel
        {
            Name = grp.Name,
            Description = grp.Description,
            IsActive = grp.IsActive,
            Slug = grp.Slug,
            StrapLine = grp.StrapLine,
            LogoInBase64 = grp.LogoInBase64,
            SlackChannelName = grp.SlackChannelName
        }).ToListAsync();

        return TypedResults.Ok(result);
    }
    
    public static async Task<Results<NoContent, ValidationProblem>> CreateGroup(
        [FromServices] CreateGroupModelValidator validator,
        [FromBody] CreateGroupApiModel apiModel)
    {
        var validationResult = await validator.ValidateAsync(apiModel);
        if (!validationResult.IsValid) 
            return TypedResults.ValidationProblem(validationResult.ToDictionary());
        
        
        await Task.Delay(1);
        return TypedResults.NoContent();
    }
    
}