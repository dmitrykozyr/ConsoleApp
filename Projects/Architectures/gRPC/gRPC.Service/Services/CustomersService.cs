using gRPC.Service.Protos;
using Grpc.Core;

namespace gRPC.Service.Services;

// Сначала создаем протофайл: customers.proto
// В его Properties выбираем нужные значения
// Затем создаем данный сервис и наследуемся от сгенерированного Customers
public class CustomersService : Customer.CustomerBase
{
    public ILogger<CustomersService> _logger { get; }

    public CustomersService(ILogger<CustomersService> logger)
    {
        _logger = logger;
    }

    public override Task<CustomerModel> GetCustomerInfo(CustomerLookupModel request, ServerCallContext context)
    {
        var customerModel = new CustomerModel();

        if (request.UserId == 1)
        {
            customerModel.FirstName = "Name 1";
            customerModel.LastName = "Last Name 1";
        }
        else
        {
            customerModel.FirstName = "Name 2";
            customerModel.LastName = "Last Name 2";
        }

        return Task.FromResult(customerModel);
    }
}
