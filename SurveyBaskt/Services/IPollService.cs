namespace SurveyBaskt.Services
{
    public interface IPollService
    {
        IEnumerable<Poll> GetAll(); 

        Poll? Get(int id);

        Poll Add(Poll poll);

        bool Update(Poll poll);

        bool Delete(int id);

    }
}
