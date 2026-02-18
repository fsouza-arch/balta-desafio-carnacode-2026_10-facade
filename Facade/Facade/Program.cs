using Facade.Application.Facades;
using Facade.Domain.DTOs;

Console.WriteLine("=== Sistema de E-commerce (Padrão Facade) ===\n");

// O CLIENTE AGORA É SIMPLES!
// Ele não precisa conhecer InventorySystem, PaymentGateway, etc.
var facade = new OrderFacade();

var order = new OrderDTO
{
    ProductId = "PROD001",
    Quantity = 2,
    CustomerEmail = "cliente@email.com",
    CreditCard = "1234567890123456",
    Cvv = "123",
    ProductPrice = 100.00m,
    CouponCode = "PROMO10",
    ZipCode = "12345-678",
    ShippingAddress = "Rua das Flores, 10"
};

// Apenas uma chamada resolve toda a complexidade!
bool success = facade.PlaceOrder(order);

if (success)
{
    Console.WriteLine("Resultado Final: Pedido concluído com sucesso pelo cliente!");
}
else
{
    Console.WriteLine("Resultado Final: Falha ao processar o pedido.");
}