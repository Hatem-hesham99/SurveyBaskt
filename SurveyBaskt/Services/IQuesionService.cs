
namespace SurveyBaskt.Services
{
    public interface IQuesionService
    {
      Task<Result<QuesionResponse>> AddAsync(int pollId, QuesionRequest request, CancellationToken cancellationToken);
      Task<Result<IEnumerable<QuesionResponse>>> GetAllAsync(int pollId, CancellationToken cancellationToken);
      Task<Result<QuesionResponse>> GetAsync(int pollId,int Id, CancellationToken cancellationToken);
    }
}
