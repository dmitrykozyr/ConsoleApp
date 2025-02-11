namespace CQRS.Application.Members.Queries.GetMemberById;

public sealed record MemberResponse(Guid Id, string Email);
