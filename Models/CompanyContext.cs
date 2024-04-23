using Microsoft.EntityFrameworkCore;
namespace LR12.Models
{
    public class CompanyContext : DbContext
    {
        public DbSet<Company>Companies { get; set; }

        public CompanyContext(DbContextOptions<CompanyContext> options) : base(options)
        {

        }
    }
}
