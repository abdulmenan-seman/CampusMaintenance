namespace CampusMaintenance.Application.DTOs.Auth;

public class RegisterRequestDto
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    
    // Conditional / Optional fields based on whether user is student or staff
    public string? StudentId { get; set; }
    public string? StaffId { get; set; }
    public int? CampusId { get; set; }
}