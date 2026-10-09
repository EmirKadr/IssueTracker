using Dapper;
using IssueTracker.Api.Dtos;
using IssueTracker.Api.Models;
using Microsoft.Data.SqlClient;

namespace IssueTracker.Api.Endpoints;

public static class TicketEndpoints
{
    public static WebApplication MapTicketEndpoints(this WebApplication app)
    {
        app.MapGet("/api/tickets", () =>
        {
            using var connection = new SqlConnection(app.Configuration.GetConnectionString("Prod")
                ?? throw new InvalidOperationException("Anslutningssträngen 'Prod' saknas."));
            connection.Open();

            var tickets = connection.Query<Tickets>(
                "SELECT Id, Title, Description, Status, Priority FROM dbo.Tickets ORDER BY Id;");

            var ticketDtos = tickets.Select(ticket => new TicketDto
            {
                Id = ticket.Id,
                Title = ticket.Title,
                Description = ticket.Description,
                Status = ticket.Status,
                Priority = ticket.Priority
            }).ToList();

            return Results.Ok(ticketDtos);
        });

        return app;
    }
}
