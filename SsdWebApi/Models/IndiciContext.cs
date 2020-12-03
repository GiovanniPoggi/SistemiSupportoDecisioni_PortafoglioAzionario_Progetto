using Microsoft.EntityFrameworkCore;

//Database model
namespace SsdWebApi.Models
{
    public class IndiciContext : DbContext
    {
        public IndiciContext(DbContextOptions<IndiciContext> options)
        : base(options)
        {
        }
        public DbSet<Indice> indici { get; set; }
    }
}