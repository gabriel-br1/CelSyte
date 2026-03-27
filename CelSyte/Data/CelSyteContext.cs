using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CelSyte.Models;

namespace CelSyte.Data
{
    public class CelSyteContext(DbContextOptions<CelSyteContext> options) : IdentityDbContext<User>(options)
    {
    }
}
