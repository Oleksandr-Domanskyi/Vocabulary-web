using Vocabulary.Core.DTO.Request.Example;
using Vocabulary.Core.DTO.Request.Transaltion;
using Vocabulary.Core.Entity;

namespace Vocabulary.Application.Mappers.Vocabulary;

public static partial class VocabularyRelationsMapper
{
    public static List<Example> MapListExamplesRequestToModel(List<ExampleRequest> requests)
        => requests.ConvertAll(MapExampleRequestToModel);

    public static Example MapExampleRequestToModel(ExampleRequest request)
        => new()
        {
            Sentences = request.Sentence
        };
    public static List<Example> MapUpdateExamples(List<Example> domain, List<ExampleRequest> requests)
    {
        var existing = domain.ToDictionary(x => x.Id);
        var requestedIds = new HashSet<Guid>();

        foreach (var request in requests)
        {
            if (request.Id is null)
            {
                domain.Add(MapExampleRequestToModel(request));
                continue;
            }

            var id = request.Id.Value;

            if (!requestedIds.Add(id))
                throw new ArgumentException($"Duplicate Example ID: {id}");

            if (!existing.TryGetValue(id, out var example))
                throw new KeyNotFoundException($"Example '{id}' not found.");

            example.Sentences = request.Sentence;
        }

        domain.RemoveAll(x => existing.ContainsKey(x.Id) && !requestedIds.Contains(x.Id));

        return domain;
    }

}
