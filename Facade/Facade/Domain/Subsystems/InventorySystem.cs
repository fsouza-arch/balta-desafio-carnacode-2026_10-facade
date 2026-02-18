namespace Facade.Domain.Subsystems;

public class InventorySystem
{
    private Dictionary<string, int> _stock = new Dictionary<string, int>
    {
        ["PROD001"] = 10,
        ["PROD002"] = 5,
        ["PROD003"] = 0
    };

    public bool CheckAvailability(string productId)
    {
        Console.WriteLine($"[Estoque] Verificando disponibilidade de {productId}...");
        return _stock.ContainsKey(productId) && _stock[productId] > 0;
    }

    public void ReserveProduct(string productId, int quantity)
    {
        Console.WriteLine($"[Estoque] Reservando {quantity}x {productId}");
        if (_stock.ContainsKey(productId)) _stock[productId] -= quantity;
    }

    public void ReleaseReservation(string productId, int quantity)
    {
        Console.WriteLine($"[Estoque] Liberando reserva de {quantity}x {productId}");
        if (_stock.ContainsKey(productId)) _stock[productId] += quantity;
    }
}
