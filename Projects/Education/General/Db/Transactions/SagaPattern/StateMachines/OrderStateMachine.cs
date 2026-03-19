using Education.General.Db.Transactions.SagaPattern.Records;
using Education.General.Db.Transactions.SagaPattern.Records.Commands;
using Education.General.Db.Transactions.SagaPattern.Records.Events;
using Education.General.Db.Transactions.SagaPattern.States;
using MassTransit;

namespace Education.General.Db.Transactions.SagaPattern.StateMachines;

public class OrderStateMachine : MassTransitStateMachine<OrderSagaState>
{
    // Описываем состояния
    public State Accepted { get; private set; }
    public State Paid { get; private set; }
    public State Failed { get; private set; }

    // Описываем события
    public Event<OrderSubmitted> OrderSubmitted { get; private set; }
    public Event<PaymentConfirmed> PaymentConfirmed { get; private set; }
    public Event<PaymentFailed> PaymentFailed { get; private set; }

    public OrderStateMachine()
    {
        // Поле, по которому MassTransit находит нужный экземпляр саги
        InstanceState(orderSagaState => orderSagaState.CurrentState);

        // Когда пришел новый заказ
        Initially(
         When(OrderSubmitted)
          .Then(context =>
          {
              context.Saga.OrderId = context.Message.OrderId;
              context.Saga.Amount = context.Message.Amount;
          })
          .TransitionTo(Accepted)
          .Publish(context => new ProcessPayment(context.Message.OrderId)
        ));

        // Когда оплата прошла успешно
        During(Accepted,
         When(PaymentConfirmed)
          .TransitionTo(Paid)
          .Finalize() // Завершаем сагу
        );

        // КОМПЕНСАЦИЯ: Если оплата не прошла
        During(Accepted,
         When(PaymentFailed)
          .TransitionTo(Failed)
          .Then(context => Console.WriteLine("Откат: отменяем резерв товара"))
        );
    }
}
