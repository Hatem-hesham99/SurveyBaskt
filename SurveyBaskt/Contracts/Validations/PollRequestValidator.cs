

namespace SurveyBaskt.Contracts.Validations
{
    public class PollRequestValidator : AbstractValidator<PollRequest>
    {
        public PollRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MinimumLength(3).WithMessage("Title must be at least 3 characters long.")
                .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");
            RuleFor(x => x.Summary)
                .NotEmpty().WithMessage("Summary is required. ")
                .MaximumLength(1500);

            RuleFor(x=>x.StartsAt)
                .NotEmpty().WithMessage("Starts date is required.")
                .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today)).WithMessage("Start date cannot be in the past.")
                .LessThanOrEqualTo(x => x.EndsAt).WithMessage("Start date must be before end date.");

        }
    }
}
