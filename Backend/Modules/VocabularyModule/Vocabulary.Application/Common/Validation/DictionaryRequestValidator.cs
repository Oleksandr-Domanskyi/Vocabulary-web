using Backend.Shared.Validation;
using FluentValidation;
using Vocabulary.Core.DTO.Request.Dictionary;
using Vocabulary.Core.Entity.Enum;

namespace Vocabulary.Application.Common.Validation;

public sealed class DictionaryRequestValidator : AbstractValidator<DictionaryRequest>
{
    public DictionaryRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(150);
        RuleFor(x => x.DictionaryUrl).NotEmpty().MaximumLength(2048).HttpUrl();
        RuleFor(x => x.Language).EnumName<DictionaryRequest, LanguagesType>();
    }
}
