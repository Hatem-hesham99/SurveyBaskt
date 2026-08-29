namespace SurveyBaskt.persistence.EntitiesConfiguration
{
    public class VoteConfiguration : IEntityTypeConfiguration<Vote>
    {
        public void Configure(EntityTypeBuilder<Vote> builder)
        {
            builder.HasIndex(v => new { v.UserId, v.PollId }).IsUnique();
        }
    }
}
