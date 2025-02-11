using CQRS.Application.Abstractions.Messaging;

namespace CQRS.Application.Members.Queries.GetMemberById;

public sealed record GetMemberByIdQuery(Guid MemberId) : IQuery<MemberResponse>;
