namespace CampusMaintenance.Domain.Entities;

public class Campus
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;

    // Navigation Properties
    public virtual ICollection<Building> Buildings { get; set; } = new List<Building>();
    public virtual ICollection<User> Users { get; set; } = new List<User>();
    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}