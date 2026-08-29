using SurveyBaskt.Contracts.Responses;

namespace SurveyBaskt.Services
{
    public interface IPollService
    {
        Task<IEnumerable<PollResponse>> GetAllAsync(CancellationToken cancellationToken = default); 

        Task<IEnumerable<PollResponse>> GetCurrentlyActivePollsAsync(CancellationToken cancellationToken = default);

        Task<Result<PollResponse>> GetAsync(int id, CancellationToken cancellationToken = default);

        Task<Result<PollResponse>> AddAsync(PollRequest request , CancellationToken cancellationToken= default );

        Task<Result> UpdateAsync(int id , PollRequest poll , CancellationToken cancellationToken = default );

       Task<Result> DeleteAsync(int id,CancellationToken cancellationToken);
       Task<Result> TogglePublishAsync(int id,CancellationToken cancellationToken);

        //Poll? Get(int id);

        //Poll Add(Poll poll);

        //bool Update(int id, Poll poll);

        //bool Delete(int id);

    }
}
