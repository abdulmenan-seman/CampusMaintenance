using Microsoft.AspNetCore.Identity;

namespace CampusMaintenance.Infrastructure.Identity;

public class ApplicationUser : IdentityUser<int>
{
    public string FullName { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Additional reporter fields
    public string? StudentId { get; set; }
    public string? StaffId { get; set; }
    public int? CampusId { get; set; }

    // Refresh Token fields for JWT lifecycle
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
}