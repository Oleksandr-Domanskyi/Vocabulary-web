using Vocabulary.API.Endpoints;
using Vocabulary.API.Extensions;
using User.API.Extensions;
using User.API.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddVocabularyModule(builder.Configuration);
builder.Services.AddUserModule(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCorsExtension();

//Seeder
var userId = await app.SeedUserAsync();
await app.SeedUserVocabularyAsync(userId!);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

VocabularyEndpoints.MapVocabularyEndpoints(app);
app.MapUserEndpoints();

app.Run();
