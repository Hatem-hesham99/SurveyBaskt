using Mapster;
using SurveyBaskt.Contracts.Responses;

namespace SurveyBaskt.Mapping
{
    public class MappingConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Poll, PollResponse>()
                .Map(dest => dest.Name, src => src.Title);
            config.NewConfig<PollResponse,Poll>()
                .Map(dest=>dest.Title, src=>src.Name);

            // Quesion mapping

            config.NewConfig<QuesionRequest, Quesion>()
                .Map(dis=>dis.Answers , src=>src.Answers.Select(answer=> new Answer { Content = answer })  );
                //.Ignore(src => src.Answers);

        }
    }
}
