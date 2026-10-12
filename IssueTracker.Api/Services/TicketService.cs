using IssueTracker.Api.Validators;
using IssueTracker.Api.Dtos;
using IssueTracker.Api.Models;
using Microsoft.Data.SqlClient;
using Dapper;

namespace IssueTracker.Api.Services;

public class TicketService
{
    private readonly string _connectionString;

    public TicketService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public Tickets? GetTicket(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        return connection.QuerySingleOrDefault<Tickets>(
            "SELECT Id, Title, Description, Status, Priority FROM dbo.Tickets WHERE Id = @Id;",
            new { Id = id });
    }

    public int UpdateTicket(int id, string title, string description, string status, string priority)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        return connection.Execute(@"
            UPDATE dbo.Tickets
            SET Title = @Title, Description = @Description, Status = @Status, Priority = @Priority
            WHERE Id = @Id;",
            new { Id = id, Title = title, Description = description, Status = status, Priority = priority });
    }

    public Tickets CreateTicket(string title, string description, string priority)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        return connection.QuerySingle<Tickets>(@"
        INSERT INTO dbo.Tickets (Title, Description, Priority)
        OUTPUT INSERTED.Id, INSERTED.Title, INSERTED.Description,
               INSERTED.Status, INSERTED.Priority
        VALUES (@Title, @Description, @Priority);",
            new { Title = title, Description = description, Priority = priority });
    }
    }
