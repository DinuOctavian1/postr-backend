using API;
using API.Common.Errors;
using Application;
using Infrastructure;
using Microsoft.AspNetCore.Mvc.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddAplication()
        .AddInfrastructure(builder.Configuration)
        .AddApi();


    builder.Services.AddControllers();
    builder.Services.AddSingleton<ProblemDetailsFactory, PostrProblemDetailsFactory>();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}




var app = builder.Build();
{
    app.UseExceptionHandler("/error");
    app.UseCors(options => options
                  .WithOrigins(builder.Configuration["ClientUrl"])
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .AllowCredentials());

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();

}

