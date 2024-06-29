using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CSV_API.Models
{
    public class CSV_Context: IdentityDbContext<AppUser>
    {
        public CSV_Context(DbContextOptions<CSV_Context> options)
        : base(options)
        {
            
        }
    }
}
