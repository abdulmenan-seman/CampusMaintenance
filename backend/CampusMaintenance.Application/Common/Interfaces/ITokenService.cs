using System.Security.Claims;

namespace CampusMaintenance.Application.Common.Interfaces;

public interface ITokenService
{
    string CreateAccessToken(
        string userId,
        string email,
        IReadOnlyCollection<string> roles);

    string CreateRefreshToken();

    string HashRefreshToken(string token);

    ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
}