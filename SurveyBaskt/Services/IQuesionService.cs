
namespace SurveyBaskt.Services
{
    public interface IQuesionService
    {
      Task<Result<QuesionResponse>> AddAsync(int pollId, QuesionRequest request, CancellationToken cancellationToken);
    }
}
