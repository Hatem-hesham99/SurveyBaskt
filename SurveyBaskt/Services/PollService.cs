
using Mapster;
using SurveyBaskt.Contracts.Responses;

namespace SurveyBaskt.Services
{
    public class PollService(ApplicatonDbContext dbContext) : IPollService
    {
        private readonly ApplicatonDbContext _dbContext = dbContext ;

        public async Task<IEnumerable<PollResponse>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            
               return  await _dbContext.Polls
                                            .AsNoTracking()
                                            .ProjectToType<PollResponse>()
                                            .ToListAsync(cancellationToken);
   
        }

        public Task<PollResponse?> GetAsync(int id, CancellationToken cancellationToken = default)
        {
           return _dbContext.Polls.AsNoTracking().Where(p=>p.Id == id).ProjectToType<PollResponse>().FirstOrDefaultAsync(cancellationToken);   
        }

        public async Task<PollResponse> AddAsync(PollRequest request , CancellationToken cancellationToken = default )
        {
          var poll = request.Adapt<Poll>();
          await _dbContext.Polls.AddAsync(poll, cancellationToken); 
          await _dbContext.SaveChangesAsync(cancellationToken);
          var response = poll.Adapt<PollResponse>();
          return response;
        }

        public async Task<bool> UpdateAsync(int id, PollRequest createPoll , CancellationToken cancellationToken = default)
        {
            PollResponse? pollResponse = await GetAsync(id, cancellationToken);
            if(pollResponse == null) return false;

            var poll = pollResponse.Adapt<Poll>();
            
            poll.Summary = createPoll.Summary;
            poll.Ispublished = createPoll.Ispublished;
            poll.Title= createPoll.Title;
            poll.EndsAt = createPoll.EndsAt;
            poll.StartsAt = createPoll.StartsAt;

            _dbContext.Update(poll);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;


        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        { 
            var pollresponse = await GetAsync(id, cancellationToken);
            if(pollresponse == null) return false;
            var poll = pollresponse.Adapt<Poll>();
            _dbContext.Polls.Remove(poll);  
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> TogelPublish(int id, CancellationToken cancellationToken)
            {
                var pollresponse = await GetAsync(id, cancellationToken);
                if(pollresponse == null) return false;
                var poll = pollresponse.Adapt<Poll>();
                poll.Ispublished = !poll.Ispublished;
                _dbContext.Polls.Update(poll);  
                await _dbContext.SaveChangesAsync();
                return true;
        }

        #region In-Memory Data Store (Commented Out)
        //    private static readonly List<Poll> _polls = new List<Poll>
        //    {
        //        new Poll { Id = 1,
        //                   Title = "Favorite Programming Language?",
        //                   Ispublished = true },
        //        new Poll { Id = 2, Title = "Best Web Framework?", Ispublished = true },
        //    };
        //    public IEnumerable<Poll> GetAll()
        //    {
        //        return _polls;
        //    }
        //    public Poll? Get(int id)
        //    {
        //            var poll = _polls.SingleOrDefault(p => p.Id == id);
        //            return poll;
        //    }

        //    public Poll Add(Poll poll)
        //    {
        //        poll.Id = _polls.Max(p => p.Id) + 1;
        //        _polls.Add(poll);
        //        return poll;    
        //    }

        //    public bool Update(int id, Poll poll)
        //    {
        //        if (poll.Id == null ) return false;

        //        var findPoll = Get(id);

        //        if (findPoll == null)
        //            return false;

        //        findPoll.Title = poll.Title;
        //        findPoll.Ispublished = poll.Ispublished;
        //        return true;
        //    }

        //    public bool Delete(int id)
        //    {
        //        if (id == null) return false;

        //        var findPoll = Get(id);

        //        if (findPoll == null)
        //            return false;
        //        _polls.Remove(findPoll);
        //        return true;
        //    }

        #endregion
    }
}