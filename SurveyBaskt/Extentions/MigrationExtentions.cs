namespace SurveyBaskt.Extentions
{
    public static class MigrationExtentions
    {
        public static void ApplyMigrations(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
          var dbContext =   scope.ServiceProvider.GetRequiredService<ApplicatonDbContext>();
            dbContext.Database.Migrate();
        }
    }
}
