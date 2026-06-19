using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SurveyBaskt.Authantication;
using SurveyBaskt.Contracts.Responses;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SurveyBaskt.Services
{
    public class AuthService(UserManager<ApplicationUser>  _userManager ,IJwtProvider _jwtProvider) : IAuthService
    {
        public async Task<AuthResponse?> GetTokenAsync(string email, string password, CancellationToken cancellationToken = default)
        {
            // valid email  
            ApplicationUser? user = await _userManager.FindByEmailAsync(email);
            if(user == null)
                 return null;

            // valid password
            bool isPasswordValid = await _userManager.CheckPasswordAsync(user, password);
            if (!isPasswordValid) 
                    return null;

            //describe the token
            var (mytoken, expiresIn) = _jwtProvider.GenerateToken(user);

            // return response
            return new AuthResponse (Id: user.Id, FirstName: user.FirstName, LastName: user.LastName,Email: user.Email, Token: mytoken, ExpiresAt: expiresIn) ;
        }
    }
}
