using Mapster;
using SurveyBaskt.Errors;

namespace SurveyBaskt.Services
{
    public class QuesionService(ApplicatonDbContext _dbContext) : IQuesionService
    {


        public async Task<Result<IEnumerable<QuesionResponse>>> GetAllAsync(int pollId, CancellationToken cancellationToken)
        {
            bool PollIsExcist = await _dbContext.Polls.AnyAsync(p=>p.Id == pollId , cancellationToken );
            if (!PollIsExcist) return   Result.Failure<IEnumerable<QuesionResponse>>(PollError.PollNotFound); 

            var result = await _dbContext.Quesions.Where(q => q.PollId == pollId ).AsNoTracking().ProjectToType<QuesionResponse>().ToListAsync(cancellationToken);
            return Result.Success<IEnumerable<QuesionResponse>>(result);
        }


        public async Task<Result<IEnumerable<QuesionResponse>>> GetCurrentlyActiveQuesionsAsync(int pollId, string userId, CancellationToken cancellationToken)
        {
           var pollisExist =await _dbContext.Polls.AnyAsync(p => p.Id == pollId && p.Ispublished 
                                                 && p.StartsAt <= DateOnly.FromDateTime(DateTime.UtcNow) && p.EndsAt >= DateOnly.FromDateTime(DateTime.UtcNow),cancellationToken);
            if(!pollisExist) 
                return Result.Failure<IEnumerable<QuesionResponse>>(PollError.PollNotFound);
            var userHasAlreadyAnswered = await _dbContext.Votes.AnyAsync(v=>v.UserId == userId && v.PollId == pollId,cancellationToken);
            if(userHasAlreadyAnswered)
                return Result.Failure<IEnumerable<QuesionResponse>>(VoteError.UserHasAlreadyAnswered);
            var questions = await _dbContext.Quesions
                                 .Where(q=>q.PollId == pollId && q.IsActive)
                                 .Include(q => q.Answers)
                .Select(  q=> new QuesionResponse(

                    q.Id ,
                    q.Content,
                    q.Answers.Where(a => a.IsActive).Select(a => new AnswerResponse
                    (
                       a.Id,
                       a.Content
                    ))
                ))
                .AsNoTracking()
                .ToListAsync(cancellationToken);
            return Result.Success<IEnumerable<QuesionResponse>>(questions);
        }
        public async Task<Result<QuesionResponse>> GetAsync(int pollId, int Id, CancellationToken cancellationToken)
        {
            bool pollIsExcist = await _dbContext.Polls.AnyAsync(p=>p.Id ==pollId , cancellationToken);
            if (!pollIsExcist) return Result.Failure<QuesionResponse>(PollError.PollNotFound);

            var quesion =await _dbContext.Quesions.Where(q=>q.Id == Id && q.PollId == pollId).ProjectToType<QuesionResponse>().FirstOrDefaultAsync();

            if (quesion is null) return Result.Failure<QuesionResponse>(QuesionError.QuesionNotFound);
            
            return Result.Success(quesion);

        }
        public async Task<Result<QuesionResponse>> AddAsync(int pollId,QuesionRequest request , CancellationToken cancellationToken)
        {

            var pollIsExist = await _dbContext.Polls.AnyAsync(p => p.Id == pollId, cancellationToken: cancellationToken);

            if (!pollIsExist)
                return Result.Failure<QuesionResponse>(PollError.PollNotFound);

            var questionContentIsExist = await _dbContext.Quesions.AnyAsync(q => q.Content == request.Content && q.PollId == pollId, cancellationToken: cancellationToken);

            if(questionContentIsExist)          
                return Result.Failure<QuesionResponse>(QuesionError.DubliateContent);


            var question = request.Adapt<Quesion>();

            question.PollId = pollId;

           
            //request.Answers.ForEach(answer => question.Answers.Add(new Answer { Content = answer }));

            await _dbContext.Quesions.AddAsync(question, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var response = question.Adapt<QuesionResponse>();
            return Result.Success(response);
        }


        public async Task<Result> UpdateAsync(int pollId, int id, QuesionRequest request, CancellationToken cancellationToken)
        {
            var quesionIsExist = await _dbContext.Quesions.AnyAsync(q =>q.Content == request.Content && q.PollId == pollId && q.Id != id, cancellationToken: cancellationToken);

            if (quesionIsExist)
                return Result.Failure(QuesionError.DubliateContent);

            var quesion = await _dbContext.Quesions.Include(a=>a.Answers).SingleOrDefaultAsync(q => q.Id == id && q.PollId == pollId, cancellationToken);

            if (quesion is null)
                return Result.Failure(QuesionError.QuesionNotFound);


            quesion.Content = request.Content;

            // current answers
            var currentanswer = quesion.Answers.Select(q=>q.Content).ToList();
            // new answers
            var newAnswers = request.Answers.Except(currentanswer).ToList();
            // Add new answers
            newAnswers.ForEach(answer => quesion.Answers.Add(new Answer { Content = answer }));
            // Remove deleted answers
          
            quesion.Answers.ToList().ForEach(answer =>
            {
               answer.IsActive = request.Answers.Contains(answer.Content);
            });

            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Success();

        }


        public async Task<Result> ToggleStatusAsync(int pollId, int Id,  CancellationToken cancellationToken)
        {
           var quesion = _dbContext.Quesions.FirstOrDefault(q => q.Id == Id && q.PollId == pollId);
           if (quesion is null)
               return Result.Failure(QuesionError.QuesionNotFound);

           quesion.IsActive = !quesion.IsActive;
           await _dbContext.SaveChangesAsync(cancellationToken);
           return Result.Success();
        }

    
    }
}
