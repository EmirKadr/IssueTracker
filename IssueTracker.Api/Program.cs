using IssueTracker.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.UseHttpsRedirection();

app.MapTicketEndpoints();

app.Run();
