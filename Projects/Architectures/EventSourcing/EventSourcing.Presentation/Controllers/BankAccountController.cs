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

    [HttpPost("{accountId:guid}/deposit")]
    public async Task<IActionResult> Deposit(Guid accountId, decimal amount, CancellationToken cancellationToken)
    {
        try
        {
            await _deposit.HandleAsync(new DepositMoneyCommand(accountId, amount), cancellationToken);

            return NoContent();
        }
        catch (Exception ex)
        {
            throw new Exception($"Исключение: {ex}");
        }
    }

    [HttpPost("{accountId:guid}/withdraw")]
    public async Task<IActionResult> Withdraw(Guid accountId, decimal amount, CancellationToken cancellationToken)
    {
        try
        {
            await _withdraw.HandleAsync(new WithdrawMoneyCommand(accountId, amount), cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception($"Исключение: {ex}");
        }

        return NoContent();
    }

    [HttpGet("{accountId:guid}/balance")]
    public async Task<ActionResult<decimal>> Balance(Guid accountId, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _balance.HandleAsync(new GetBalanceQuery(accountId), cancellationToken);

            if (result is null)
            {
                return NotFound();
            }

            return Ok(result.Value);
        }
        catch (Exception ex)
        {
            throw new Exception($"Исключение: {ex}");
        }
    }
}
