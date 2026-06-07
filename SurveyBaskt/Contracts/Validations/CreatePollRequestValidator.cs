

namespace SurveyBaskt.Contracts.Validations
{
    public class CreatePollRequestValidator : AbstractValidator<CreatePollRequest>
    {
        public CreatePollRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is hatem.")
                .MinimumLength(3).WithMessage("Title must be at least 3 characters long.")
                .MaximumLength(10).WithMessage("Title cannot exceed 10 characters.");
                
        }
    }
}
