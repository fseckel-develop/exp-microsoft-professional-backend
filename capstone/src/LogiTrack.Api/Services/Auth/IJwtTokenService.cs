using LogiTrack.Api.Models;

namespace LogiTrack.Api.Services.Auth;

public interface IJwtTokenService
{
    Task<string> GenerateTokenAsync(ApplicationUser user);
}