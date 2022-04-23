using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using RankingTracker.Services.GoogleRankingService;
using RankingTracker.Services.RankingTrackServices.Queries;
using RankingTracker.Services.RankingTrackServices.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddFluentValidation();
builder.Services.AddMediatR(typeof(GetRankingQueryHandler).Assembly);
builder.Services.AddScoped<IGoogleRankingService, GoogleRankingService>();
builder.Services.AddScoped<IValidator<GetRankingQuery>, GetRankingQueryValidator>(); ;


// config
builder.Services.Configure<GoogleRankingOptions>(builder.Configuration.GetSection(nameof(GoogleRankingOptions)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
