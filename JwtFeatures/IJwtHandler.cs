using System.Security.Claims;

namespace Shifaa.JwtFeatures
{
    public interface IJwtHandler
    {
        Task<string> GenerateAccessTokenAsync(ApplicationUser user);
















        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
