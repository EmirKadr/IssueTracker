using IssueTracker.Api.Validators;
using IssueTracker.Api.Dtos;
using IssueTracker.Api.Models;
using Microsoft.Data.SqlClient;
using Dapper;

namespace IssueTracker.Api.Services;

public class TicketService
{
    //public int UpdateProperty(int id, UpdatePropertyRequest request)
    //{
    //    using var connection = new SqlConnection(_connectionString);
    //    connection.Open();
    //    return connection.Execute(@"
    //    UPDATE dbo.Properties
    //    SET Name = @Name, Address = @Address, Floors = @Floors, PortCode = @PortCode
    //    WHERE PropertyID = @Id;",
    //        new { Id = id, request.Name, request.Address, request.Floors, request.PortCode });
    //}

    private readonly string _connectionString;

    public TicketService(string connectionString)
    {
        _connectionString = connectionString;
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
