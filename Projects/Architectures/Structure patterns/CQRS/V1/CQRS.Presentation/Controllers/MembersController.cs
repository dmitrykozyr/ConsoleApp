using CQRS.Application.Members.Commands.CreateMember;
using CQRS.Application.Members.Queries.GetMemberById;
using CQRS.Domain.Shared;
using CQRS.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CQRS.Presentation.Controllers;

[Route("api/members")]
public sealed class MembersController : ApiController
{
    public MembersController(ISender sender)
        : base(sender)
    {
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMemberById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetMemberByIdQuery(id);

        Result<MemberResponse> response = await Sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
    }

    [HttpPost]
    public async Task<IActionResult> RegisterMember(CancellationToken cancellationToken)
    {
        // CreateMemberCommand определен в Application
        var command = new CreateMemberCommand(
            "email",
            "name",
            "surname");

        // Пересылаем запрос из Presentation в Application
        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
    }
}
