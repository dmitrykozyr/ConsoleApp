using MassTransit;

namespace Education.General.Db.Transactions.SagaPattern.States;

public class OrderSagaState : SagaStateMachineInstance
{
    // ID саги, связывающий все сообщения
    public Guid CorrelationId { get; set; }

    // Текущий статус (Submitted, Paid и т.д.)
    public string? CurrentState { get; set; }

    public int OrderId { get; set; }

    public decimal Amount { get; set; }
}
