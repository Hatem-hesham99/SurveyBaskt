using Microsoft.AspNetCore.Identity;
using SurveyBaskt.Authantication;
using SurveyBaskt.Contracts.Responses;
using SurveyBaskt.Errors;


namespace SurveyBaskt.Services
{
    public class AuthService(UserManager<ApplicationUser>  _userManager ,IJwtProvider _jwtProvider) : IAuthService
    {
        public async Task<Result<AuthResponse>> GetTokenAsync(string email, string password, CancellationToken cancellationToken = default)
        {
            // valid email  
            ApplicationUser? user = await _userManager.FindByEmailAsync(email);
            if(user == null)
                 return Result.Failure<AuthResponse>(UserError.InvalidCredentials);

            // valid password
            bool isPasswordValid = await _userManager.CheckPasswordAsync(user, password);
            if (!isPasswordValid)
                return Result.Failure<AuthResponse>(UserError.InvalidCredentials);

            //describe the token
            var (mytoken, expiresIn) = _jwtProvider.GenerateToken(user);

             var response = new AuthResponse (Id: user.Id, FirstName: user.FirstName, LastName: user.LastName,Email: user.Email, Token: mytoken, ExpiresAt: expiresIn) ;
            return Result.Success(response);    
            // return new AuthResponse (Id: user.Id, FirstName: user.FirstName, LastName: user.LastName,Email: user.Email, Token: mytoken, ExpiresAt: expiresIn) ;
        }
    }
}
