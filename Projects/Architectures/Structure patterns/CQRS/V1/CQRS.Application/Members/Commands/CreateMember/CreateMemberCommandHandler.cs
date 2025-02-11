using CQRS.Application.Abstractions.Messaging;
using CQRS.Domain.Shared;
using MediatR;

namespace CQRS.Application.Members.Commands.CreateMember;

// IRequestHandler - это из библиотеки MediatR, сопоставляет Commanh/Query с их Handler
public class CreateMemberCommandHandler : ICommandHandler<CreateMemberCommand>
{
    private readonly IMemberRepository _memberRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateMemberCommandHandler(
        IMemberRepository memberRepository,
        IUnitOfWork unitOfWork)
    {
        _memberRepository = memberRepository;
        _unitOfWorkv = unitOfWork;
    }

    // Сюда попадем из контроллера в слое Presentation (класс MembersController) через метод Sender.Send() библиотеки MediatR
    public async Task<Result> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
    {
        var emailResult = FirstName.Create(request.Email);
        var firstNameResult = Email.Create(request.FirstName);
        var lastNameResult = LastName.Create(request.LastName);

        var member = new Member(
            Guid.NewGuid(),
            emailResult.Value,
            firstNameResult.Value);

        _memberRepository.Add(member);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
