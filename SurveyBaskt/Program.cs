using Microsoft.EntityFrameworkCore;
using SurveyBaskt;
using SurveyBaskt.Middleware;
using SurveyBaskt.persistence;



var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration.Sources;


builder.Services.AddDependencies(builder.Configuration );
//builder.Services.AddIdentityApiEndpoints<ApplicationUser>().AddEntityFrameworkStores<ApplicatonDbContext>(); 
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "My API v1");
    });
}


app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();
app.UseExceptionHandler();

//app.MapIdentityApi<ApplicationUser>();
app.MapControllers();

app.Run();
