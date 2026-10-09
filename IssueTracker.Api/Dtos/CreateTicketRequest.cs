namespace IssueTracker.Api.Dtos;
    public record CreateTicketRequest
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Priority { get; set; }
    }

