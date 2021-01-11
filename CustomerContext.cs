using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    public class CustomerContext : DbContext
    {
        public CustomerContext(DbContextOptions<CustomerContext> options)
            : base(options)
        {
        }

        public DbSet<CustomerProperties> CustomerItems { get; set; }
    }
}