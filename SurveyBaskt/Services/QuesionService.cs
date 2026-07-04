using Mapster;
using SurveyBaskt.Errors;

namespace SurveyBaskt.Services
{
    public class QuesionService(ApplicatonDbContext _dbContext) : IQuesionService
    {
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

           
            request.Answers.ForEach(answer => question.Answers.Add(new Answer { Content = answer }));

            await _dbContext.Quesions.AddAsync(question, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var response = question.Adapt<QuesionResponse>();
            return Result.Success(response);
        }
    }
}
