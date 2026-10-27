namespace CampusMaintenance.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Additional reporter fields

    public string? StudentId { get; set; }
    public string? StaffId { get; set; }
    public int? CampusId { get; set; }

    // Navigation Properties
    public virtual Campus? Campus { get; set; }
    public virtual TechnicianProfile? TechnicianProfile { get; set; }
    public virtual ICollection<Ticket> ReportedTickets { get; set; } = new List<Ticket>();
    public virtual ICollection<TicketAssignment> AssignedByAssignments { get; set; } = new List<TicketAssignment>();
    public virtual ICollection<TicketHistory> PerformedHistories { get; set; } = new List<TicketHistory>();
}