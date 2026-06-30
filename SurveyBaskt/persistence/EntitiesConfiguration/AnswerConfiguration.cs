namespace SurveyBaskt.persistence.EntitiesConfiguration
{
    public class AnswerConfiguration : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.HasIndex(a => new { a.QuesionId, a.Content }).IsUnique();
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Content)
                .IsRequired()
                .HasMaxLength(500);

            builder.HasOne(a => a.Quesion)
                .WithMany(q => q.Answers)
                .HasForeignKey(a => a.QuesionId);
        
        }
    }
}
