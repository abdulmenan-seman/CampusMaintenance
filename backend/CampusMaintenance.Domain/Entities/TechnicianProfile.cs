namespace CampusMaintenance.Domain.Entities;

public class TechnicianProfile
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string CategorySpecialty { get; set; } = string.Empty;
    

    // Navigation Properties
    public virtual User User { get; set; } = null!;
    public virtual ICollection<TicketAssignment> Assignments { get; set; } = new List<TicketAssignment>();
}