using System.ComponentModel.DataAnnotations;

namespace SurveyBaskt.Authantication
{
    public class JwtOptions
    {
        [Required]
        public string SigningKey { get; init; } = string.Empty;
        [Required]
        public string Issuer { get; init; } = string.Empty;
        [Required]
        public string Audience { get; init; } = string.Empty;
        [Required,Range(1,int.MaxValue,ErrorMessage = "invalid Expires In Minutes ")]
        public int ExpiresInMinutes { get; set; }

    }
}
