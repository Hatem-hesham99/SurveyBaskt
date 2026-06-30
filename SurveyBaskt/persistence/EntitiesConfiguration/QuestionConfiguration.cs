namespace SurveyBaskt.persistence.EntitiesConfiguration
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Quesion>
    {
        public void Configure(EntityTypeBuilder<Quesion> builder)
        {
            builder.HasIndex(q=> new {q.PollId, q.Content }).IsUnique();

            builder.HasKey(q => q.Id);

            builder.Property(q => q.Content)
                .IsRequired()
                .HasMaxLength(500);


        }
    }
}
