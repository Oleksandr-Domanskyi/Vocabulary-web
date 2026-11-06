using Backend.Shared.Errors;
using Vocabulary.Application.Common.Errors;
using Vocabulary.Application.Common.Exeptions;

namespace Vocabulary.Application.Mappers.Vocabulary.EnumMapper;

public static class GenericEnumMapper
{
    public static TEnum DefineType<TEnum>(string input) where TEnum : struct, Enum
       => Enum.TryParse<TEnum>(input, ignoreCase: true, out var result) && Enum.IsDefined(result)
           ? result
           : throw new VocabularyException(CommonErrors.InvalidEnum(typeof(TEnum).Name, input));

}
