using Facade.Domain.DTOs;
using Facade.Domain.Subsystems;

namespace Facade.Application.Facades;

public class OrderFacade
{
    private readonly InventorySystem _inventory = new InventorySystem();
    private readonly PaymentGateway _payment = new PaymentGateway();
    private readonly ShippingService _shipping = new ShippingService();
    private readonly CouponSystem _coupon = new CouponSystem();
    private readonly NotificationService _notification = new NotificationService();

    public bool PlaceOrder(OrderDTO order)
    {
        Console.WriteLine($"\n--- Facade: Iniciando processamento do pedido para {order.CustomerEmail} ---");

        try
        {
            // 1. Verificar e Reservar Estoque
            if (!_inventory.CheckAvailability(order.ProductId))
            {
                Console.WriteLine("❌ Produto indisponível no estoque.");
                return false;
            }
            _inventory.ReserveProduct(order.ProductId, order.Quantity);

            // 2. Calcular Descontos e Frete
            decimal discount = 0;
            if (!string.IsNullOrEmpty(order.CouponCode) && _coupon.ValidateCoupon(order.CouponCode))
            {
                discount = _coupon.GetDiscount(order.CouponCode);
            }

            decimal subtotal = order.ProductPrice * order.Quantity;
            decimal shippingCost = _shipping.CalculateShipping(order.ZipCode, order.Quantity * 0.5m);
            decimal total = subtotal * (1 - discount) + shippingCost;

            // 3. Processar Pagamento
            string txnId = _payment.InitializeTransaction(total);
            if (!_payment.ValidateCard(order.CreditCard, order.Cvv) || !_payment.ProcessPayment(txnId, order.CreditCard))
            {
                _inventory.ReleaseReservation(order.ProductId, order.Quantity);
                Console.WriteLine("❌ Falha no pagamento. Reserva liberada.");
                return false;
            }

            // 4. Logística e Envio
            string orderId = $"ORD_{DateTime.Now.Ticks}";
            string labelId = _shipping.CreateShippingLabel(orderId, order.ShippingAddress);
            _shipping.SchedulePickup(labelId, DateTime.Now.AddDays(1));

            // 5. Finalização e Notificações
            if (!string.IsNullOrEmpty(order.CouponCode)) _coupon.MarkCouponAsUsed(order.CouponCode, order.CustomerEmail);

            _notification.SendOrderConfirmation(order.CustomerEmail, orderId);
            _notification.SendPaymentReceipt(order.CustomerEmail, txnId);
            _notification.SendShippingNotification(order.CustomerEmail, labelId);

            Console.WriteLine($"--- Facade: Pedido {orderId} finalizado com sucesso! ---\n");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Erro crítico na Facade: {ex.Message}");
            return false;
        }
    }
}
