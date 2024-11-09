using VotePoll.Application;
using VotePoll.Infrastructure.Repositories.Concretes;
using VotePoll.Infrastructure.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(VotePollProfile).Assembly));
builder.Services.AddAutoMapper(typeof(VotePollProfile).Assembly);

builder.Services.AddSingleton<IPollRepository, PollRepository>();
builder.Services.AddSingleton<IPollOptionRepository, PollOptionRepository>();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();  

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(options =>
{
    options.WithOrigins("http://localhost:3000")
        .AllowAnyMethod()
        .AllowAnyHeader();
});

app.MapControllers();

app.Run();
