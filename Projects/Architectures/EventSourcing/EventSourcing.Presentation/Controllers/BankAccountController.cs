using EventSourcing.Application.Commands.DepositMoney;
using EventSourcing.Application.Commands.WithdrawMoney;
using EventSourcing.Application.Queries.GetBalance;
using Microsoft.AspNetCore.Mvc;

namespace EventSourcing.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class BankAccountController : ControllerBase
{
    private readonly DepositMoneyHandler _deposit;
    private readonly WithdrawMoneyHandler _withdraw;
    private readonly GetBalanceHandler _balance;

    public BankAccountController(
        DepositMoneyHandler deposit,
        WithdrawMoneyHandler withdraw,
        GetBalanceHandler balance)
    {
        _deposit = deposit;
        _withdraw = withdraw;
        _balance = balance;
    }

    public sealed record AmountBody(decimal Amount);

    [HttpPost("{accountId:guid}/deposit")]
    public async Task<IActionResult> Deposit(Guid accountId, [FromBody] AmountBody body, CancellationToken cancellationToken)
    {
        await _deposit.HandleAsync(new DepositMoneyCommand(accountId, body.Amount), cancellationToken);

        return NoContent();
    }

    [HttpPost("{accountId:guid}/withdraw")]
    public async Task<IActionResult> Withdraw(Guid accountId, [FromBody] AmountBody body, CancellationToken cancellationToken)
    {
        try
        {
            await _withdraw.HandleAsync(new WithdrawMoneyCommand(accountId, body.Amount), cancellationToken);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpGet("{accountId:guid}/balance")]
    public async Task<ActionResult<decimal>> Balance(Guid accountId, CancellationToken cancellationToken)
    {
        var b = await _balance.HandleAsync(new GetBalanceQuery(accountId), cancellationToken);

        if (b is null)
        {
            return NotFound();
        }

        return Ok(b.Value);
    }
}
