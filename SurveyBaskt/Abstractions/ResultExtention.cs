namespace SurveyBaskt.Abstractions
{
    public static class ResultExtention
    {

        public static ObjectResult ToProblem(this Result result , int status )
        {
            if (result.IsSuccess)
                throw new InvalidOperationException();


            var problem = Results.Problem(statusCode: status);

           var problemDetails =(ProblemDetails) problem.GetType().GetProperty(nameof(ProblemDetails))?.GetValue(problem)! ;

            problemDetails.Extensions = new Dictionary<string, object?>
            {
               {
                  "error" , new [] { result.Error}
               }
            };

            return new ObjectResult(problemDetails);


            //var problemObject = new ProblemDetails
            //{
            //    Title = titel,
            //    Status = status,
            //    Extensions = new Dictionary<string , object?>
            //    {
            //        {
            //            "error" , new [] { result.Error}
            //        }
            //    }

            //};


        }
    }
}
