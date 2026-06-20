using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SurveyBaskt.Authantication
{
    public class JwtProvider(IOptions<JwtOptions> jwtoptions) : IJwtProvider
    {
        private readonly JwtOptions _jwtoptions = jwtoptions.Value;

        public (string Token, int expiresIn) GenerateToken(ApplicationUser user)
        {

            var secretKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_jwtoptions.SigningKey)); // Replace with your actual secret key

            SigningCredentials userSigningCredential = new SigningCredentials (secretKey, SecurityAlgorithms.HmacSha256);

            Claim[] UserClaims =
                [
                    new (JwtRegisteredClaimNames.Sub, user.Id),
                    new (ClaimTypes.NameIdentifier, user.Id),
                    new (ClaimTypes.Email, user.Email!),
                    new (ClaimTypes.GivenName, user.FirstName),
                    new (ClaimTypes.Surname, user.LastName)

                ];

            //var expiresin = 30;

            JwtSecurityToken myToken = new JwtSecurityToken(
                 issuer: _jwtoptions.Issuer,
                 audience:_jwtoptions.Audience,
                 expires: DateTime.Now.AddMinutes(_jwtoptions.ExpiresInMinutes),
                 claims: UserClaims,
                 signingCredentials: userSigningCredential
                );

            string token = new JwtSecurityTokenHandler().WriteToken(myToken);

            token = $"Bearer {token}";

            return (Token: token,expiresIn: _jwtoptions.ExpiresInMinutes *60);
           
        }
    }
}
