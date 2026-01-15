using gRPC.Service;
using gRPC.Service.Protos;
using Grpc.Net.Client;

// Указываем порт, на котором сервер слушает, если его запустить
//var channel = GrpcChannel.ForAddress("http://localhost:5094");
//var client = new UserService.UserServiceClient(channel);

//var createUserRequest = new CreateUserRequest
//{
//    Email = "test@test.com",
//    Name = "Test User"
//};

//var reply = client.CreateUser(createUserRequest);

//Console.WriteLine(reply.Id);


var channel = GrpcChannel.ForAddress("http://localhost:5094");
var customerClient = new Customer.CustomerClient(channel);

var customerLookupModel = new CustomerLookupModel
{
    UserId = 1
};

var customer = await customerClient.GetCustomerInfoAsync(customerLookupModel);

Console.WriteLine(customer.FirstName + " " + customer.LastName);

Console.ReadLine();

