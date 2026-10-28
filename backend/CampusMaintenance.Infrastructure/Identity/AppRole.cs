namespace CampusMaintenance.Infrastructure.Identity;

public enum AppRole
{
    Administrator = 1,
    Technician = 2,
    Reporter = 3
}

public static class RoleConstants
{
    public const string Administrator = "Administrator";
    public const string Technician = "Technician";
    public const string Reporter = "Reporter";
}