using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Diagnostics.CodeAnalysis;

namespace SurveyBaskt.Contracts.Requests
{
    public record PollRequest(
     string Title,
     string Summary,
     bool Ispublished,
     string? CreatedById ,
     DateTime CreatedAt,
     string? UpdatedById,
     DateTime? UpdatedAt,
     DateOnly StartsAt,
     DateOnly EndsAt
        );

}
