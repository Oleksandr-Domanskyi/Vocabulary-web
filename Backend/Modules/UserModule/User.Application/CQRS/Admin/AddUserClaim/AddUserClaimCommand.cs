using MediatR;
using User.Application.Common.Result;
using User.Application.DTO.Requests.Admin;
namespace User.Application.CQRS.Admin.AddUserClaim;

public sealed record AddUserClaimCommand(Guid UserId, AddUserClaimRequest Data) : IRequest<UserResult<bool>>;
