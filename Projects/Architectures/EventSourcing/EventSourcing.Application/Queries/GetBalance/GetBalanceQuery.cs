namespace EventSourcing.Application.Queries.GetBalance;

// Входные параметры запроса
public sealed record GetBalanceQuery(Guid accountId);
