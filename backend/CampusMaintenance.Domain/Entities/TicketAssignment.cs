namespace CampusMaintenance.Domain.Entities;
public class TicketAssignment
{
    public int Id { get; set; }
    public int TicketId { get; set; }
    public int TechnicianId { get; set; }
    public int AssignedById { get; set; }
    public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
    public DateTime? EndedAt { get; set; }
    public string? Reason { get; set; }

    // Navigation Properties
    public virtual Ticket Ticket { get; set; } = null!;
    public virtual TechnicianProfile Technician { get; set; } = null!;
    public virtual User AssignedBy { get; set; } = null!;
}