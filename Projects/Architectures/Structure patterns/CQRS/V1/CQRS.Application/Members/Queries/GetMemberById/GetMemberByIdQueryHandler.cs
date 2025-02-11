using CQRS.Application.Abstractions.Messaging;
using CQRS.Domain.Shared;

namespace CQRS.Application.Members.Queries.GetMemberById;

public sealed class GetMemberByIdQueryHandler
    : IQueryHandler<GetMemberByIdQuery, MemberResponse>
{
    private readonly IMemberRepository _memberRepository;

    public GetMemberByIdQueryHandler(IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }

    public async Task<Result<MemberResponse>> Handle(
        GetMemberByIdQuery request,
        CancellationToken cancellationToken)
    {
        var member = await _memberRepository.GetMemberByIdAsync(request.MemberId, cancellationToken);

        if (member is null)
        {
            return Result.Failure<MemberResponse>(new Error(
                "Member.NotFoud",
                $"The member with Id {request.MemberId} was not found"));
        }

        var response = new MemberResponse(member.Id, member.Email.Value);

        return response;
    }
}
