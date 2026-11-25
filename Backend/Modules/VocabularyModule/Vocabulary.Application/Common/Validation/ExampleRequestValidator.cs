using FluentValidation;
using Vocabulary.Core.DTO.Request.Example;

namespace Vocabulary.Application.Common.Validation;

public sealed class ExampleRequestValidator : AbstractValidator<ExampleRequest>
{
    public ExampleRequestValidator()
    {
        RuleFor(x => x.Sentence).NotEmpty().MaximumLength(1000);
    }
}
