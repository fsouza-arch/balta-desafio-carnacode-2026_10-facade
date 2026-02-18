namespace Facade.Domain.DTOs;

public class OrderDTO
{
    public string ProductId { get; set; }
    public int Quantity { get; set; }
    public string CustomerEmail { get; set; }
    public string CreditCard { get; set; }
    public string Cvv { get; set; }
    public string ShippingAddress { get; set; }
    public string ZipCode { get; set; }
    public string CouponCode { get; set; }
    public decimal ProductPrice { get; set; }
}
