namespace Facade.Domain.Subsystems;

public class PaymentGateway
{
    public string InitializeTransaction(decimal amount)
    {
        Console.WriteLine($"[Pagamento] Inicializando transação de R$ {amount:N2}");
        return $"TXN{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";
    }

    public bool ValidateCard(string cardNumber, string cvv)
    {
        Console.WriteLine($"[Pagamento] Validando cartão {cardNumber}");
        return cardNumber.Length == 16;
    }

    public bool ProcessPayment(string transactionId, string cardNumber)
    {
        Console.WriteLine($"[Pagamento] Processando pagamento {transactionId}");
        return true;
    }

    public void RollbackTransaction(string transactionId)
    {
        Console.WriteLine($"[Pagamento] Revertendo transação {transactionId}");
    }
}
