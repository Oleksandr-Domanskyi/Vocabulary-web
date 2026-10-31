using Vocabulary.API.minimalApi;
using Vocabulary.API.Extentions;
using Vocabulary.Infrastructure.Data;
using Vocabulary.Infrastructure.Seed;
using Vocabulary.Application.Extensions;
using Vocabulary.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddVocabulary(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

//Seeder
await app.SeedVocabularyAsync();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

MinimalApi.MapVocabularyEndpoints(app);

app.Run();
