using Microsoft.AspNetCore.Identity;
using Postr;
using Postr.Configurations;
using Postr.Data;
using Postr.Models;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services.AddCors();
    builder.Services.AddAutoMapper(typeof(MapperInitializer));

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    
    builder.Services.ConfigureDB(builder.Configuration);
    builder.Services.AddDependencyInjection();

    builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<PostrDBContext>()
          .AddDefaultTokenProviders();

    builder.Services.Configure<IdentityOptions>(opts => {
        opts.SignIn.RequireConfirmedEmail = true;
        opts.User.RequireUniqueEmail = true;
        /* opts.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyz";
         opts.Password.RequiredLength = 8;
         opts.Password.RequireLowercase = true;*/
    });


    //TODO: Change password options
    builder.Services.Configure<IdentityOptions>(options =>
    {
        // Default Password settings.
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequiredLength = 5;
        options.Password.RequiredUniqueChars = 0;
    });

    builder.Services.AddAuthentication()
        .AddFacebook(facebookOptions =>
        {
            facebookOptions.AppId = builder.Configuration["FacebookAppId"];
            facebookOptions.AppSecret = builder.Configuration["FacebookAppSecret"];
        });

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
                  .WithOrigins("http://localhost:3000")
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .AllowCredentials());

    app.UseHttpsRedirection();

    app.UseAuthentication();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}

