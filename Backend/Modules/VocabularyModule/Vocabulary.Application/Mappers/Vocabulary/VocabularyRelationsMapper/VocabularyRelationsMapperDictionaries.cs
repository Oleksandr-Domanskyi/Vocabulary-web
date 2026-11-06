using Backend.Shared.Errors;
using Vocabulary.Application.Common.Exeptions;
using Vocabulary.Application.Mappers.Vocabulary.EnumMapper;
using Vocabulary.Core.DTO.Request.Dictionary;
using Vocabulary.Core.Entity.Enum;
using VocabularyDictionary = Vocabulary.Core.Entity.Vocabulary.Dictionary;

namespace Vocabulary.Application.Mappers.Vocabulary;
/// <summary>
/// Provides mapping between request DTOs and vocabulary-related entities.
/// Supports creation and synchronization of Examples, Dictionaries,
/// Translations, and TranslationExamples.
/// </summary>
/// <remarks>
/// <para>
/// Update operations preserve existing entity IDs and modify their properties.
/// New entities are added, while entities missing from the request are removed
/// according to PUT semantics.
/// </para>
/// <para>
/// The mapper is split into multiple files using partial classes.
/// Related entities must be loaded and tracked by EF Core before updating.
/// </para>
/// </remarks>
public static partial class VocabularyRelationsMapper
{
    public static List<VocabularyDictionary> MapListDictionaryRequestToModel(List<DictionaryRequest> requests)
        => requests.ConvertAll(MapDictionaryRequestToModel);

    public static VocabularyDictionary MapDictionaryRequestToModel(DictionaryRequest request) => new()
    {
        Name = request.Name,
        DictionaryUrl = request.DictionaryUrl,
        Language = GenericEnumMapper.DefineType<LanguagesType>(request.Language)
    };

    /// <summary>
    /// Synchronizes dictionaries with the provided requests.
    /// Updates existing entries by ID, adds new entries,
    /// and removes entries missing from the request.
    /// </summary>
    public static List<VocabularyDictionary> MapUpdateDictionaries(List<VocabularyDictionary> domain, List<DictionaryRequest> requests)
    {
        var existing = domain.ToDictionary(x => x.Id);
        var requestedIds = new HashSet<Guid>();

        foreach (var request in requests)
        {
            if (request.Id is null)
            {
                domain.Add(MapDictionaryRequestToModel(request));
                continue;
            }

            var id = request.Id.Value;

            if (!requestedIds.Add(id))
                throw new VocabularyException(CommonErrors.Duplicated(id));

            if (!existing.TryGetValue(id, out var dictionary))
                throw new VocabularyException(CommonErrors.NotFound("Dictionary", id));

            dictionary.Name = request.Name;
            dictionary.DictionaryUrl = request.DictionaryUrl;
            dictionary.Language = GenericEnumMapper.DefineType<LanguagesType>(request.Language);
        }

        domain.RemoveAll(x => existing.ContainsKey(x.Id) && !requestedIds.Contains(x.Id));

        return domain;
    }
}
