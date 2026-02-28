using System.Security.Claims;

namespace CelSyte.Models
{
    public class User
    {

        public int Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public ClaimsPrincipal ToClaimsPrincipal() => new(new ClaimsIdentity(new Claim[]
        {
            new (ClaimTypes.Name, Email),
            new (ClaimTypes.Hash, Password)
        }, "CelsyteUser"));

        public static User FromClaimsPrincipal(ClaimsPrincipal principal) => new()
        {
            Email = principal.FindFirstValue(ClaimTypes.Name),
            Password = principal.FindFirstValue(ClaimTypes.Hash)
        };

    }
}
