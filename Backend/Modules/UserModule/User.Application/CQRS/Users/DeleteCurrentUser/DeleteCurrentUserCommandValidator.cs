using FluentValidation;

namespace User.Application.CQRS.Users.DeleteCurrentUser;

public sealed class DeleteCurrentUserCommandValidator : AbstractValidator<DeleteCurrentUserCommand>
{
    public DeleteCurrentUserCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
    }
}
