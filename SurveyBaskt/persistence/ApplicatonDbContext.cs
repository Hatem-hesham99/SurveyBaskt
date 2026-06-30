using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Reflection;
using System.Security.Claims;

namespace SurveyBaskt.persistence
{
    public class ApplicatonDbContext(DbContextOptions<ApplicatonDbContext> options , IHttpContextAccessor _httpContextAccessor ) : IdentityDbContext<ApplicationUser>(options)
    {

        public DbSet<Poll> Polls { get; set; }
        public DbSet<Quesion> Quesions { get; set; }    
        public DbSet<Answer> Answers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            var cascadeFks = modelBuilder.Model.GetEntityTypes().
                                                SelectMany(Model => Model.GetForeignKeys())
                                                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var Fk in cascadeFks)
                Fk.DeleteBehavior = DeleteBehavior.Restrict;

            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {

            var entries = ChangeTracker.Entries<AuditableEntity>();
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.Now;
                    entry.Entity.CreatedById = userId; // Replace with actual user ID
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = DateTime.Now;
                    entry.Entity.UpdatedById = userId; // Replace with actual user ID
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

    }
}
