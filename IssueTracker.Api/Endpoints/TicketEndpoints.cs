using Dapper;
using IssueTracker.Api.Dtos;
using IssueTracker.Api.Models;
using IssueTracker.Api.Services;
using IssueTracker.Api.Validators;
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

        app.MapPost("/api/tickets", (CreateTicketRequest request) =>
        {
            var errors = TicketValidator.ValidateTicket(request);

            if (errors.Count > 0)
            {
                return Results.ValidationProblem(errors);
            }

            var service = new TicketService(
                app.Configuration.GetConnectionString("Prod")
                ?? throw new InvalidOperationException("Anslutningssträngen 'Prod' saknas."));

            var ticket = service.CreateTicket(
                request.Title!,
                request.Description!,
                request.Priority!);

            var ticketDto = new TicketDto
            {
                Id = ticket.Id,
                Title = ticket.Title,
                Description = ticket.Description,
                Status = ticket.Status,
                Priority = ticket.Priority
            };

            return Results.Created($"/api/tickets/{ticket.Id}", ticketDto);
        });

        app.MapPut("/api/tickets/{id:int}", (int id, UpdateTicketRequest request) =>
        {
            var errors = TicketValidator.ValidateTicket(request);
            if (errors.Count > 0)
            {
                return Results.ValidationProblem(errors);
            }

            var service = new TicketService(
                app.Configuration.GetConnectionString("Prod")
                ?? throw new InvalidOperationException("Anslutningssträngen 'Prod' saknas."));

            var rows = service.UpdateTicket(id, request.Title!, request.Description!, request.Status!, request.Priority!);

            return rows == 0 ? Results.NotFound() : Results.NoContent();
        });

        app.MapDelete("/api/tickets/{id:int}", (int id) =>
        {
            using var connection = new SqlConnection(app.Configuration.GetConnectionString("Prod")
                ?? throw new InvalidOperationException("Anslutningssträngen 'Prod' saknas."));
            connection.Open();

            var rows = connection.Execute(
                "DELETE FROM dbo.Tickets WHERE Id = @Id;",
                new { Id = id });

            return rows == 0 ? Results.NotFound() : Results.NoContent();
        });

        return app;
    }

}


