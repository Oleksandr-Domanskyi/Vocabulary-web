using Backend.Shared.Errors;
using Vocabulary.Application.Common.Exeptions;
using Vocabulary.Application.Mappers.Vocabulary.EnumMapper;
using Vocabulary.Core.DTO.Request.Transaltion;
using Vocabulary.Core.Entity.Enum;
using Vocabulary.Core.Entity.Translation;

namespace Vocabulary.Application.Mappers.Vocabulary;
/// <summary>
/// Synchronizes vocabulary translations and their associated examples.
/// Preserves existing entity IDs and creates new translations when needed.
/// </summary>
public static partial class VocabularyRelationsMapper
{
    public static List<TranslationModel> MapListTranslationRequestToModel(List<TranslationRequest> requests)
    => requests.ConvertAll(requests => new TranslationModel
    {
        TranslatedWord = requests.TranslatedWord,
        TranslatedDefinition = requests.TranslatedDefinition,
        LanguageCode = GenericEnumMapper.DefineType<LanguagesType>(requests.LanguageCode),
        TranslationExamples = MapListTrExamplesRequestToModel(requests.TranslationExamples)
    });
    public static List<TranslationExample> MapListTrExamplesRequestToModel(List<TranslationExampleRequest> requests)
    => requests.ConvertAll(request => new TranslationExample
    {
        TranslatedSentences = request.TranslatedSentence
    });

    public static List<TranslationExample> MapUpdateTranslationExamples(TranslationModel translation, List<TranslationExampleRequest> requests)
    {
        var domain = translation.TranslationExamples;
        var existing = domain.ToDictionary(x => x.Id);
        var requestedIds = new HashSet<Guid>();

        foreach (var request in requests)
        {
            if (request.Id is null)
            {
                if (request.ExampleId is null)
                    throw new VocabularyException(CommonErrors.InvalidId());

                domain.Add(new TranslationExample
                {
                    ExampleId = request.ExampleId.Value,
                    TranslationId = translation.Id,
                    TranslatedSentences = request.TranslatedSentence
                });

                continue;
            }

            var id = request.Id.Value;

            if (!requestedIds.Add(id))
                throw new VocabularyException(CommonErrors.Duplicated(id));

            if (!existing.TryGetValue(id, out var example))
                throw new VocabularyException(CommonErrors.NotFound("TranslationExample", id));

            if (request.ExampleId is Guid exampleId)
                example.ExampleId = exampleId;

            example.TranslatedSentences = request.TranslatedSentence;
        }
        domain.RemoveAll(x =>
            existing.ContainsKey(x.Id) && !requestedIds.Contains(x.Id));

        return domain;
    }
}
