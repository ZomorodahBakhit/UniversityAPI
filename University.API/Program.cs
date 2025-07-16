using Autofac;
using Autofac.Extensions.DependencyInjection;
using Serilog;
using University.API.Modules;
using University.Core.Services;
using University.Data.Contexts;
using University.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen();   

Log.Logger= new LoggerConfiguration()
    .MinimumLevel.Information() //Every incoming request in the controller (Info)
    .WriteTo.File("Logs/log-.txt", rollingInterval:RollingInterval.Day)
    .CreateLogger();

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.ConfigureContainer<ContainerBuilder>(container =>
{
    container.RegisterModule<RepositoryModule>();
    container.RegisterModule<ServiceModule>();
    container.RegisterType<UniversityDbContext>().AsSelf().InstancePerLifetimeScope();
});

builder.Host.UseSerilog();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
