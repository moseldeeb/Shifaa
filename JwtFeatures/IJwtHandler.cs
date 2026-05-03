using System.Security.Claims;

namespace Shifaa.JwtFeatures
{
    public interface IJwtHandler
    {
        Task<string> GenerateAccessTokenAsync(ApplicationUser user);
        Task<string> GenerateAccessTokenAsync(ApplicationUser user, string selectedRole);
















        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
