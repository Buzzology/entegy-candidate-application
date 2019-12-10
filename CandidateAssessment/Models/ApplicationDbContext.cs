using Microsoft.EntityFrameworkCore;

namespace CandidateAssessment.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Cheque> Cheques { get; set; }
    }
}
