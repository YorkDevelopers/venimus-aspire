var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .WithLifetime(ContainerLifetime.Persistent);
var venimusdb = postgres.AddDatabase("venimusdb");

var apiService = builder.AddProject<Projects.venimus2_ApiService>("apiservice")
    .WithReference(venimusdb);

builder.AddProject<Projects.venimus2_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
