namespace SurveyBaskt.Contracts.Validations
{
    public class QuesionRequestValidator : AbstractValidator<QuesionRequest>
    {
        public QuesionRequestValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Content is required.")
                .Length(3, 500).WithMessage("Content must be between 3 and 500 characters.");

            RuleFor(x => x.Answers)
                .NotNull().WithMessage("Answers are required.")
                .Must(answers => answers.Count >= 2).WithMessage("At least two answers are required.")
                .Must(answers => answers.All(answer => !string.IsNullOrWhiteSpace(answer))).WithMessage("Answers cannot be empty or whitespace.");

            RuleFor(x=>x.Answers).Must(answer => answer.Distinct().Count() == answer.Count).WithMessage("Answers must be unique.");
        }
    }
}
