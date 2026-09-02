using Vocabulary.API.minimalApi;
using Vocabulary.API.Extentions;
using Vocabulary.Infrastructure.Data;
using Vocabulary.Infrastructure.Seed;
using Vocabulary.Application.Extensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddVocabulary(builder.Configuration);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<VocabularyDbContext>();
    var seed = new VocabularySeed();
    await seed.seed(context);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => "Hello World!");

MinimalApi.MapVocabularyEndpoints(app);

app.Run();
