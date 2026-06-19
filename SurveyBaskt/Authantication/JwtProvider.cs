using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SurveyBaskt.Authantication
{
    public class JwtProvider : IJwtProvider
    {
        public (string Token, int expiresIn) GenerateToken(ApplicationUser user)
        {

            var secretKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("J3rpXIEJ7PNNHSSlOsFRLf1PTnAC1DhW")); // Replace with your actual secret key

            SigningCredentials userSigningCredential = new SigningCredentials (secretKey, SecurityAlgorithms.HmacSha256);

            Claim[] UserClaims =
                [
                    new (JwtRegisteredClaimNames.Sub, user.Id),
                    new (ClaimTypes.NameIdentifier, user.Id),
                    new (ClaimTypes.Email, user.Email!),
                    new (ClaimTypes.GivenName, user.FirstName),
                    new (ClaimTypes.Surname, user.LastName)

                ];

            var expiresin = 30;

            JwtSecurityToken myToken = new JwtSecurityToken(
                 issuer: "SurveyBaskt App create by hatem",
                 audience:"SurveyBaskt Users",
                 expires: DateTime.Now.AddMinutes(expiresin),
                 claims: UserClaims,
                 signingCredentials: userSigningCredential
                );

            string token = new JwtSecurityTokenHandler().WriteToken(myToken);

            token = $"Bearer +{token}";

            return (Token: token,expiresIn: expiresin);
           
        }
    }
}
