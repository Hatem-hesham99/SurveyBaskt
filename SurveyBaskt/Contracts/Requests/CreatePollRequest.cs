using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SurveyBaskt.Contracts.Requests
{
    public record CreatePollRequest(
        string Title,
        string Description
        );

}
