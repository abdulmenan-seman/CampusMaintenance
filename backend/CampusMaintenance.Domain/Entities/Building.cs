namespace CampusMaintenance.Domain.Entities;
public class Building
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int CampusId { get; set; }
    public int MaxRoomNumber { get; set; }
    public bool IsActive { get; set; } = true;

    // Navigation Properties
    public virtual Campus Campus { get; set; } = null!;
    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}