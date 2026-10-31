using Vocabulary.Application.Common.Errors;
using Vocabulary.Application.Common.Exeptions;
using Vocabulary.Application.Mappers.Vocabulary.EnumMapper;
using Vocabulary.Core.DTO.Request.Transaltion;
using Vocabulary.Core.Entity;
using Vocabulary.Core.Entity.Enum;

namespace Vocabulary.Application.Mappers.Vocabulary;
/// <summary>
/// Synchronizes vocabulary translations and their associated examples.
/// Preserves existing entity IDs and creates new translations when needed.
/// </summary>
public static partial class VocabularyRelationsMapper
{
    private static TranslationModel CreateTranslation(TranslationRequest request, Guid vocabularyId)
    {
        var translation = new TranslationModel
        {
            VocabularyId = vocabularyId,
            TranslatedWord = request.TranslatedWord,
            TranslatedDefinition = request.TranslatedDefinition,
            LanguageCode = GenericEnumMapper.DefineType<LanguagesType>(request.LanguageCode)
        };
        MapUpdateTranslationExamples(translation, request.TranslationExamples);

        return translation;
    }

    public static List<TranslationModel> MapUpdateTranslation(List<TranslationModel> domain, List<TranslationRequest> requests, Guid vocabularyId)
    {
        var existing = domain.ToDictionary(x => x.Id);
        var requestedIds = new HashSet<Guid>();

        foreach (var request in requests)
        {
            if (request.Id is null)
            {
                domain.Add(CreateTranslation(request, vocabularyId));
                continue;
            }

            var id = request.Id.Value;

            if (!requestedIds.Add(id))
                throw new ArgumentException($"Duplicate Translation ID: {id}");

            if (!existing.TryGetValue(id, out var translation))
                throw new KeyNotFoundException($"Translation '{id}' not found.");

            translation.TranslatedWord = request.TranslatedWord;
            translation.TranslatedDefinition = request.TranslatedDefinition;
            translation.LanguageCode = GenericEnumMapper.DefineType<LanguagesType>(request.LanguageCode);

            MapUpdateTranslationExamples(
                translation,
                request.TranslationExamples);
        }

        domain.RemoveAll(x =>
            existing.ContainsKey(x.Id) && !requestedIds.Contains(x.Id));

        return domain;
    }
}
