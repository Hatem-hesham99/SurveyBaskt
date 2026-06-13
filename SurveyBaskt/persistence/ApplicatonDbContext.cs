using System.Reflection;

namespace SurveyBaskt.persistence
{
    public class ApplicatonDbContext(DbContextOptions<ApplicatonDbContext> options) : DbContext(options)
    {
       public DbSet<Poll> Polls { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
