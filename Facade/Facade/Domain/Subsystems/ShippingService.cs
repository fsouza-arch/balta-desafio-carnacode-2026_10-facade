namespace Facade.Domain.Subsystems;

public class ShippingService
{
    public decimal CalculateShipping(string zipCode, decimal weight)
    {
        Console.WriteLine($"[Envio] Calculando frete para CEP {zipCode}");
        return 15.00m;
    }

    public string CreateShippingLabel(string orderId, string address)
    {
        Console.WriteLine($"[Envio] Criando etiqueta para pedido {orderId}");
        return $"LABEL_{orderId}";
    }

    public void SchedulePickup(string labelId, DateTime date)
    {
        Console.WriteLine($"[Envio] Agendando coleta {labelId} para {date:dd/MM/yyyy}");
    }
}
