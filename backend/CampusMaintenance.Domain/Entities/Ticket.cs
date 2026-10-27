namespace CampusMaintenance.Domain.Entities;
using CampusMaintenance.Domain.Enums;
public class Ticket
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public int CampusId { get; set; }
    public int BuildingId { get; set; }
    public string RoomNumber { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TicketStatus Status { get; set; } = TicketStatus.Submitted;
    public int ReporterId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    // Navigation Properties
    public virtual Campus Campus { get; set; } = null!;
    public virtual Building Building { get; set; } = null!;
    public virtual User Reporter { get; set; } = null!;
    public virtual ICollection<TicketAssignment> Assignments { get; set; } = new List<TicketAssignment>();
    public virtual ICollection<TicketHistory> Histories { get; set; } = new List<TicketHistory>();
}