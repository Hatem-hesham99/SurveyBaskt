namespace SurveyBaskt.Contracts.Validations
{
    public class AuthRequestValidator : AbstractValidator<AuthRequest>
    {
        public AuthRequestValidator() 
        {
            RuleFor(r => r.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress();
                
            RuleFor(r=>r.Password)
                .NotEmpty().WithMessage("Password is required.");
        }
    }
    
}
