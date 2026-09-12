namespace SurveyBaskt.Contracts.Validations
{
    public class VoteRequestValidator : AbstractValidator<VoteRequest>
    {
        public VoteRequestValidator() 
        {
            RuleFor(x => x.VoteAnswers)
                .NotEmpty().WithMessage("VoteAnswers cannot be empty.")
                .Must(voteAnswers => voteAnswers.All(va => !string.IsNullOrWhiteSpace(va.QuesionId) && !string.IsNullOrWhiteSpace(va.AnswerId)))
                .WithMessage("Each VoteAnswer must have a valid QuesionId and AnswerId.");
        }
    }
}
