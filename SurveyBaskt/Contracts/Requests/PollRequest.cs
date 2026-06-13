using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SurveyBaskt.Contracts.Requests
{
    public record PollRequest(
     string Title,
     string Summary,
     bool Ispublished,
     DateOnly StartsAt,
     DateOnly EndsAt
        );

}
