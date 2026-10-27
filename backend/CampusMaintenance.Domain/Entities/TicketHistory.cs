namespace CampusMaintenance.Domain.Entities;
public class TicketHistory
{
    public int Id { get; set; }
    public int TicketId { get; set; }
    public int ActorId { get; set; }
    public string? OldStatus { get; set; }
    public string? NewStatus { get; set; }
    public string Details { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation Properties
    public virtual Ticket Ticket { get; set; } = null!;
    public virtual User Actor { get; set; } = null!;
}