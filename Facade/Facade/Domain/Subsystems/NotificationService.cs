namespace Facade.Domain.Subsystems;

public class NotificationService
{
    public void SendOrderConfirmation(string email, string orderId)
    {
        Console.WriteLine($"[Notificação] Enviando confirmação de pedido {orderId} para {email}");
    }

    public void SendPaymentReceipt(string email, string transactionId)
    {
        Console.WriteLine($"[Notificação] Enviando recibo de pagamento {transactionId}");
    }

    public void SendShippingNotification(string email, string trackingCode)
    {
        Console.WriteLine($"[Notificação] Enviando código de rastreamento {trackingCode}");
    }
}
