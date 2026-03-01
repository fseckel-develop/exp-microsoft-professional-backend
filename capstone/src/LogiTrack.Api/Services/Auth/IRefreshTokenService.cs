namespace LogiTrack.Api.Services.Auth;

public interface IRefreshTokenService
{
    string GenerateRefreshToken();
}