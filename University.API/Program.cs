using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;
using University.Api.Helpers;
using University.API.Configurations;
using University.API.Modules;
using University.Core.Entities.Identity;
using University.Core.Services;
using University.Data.Contexts;



var builder = WebApplication.CreateBuilder(args);



builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen(c =>

{ 

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });

});


Log.Logger= new LoggerConfiguration()
    .MinimumLevel.Information() //Every incoming request in the controller (Info)
    .WriteTo.File("Logs/log-.txt", rollingInterval:RollingInterval.Day)
    .CreateLogger();


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<UniversityDbContext>(options =>
    options.UseSqlServer(
        connectionString,
        sqlOptions => sqlOptions.MigrationsAssembly("University.Data") 
    ));


builder.Services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<UniversityDbContext>()
    .AddDefaultTokenProviders();


//Dependency Injection
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(container =>
{
    container.RegisterModule<RepositoryModule>();
    container.RegisterModule<ServiceModule>();
    container.RegisterType<UniversityDbContext>().AsSelf().InstancePerLifetimeScope();
    container.RegisterType<AuthService>().As<IAuthService>().InstancePerLifetimeScope();
    container.RegisterType<JwtTokenHelper>().As<IJwtTokenHelper>().InstancePerLifetimeScope();

});

builder.Host.UseSerilog();


var jwtSettings = builder.Configuration.GetSection("JwtSettings");
builder.Services.Configure<JwtSettings>(jwtSettings);


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        var secretKey=Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]);
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(secretKey)
        };
    });


// Add services to the container.
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}





// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.UseAuthentication();    

app.UseAuthorization();

app.MapControllers();

app.Run();
