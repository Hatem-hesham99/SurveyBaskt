namespace SurveyBaskt.Contracts.Validations
{
    public class VoteRequestValidator : AbstractValidator<VoteRequest>
    {
        public VoteRequestValidator() 
        {
            RuleFor(x => x.VoteAnswers)
                   .NotEmpty().WithMessage("VoteAnswers cannot be empty.").Must(voteAnswers => voteAnswers.All(va => va.QuesionId > 0 && va.AnswerId > 0))
                    .WithMessage("QuestionId and AnswerId must be greater than 0.");
        }
    }
}
