using Vocabulary.API.Endpoints;
using Vocabulary.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddVocabularyModule(builder.Configuration);

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

VocabularyEndpoints.MapVocabularyEndpoints(app);

app.Run();
