using SurveyBaskt.Contracts.Responses;

namespace SurveyBaskt.Services
{
    public interface IPollService
    {
        Task<IEnumerable<PollResponse>> GetAllAsync(CancellationToken cancellationToken = default); 

        Task<PollResponse?> GetAsync(int id, CancellationToken cancellationToken = default);

        Task<PollResponse> AddAsync(PollRequest request , CancellationToken cancellationToken= default );

        Task<bool> UpdateAsync(int id , PollRequest poll , CancellationToken cancellationToken = default );

       Task< bool> DeleteAsync(int id,CancellationToken cancellationToken);
       Task< bool> TogelPublish(int id,CancellationToken cancellationToken);

        //Poll? Get(int id);

        //Poll Add(Poll poll);

        //bool Update(int id, Poll poll);

        //bool Delete(int id);

    }
}
