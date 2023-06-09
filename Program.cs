using Postr.Configurations;
using Postr.Extensions;
using Postr.Middelware;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services.AddCors();
    builder.Services.AddAutoMapper(typeof(MapperInitializer));

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.Configure<EmailConfig>(builder.Configuration.GetSection("EmailConfig"));

    builder.Services.ConfigureDB(builder.Configuration);
    builder.Services.AddDependencyInjection();

    builder.Services.AddIdentityServices(builder.Configuration);

   
    builder.Services.AddHttpClient();
}



var app = builder.Build();
{

    // Configure the HTTP request pipeline. 
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseCors(options => options
                  .WithOrigins(builder.Configuration["AppUrl"])
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .AllowCredentials());

    app.UseHttpsRedirection();

    app.UseMiddleware<JWTMiddleware>();


    app.UseAuthentication();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}

