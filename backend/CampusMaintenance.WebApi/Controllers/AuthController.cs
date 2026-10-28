using Microsoft.AspNetCore.Mvc;
using CampusMaintenance.Infrastructure.Services;
using CampusMaintenance.Application.DTOs.Auth;

namespace CampusMaintenance.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto dto)
    {
        var (succeeded, message, data) = await _authService.RegisterReporterAsync(
            dto.FullName, dto.Email, dto.Password, dto.PhoneNumber, dto.StudentId, dto.StaffId, dto.CampusId);

        if (!succeeded)
        {
            return BadRequest(new { message, data });
        }

        return Ok(new { message, data });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
    {
        var (succeeded, message, data) = await _authService.LoginAsync(dto.Email, dto.Password);

        if (!succeeded)
        {
            return Unauthorized(new { message });
        }

        return Ok(new { message, data });
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDto dto)
    {
        var (succeeded, message, data) = await _authService.RefreshTokenAsync(dto.AccessToken, dto.RefreshToken);

        if (!succeeded)
        {
            return Unauthorized(new { message });
        }

        return Ok(new { message, data });
    }
}