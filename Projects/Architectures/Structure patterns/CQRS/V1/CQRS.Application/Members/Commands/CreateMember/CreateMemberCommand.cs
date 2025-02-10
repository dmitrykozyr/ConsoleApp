using System.Windows.Input;

namespace CQRS.Application.Members.Commands.CreateMember;

public sealed record CreateMemberCommand(
    string Email,
    string FirstName,
    string LastName) : ICommand;
