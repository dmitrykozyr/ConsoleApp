namespace CQRS.Application.Queries.GetCustomerById;

public sealed record CustomerDetailsDto(long Id, string Name, string Address);
