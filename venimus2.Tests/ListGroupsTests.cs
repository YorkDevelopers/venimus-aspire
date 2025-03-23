using System.Net.Http.Json;
using venimus2.ApiService.Groups;

public class ListGroupsTests
{
    [Fact]
    public async Task CanGetListOfActiveGroups()
    {
        var appHost =
            await DistributedApplicationTestingBuilder.CreateAsync<Projects.venimus2_AppHost>(TestContext.Current.CancellationToken);
        appHost.Services.ConfigureHttpClientDefaults(clientBuilder =>
        {
            clientBuilder.AddStandardResilienceHandler();
        });

        await using var app = await appHost.BuildAsync(TestContext.Current.CancellationToken);
        await app.StartAsync(TestContext.Current.CancellationToken);

        var resourceNotificationService =
            app.Services.GetRequiredService<ResourceNotificationService>();
        await resourceNotificationService
            .WaitForResourceAsync("apiservice", KnownResourceStates.Running, TestContext.Current.CancellationToken)
            .WaitAsync(TimeSpan.FromSeconds(30), TestContext.Current.CancellationToken);

        var httpClient = app.CreateHttpClient("apiservice");
        var response = await httpClient.GetAsync("/api/groups", TestContext.Current.CancellationToken);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var groups = await response.Content.ReadFromJsonAsync<IEnumerable<ListGroupsApiModel>>(TestContext.Current.CancellationToken);
        Assert.NotNull(groups);
        Assert.Equal(1, groups.Count());
    }
}
