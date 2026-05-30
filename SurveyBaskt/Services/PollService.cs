namespace SurveyBaskt.Services
{
    public class PollService : IPollService
    {
        private static readonly List<Poll> _polls = new List<Poll>
        {
            new Poll { Id = 1,
                       Title = "Favorite Programming Language?",
                       Description = "Vote for your favorite programming language." },
            new Poll { Id = 2, Title = "Best Web Framework?", Description = "Vote for the best web framework." },
        };
        public IEnumerable<Poll> GetAll()
        {
            return _polls;
        }
        public Poll? Get(int id)
        {
                var poll = _polls.SingleOrDefault(p => p.Id == id);
                return poll;
        }

        public Poll Add(Poll poll)
        {
            poll.Id = _polls.Max(p => p.Id) + 1;
            _polls.Add(poll);
            return poll;    
        }

        public bool Update(Poll poll)
        {
            if (poll.Id == null ) return false;

            var findPoll = Get(poll.Id);

            if (findPoll == null)
                return false;

            findPoll.Title = poll.Title;
            findPoll.Description = poll.Description;
            return true;
        }

        public bool Delete(int id)
        {
            if (id == null) return false;

            var findPoll = Get(id);

            if (findPoll == null)
                return false;
            _polls.Remove(findPoll);
            return true;
        }
    }
}
