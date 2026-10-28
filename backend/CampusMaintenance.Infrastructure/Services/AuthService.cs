using Microsoft.AspNetCore.Identity;
using CampusMaintenance.Application.Common.Interfaces;
using CampusMaintenance.Infrastructure.Identity;
using CampusMaintenance.Infrastructure.Persistence;
using CampusMaintenance.Application.DTOs.Auth;
using System.Security.Claims;

namespace CampusMaintenance.Infrastructure.Services;

public interface IAuthService
{
    Task<(bool Succeeded, string Message, RegisterResponseDto? Data)> RegisterReporterAsync(
        string fullName, string email, string password, string phoneNumber, string? studentId, string? staffId, int? campusId);
    Task<(bool Succeeded, string Message, AuthResponseDto? Data)> LoginAsync(string email, string password);
    Task<(bool Succeeded, string Message, AuthResponseDto? Data)> RefreshTokenAsync(string accessToken, string refreshToken);
}

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ITokenService _tokenService;
    private readonly ApplicationDbContext _context; // Direct DB context access (No Repository Pattern)

    public AuthService(
        UserManager<ApplicationUser> userManager,
        ITokenService tokenService,
        ApplicationDbContext context)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _context = context;
    }

    public async Task<(bool Succeeded, string Message, RegisterResponseDto? Data)> RegisterReporterAsync(
        string fullName, string email, string password, string phoneNumber, string? studentId, string? staffId, int? campusId)
    {
        var existingUser = await _userManager.FindByEmailAsync(email);
        if (existingUser != null)
        {
            return (false, "Email is already registered.", null);
        }

        var user = new ApplicationUser
        {
            UserName = email,
            Email = email,
            FullName = fullName,
            PhoneNumber = phoneNumber,
            StudentId = studentId,
            StaffId = staffId,
            CampusId = campusId,
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };

        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return (false, $"Registration failed: {errors}", null);
        }

        // Assign default Reporter role
        await _userManager.AddToRoleAsync(user, RoleConstants.Reporter);

        return (true, "Reporter account registered successfully.", new RegisterResponseDto
        {
            Id = user.Id,
            Email = user.Email ?? string.Empty,
            FullName = user.FullName
        });
    }

    public async Task<(bool Succeeded, string Message, AuthResponseDto? Data)> LoginAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null || !user.IsActive)
        {
            return (false, "Invalid credentials or inactive account.", null);
        }

        var passwordIsValid = await _userManager.CheckPasswordAsync(user, password);
        if (!passwordIsValid)
        {
            return (false, "Invalid email or password.", null);
        }

        var roles = await _userManager.GetRolesAsync(user);
        var accessToken = _tokenService.CreateAccessToken(
            user.Id.ToString(), user.Email ?? user.UserName ?? string.Empty, roles.ToArray());
        var refreshToken = _tokenService.CreateRefreshToken();

        // Save refresh token to user record
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
        await _userManager.UpdateAsync(user);

        return (true, "Login successful.", new AuthResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        });
    }

    public async Task<(bool Succeeded, string Message, AuthResponseDto? Data)> RefreshTokenAsync(string accessToken, string refreshToken)
    {
        var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
        if (principal == null)
        {
            return (false, "Invalid access token.", null);
        }

        var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim == null || !int.TryParse(userIdClaim, out var userId))
        {
            return (false, "Invalid token claims.", null);
        }

        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
        {
            return (false, "Invalid or expired refresh token.", null);
        }

        var roles = await _userManager.GetRolesAsync(user);
        var newAccessToken = _tokenService.CreateAccessToken(
            user.Id.ToString(), user.Email ?? user.UserName ?? string.Empty, roles.ToArray());
        var newRefreshToken = _tokenService.CreateRefreshToken();

        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
        await _userManager.UpdateAsync(user);

        return (true, "Token refreshed successfully.", new AuthResponseDto
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken
        });
    }
}