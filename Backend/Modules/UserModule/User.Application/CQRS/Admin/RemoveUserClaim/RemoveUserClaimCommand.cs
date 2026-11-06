using MediatR;
using User.Application.Common.Result;
using User.Application.DTO.Requests.Admin;
namespace User.Application.CQRS.Admin.RemoveUserClaim;

public sealed record RemoveUserClaimCommand(Guid UserId, RemoveUserClaimRequest Data) : IRequest<UserResult<bool>>;
