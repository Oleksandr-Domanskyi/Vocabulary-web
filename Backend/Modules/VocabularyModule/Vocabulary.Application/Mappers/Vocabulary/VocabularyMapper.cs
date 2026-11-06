using Vocabulary.Core.Entity.Enum;
using Vocabulary.Core.DTO.Response;
using Vocabulary.Core.DTO.Request.Vocabulary;
using Vocabulary.Application.Mappers.Vocabulary.EnumMapper;
using Vocabulary.Core.Entity.Vocabulary;

namespace Vocabulary.Application.Mappers.Vocabulary;

public static class VocabularyMapper
{
    public static List<VocabularyResponse> VocabularyModelToResponse(List<VocabularyModel> models)
        => models.ConvertAll(VocabularyModelToResponse);

    public static VocabularyResponse VocabularyModelToResponse(VocabularyModel model)
        => new()
        {
            WordId = model.Id,
            UserId = model.UserId,
            Word = model.Word,
            WordType = model.WordType,
            Level = model.Level,
            Examples = model.Examples,
            Dictionaries = model.Dictionaries,
            Translations = model.Translations,
            Definition = model.Definition,
            Transcription = model.Transcription,
            CreatedAt = model.CreatedAt,
            ImageUrl = model.ImageUrl
        };

    public static VocabularyModel CreateRequestToVocabularyModel(CreateVocabularyRequest request)
        => new()
        {
            UserId = request.UserId,
            Word = request.Word,
            WordType = GenericEnumMapper.DefineType<WordType>(request.WordType),
            Level = GenericEnumMapper.DefineType<Level>(request.EnglishLevel),
            Examples = VocabularyRelationsMapper.MapListExamplesRequestToModel(request.Examples),
            Dictionaries = VocabularyRelationsMapper.MapListDictionaryRequestToModel(request.Dictionaries),
            Translations = VocabularyRelationsMapper.MapListTranslationRequestToModel(request.Translations),
            Transcription = request.Transcription,
            Definition = request.Definition,
            CreatedAt = DateTime.UtcNow,
            ImageUrl = request.ImageUrl
        };

    public static VocabularyModel UpdateRequestToVocabularyModel(VocabularyModel model, UpdateVocabularyRequest request)
    {
        model.Word = request.Word;
        model.WordType = GenericEnumMapper.DefineType<WordType>(request.WordType);
        model.Level = GenericEnumMapper.DefineType<Level>(request.EnglishLevel);
        model.Definition = request.Definition;
        model.Transcription = request.Transcription;
        model.ImageUrl = request.ImageUrl;
        model.Examples = VocabularyRelationsMapper.MapUpdateExamples(model.Examples, request.Examples);
        model.Dictionaries = VocabularyRelationsMapper.MapUpdateDictionaries(model.Dictionaries, request.Dictionaries);
        model.Translations = VocabularyRelationsMapper.MapUpdateTranslation(model.Translations, request.Translations, model.Id);

        return model;
    }
}
